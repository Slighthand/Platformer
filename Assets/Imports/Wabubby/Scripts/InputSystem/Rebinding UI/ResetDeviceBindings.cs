using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetDeviceBindings : MonoBehaviour
{
    
    [SerializeField] private InputActionAsset _InputActions;

    [SerializeField] private string _TargetControlScheme;

    public void ResetAllBindings() {
        foreach (InputActionMap map in _InputActions.actionMaps) {
            map.RemoveAllBindingOverrides();
        }
    }

    public void ResetControlSchemeBindings() {
        foreach (InputActionMap map in _InputActions.actionMaps) {
            foreach (InputAction action in map.actions) {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(_TargetControlScheme));
            }
        }
    }

}
