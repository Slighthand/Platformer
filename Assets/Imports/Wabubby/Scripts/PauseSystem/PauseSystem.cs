using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Wabubby;

public class PauseSystem : Singleton<PauseSystem>
{
    public PlayerInput PlayerInputComponent;

    public static bool IsPaused { get; private set; }
    public static int LastPauseFrame { get; private set; }

    public delegate void PauseHandler(); public static event PauseHandler OnPause;
    public delegate void UnpauseHandler(); public static event PauseHandler OnUnpause;

    private void OnEnable() {
        PlayerInputComponent.actions["Pause"].performed += (ctx) => TogglePause();
    }

    public void TogglePause() {
        if (IsPaused) { Unpause(); }
        else { Pause(); }
    }

    public void Pause() {
        if (Time.frameCount == LastPauseFrame) { return; }
        LastPauseFrame = Time.frameCount; // helpful for one purpose - denying first frame pause menu cancels.
        
        IsPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        OnPause?.Invoke();
    }

    
    public void Unpause() {
        if (Time.frameCount == LastPauseFrame) { return; }
        LastPauseFrame = Time.frameCount;

        IsPaused = false;
        Time.timeScale = GameSpeedSystem.GameSpeedScale;
        AudioListener.pause = false;

        OnUnpause?.Invoke();
    } 
    
}
