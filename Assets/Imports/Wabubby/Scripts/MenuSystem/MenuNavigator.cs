using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class MenuNavigator : MonoBehaviour {

    [SerializeField] private Menu DefaultMenu;

    public Menu[] MenuComponents;

    [HideInInspector] public Menu currentMenu;

    public Stack<Menu> MenuStack ; // stored menu stack when the player presses the back key

    public bool LoadOnAwake;

    protected virtual void Awake() {
        MenuStack = new Stack<Menu>();
        if (LoadOnAwake) { StartMenus(); }
    }

    protected virtual void Start() {
        MenuSystem.Instance.PlayerInputComponent.actions["Cancel"].started += ctx => EscapeKeyPressed(ctx);
        MenuSystem.Instance.PlayerInputComponent.actions["Submit"].started += ctx => Test();
    }

    [ContextMenu("AutoSet Menu Components")]
    public void AutoSetMenuComponents() {
        Menu[] temp = new Menu[transform.childCount];

        int i=0;
        foreach (Transform childTransform in transform) {
            Menu childMenu = childTransform.GetComponent<Menu>();
            if (childMenu != null) { temp[i] = childMenu; i++; }
        }

        MenuComponents = new Menu[i];
        for (int j=0; j<i; j++) { MenuComponents[j] = temp[j]; }

        if (DefaultMenu == null) { // make the first menu the default none is preset
            DefaultMenu = MenuComponents[0];
        }
    }

    public void StartMenus() {
        Debug.Log("starting menus...");
        SwitchCurrentMenu(DefaultMenu);
    }

    protected void EndMenus() {
        EndCurrentMenu();
        MenuStack.Clear();
        currentMenu = null;
    }

    public void SwitchCurrentMenu(Menu menu) {
        if (! IsMenuApart(menu)) { Debug.LogError($"Menu {menu.gameObject.name} not apart of menu navigator {gameObject.name}"); return; }
        
        Menu oldMenu = currentMenu;

        currentMenu = menu;
        FocusCurrentMenu();

        oldMenu?.gameObject.SetActive(false);

        MenuStack.Push(menu);
    }

    public bool IsMenuApart(Menu menu) {
        for(int i=0; i<MenuComponents.Length; i++) {
            if (MenuComponents[i] == menu) {
                return true;
            }
        }
        return false;
    }

    protected void FocusCurrentMenu() {
        if (currentMenu == null) { return; }
        currentMenu.gameObject.SetActive(true);
        currentMenu.FocusMenu();
    }

    protected void EndCurrentMenu() {
        if (currentMenu == null) { return; }
        currentMenu.UnfocusMenu();
        currentMenu.gameObject.SetActive(false);
    }
    
    public void EscapeKeyPressed(InputAction.CallbackContext ctx) {
        Debug.Log($"on escape called {Time.frameCount}");

        SwitchBack();
        
    }

    public void Test() {
        Debug.Log("brother!");
    }

    public virtual void SwitchBack() {
        // if (MenuStack.Count > 1) { // don't go back if last loaded menu(the currrent menu) is left
        //     MenuStack.Pop(); // current scene
        //     SwitchCurrentMenu(MenuStack.Peek());
        //     MenuStack.Pop(); // remove the one just added.
// 
        //     Debug.Log($"\t\t the current stack length is {MenuStack.Count}");
        // }
    }


}
