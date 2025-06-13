using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCoordinator : MonoBehaviour
{
    [NonSerialized] public PlayerControl control;
    [field: SerializeField] public Vector2 Movement {private set; get;}
    [field: SerializeField] public bool Attack {private set; get;}
    [field: SerializeField] public bool Interact {private set; get;}
    [field: SerializeField] public bool Bomb {private set; get;}

    public InputAction TransAction;
    public InputAction AttackAction;
    public InputAction InteractAction;
    public InputAction BombAction;

    void SetupInput() {
        control = new PlayerControl(); // make new Input asset
        control.Enable();
        
        // update associated variables according to input
        control.Player.Move.performed += context => Movement = context.ReadValue<Vector2>();
        control.Player.Move.canceled += context => Movement = context.ReadValue<Vector2>();

        control.Player.Attack.performed += context => Attack = context.ReadValueAsButton();
        control.Player.Attack.canceled += context => Attack = context.ReadValueAsButton();

        control.Player.Interact.performed += context => Interact = context.ReadValueAsButton();
        control.Player.Interact.canceled += context => Interact = context.ReadValueAsButton();

        control.Player.Bomb.performed += context => Interact = context.ReadValueAsButton();
        control.Player.Bomb.canceled += context => Interact = context.ReadValueAsButton();

         // for that extra information
        AttackAction = control.Player.Attack;
        InteractAction = control.Player.Interact;
        BombAction = control.Player.Bomb;
    }

    void OnEnable() {
        SetupInput();
    }

    void OnDisable() {
        control.Disable();
    }

}
