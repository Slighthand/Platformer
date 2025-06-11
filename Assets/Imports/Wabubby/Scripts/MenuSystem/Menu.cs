using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{

    [HideInInspector] public bool isFocused;

    public Selectable DefaultSelectedComponent;
    public GameObject[] MenuComponents; // we use gameObject instead of transform to the access component line

    public Menu BackMenu; // the menu that is loaded when back is pressed


    [ContextMenu("AutoSet Menu Components")]
    public void AutoSetMenuComponents() { // find some way to make this automatically run when children are updated
        MenuComponents = new GameObject[transform.childCount];

        int i=0;
        foreach (Transform childTransform in transform) {
            MenuComponents[i] = childTransform.gameObject;
            i++;
        }

        if (DefaultSelectedComponent == null) { // make the first component the default none is preset
            foreach (GameObject menuComponent in MenuComponents) {
                Selectable selectableComponent = menuComponent.GetComponent<Selectable>();
                if (selectableComponent != null) { DefaultSelectedComponent = selectableComponent; break; }
            }
        }
    }

    public void FocusMenu(bool doAlert=true) {
        foreach (GameObject menuComponent in MenuComponents) { 
            Selectable selectable = menuComponent.GetComponent<Selectable>();
            if (selectable == null) { continue; }
            selectable.interactable = true; 
        }
        if (DefaultSelectedComponent != null) {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedComponent.gameObject);
        }
        
        
        if (doAlert) {MenuSystem.Instance.AlertFocus(this);}
    }

    public void UnfocusMenu(bool doAlert=true) {
        foreach (GameObject menuComponent in MenuComponents) { 
            Selectable selectable = menuComponent.GetComponent<Selectable>();
            if (selectable == null) { continue; }
            selectable.interactable = false; 
        }

         if (doAlert) {MenuSystem.Instance.AlertUnfocus(this);}
    }

    private void OnDisable() {
        UnfocusMenu();
    }

}
