using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class PauseMenuNavigator : MenuNavigator {

    protected override void Start() {
        base.Start();

        PauseSystem.OnPause += PauseListener;
        PauseSystem.OnUnpause += UnpauseListener;
    }

    private void PauseListener() {
        Debug.Log("pause menu system starting");
        
        StartMenus();
    }
    
    private void UnpauseListener() {
        Debug.Log("pause menu system stopping");
        
        EndMenus();
    }

    public override void SwitchBack() {
        if (PauseSystem.LastPauseFrame == Time.frameCount) { return; }

        if (MenuStack.Count >= 2) { // don't go back if last loaded menu(the currrent menu) is left
            MenuStack.Pop(); // current scene
            SwitchCurrentMenu(MenuStack.Peek());
            MenuStack.Pop(); // remove the one just added.

        } else if (MenuStack.Count == 1) { // none left in the stack.
            PauseSystem.Instance.Unpause();
        }
        
    }

}