using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSystem : Singleton<MenuSystem>
{

    public PlayerInput PlayerInputComponent;
    
    public List<Menu> Menus;

    public string PreviousActionMap;

    public Menu CurrentFocusMenu;
    public List<Menu> FocusMenuStack;

    private Coroutine CheckDisableCorountine; 

    public void AlertFocus(Menu menu) { // whenever a menu is enabled
        Debug.Log($"focusing menu {menu.gameObject.name}");

        if (FocusMenuStack.Count == 0) { // we have just entered menus!
            PreviousActionMap = PlayerInputComponent.currentActionMap.name;
            PlayerInputComponent.SwitchCurrentActionMap("UI");

            Debug.Log("menu focused! switching to UI");
        }
        
        if (FocusMenuStack.Count == 0 || menu != FocusMenuStack[FocusMenuStack.Count - 1]) {
            FocusMenuStack.Add(menu); // add the menu on the go back stack
        }

        if (CurrentFocusMenu != null) { CurrentFocusMenu.UnfocusMenu(false); } // goodbye sweet prince D:

        CurrentFocusMenu = menu; // oh la la
    }

    public void AlertUnfocus(Menu menu) { // whenever a menu is disabled
        Debug.Log($"unfocusing menu {menu.gameObject.name}");

        FocusMenuStack.RemoveAll(m => m == menu);
        
        if (menu == CurrentFocusMenu) { // out of the spotlight
            CurrentFocusMenu = null;
            if (FocusMenuStack.Count > 0) {
                FocusMenuStack[FocusMenuStack.Count - 1].FocusMenu(); // in with the new
                // unless it gets booted right away because its disabled or something
            }
        }

        CheckNoMenus();
    }

    private void CheckNoMenus() {
        if (FocusMenuStack.Count == 0) {
            Debug.Log($"all menus disabled... switching to {PreviousActionMap}");

            // sometimes actions are disabled when exiting the rebind menus...
            if (PlayerInputComponent == null) { return; }

            foreach (InputAction action in PlayerInputComponent.actions) {
                if (action.actionMap.name == PreviousActionMap) {
                    action.Enable();
                }
            }

            PlayerInputComponent.SwitchCurrentActionMap(PreviousActionMap);
        }
    }


}
