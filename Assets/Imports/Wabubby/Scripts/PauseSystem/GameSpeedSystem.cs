using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wabubby
{
    public class GameSpeedSystem : Singleton<GameSpeedSystem>
    {
        private static float gameSpeedScale = 1f;
        public static float GameSpeedScale {
            get => gameSpeedScale;
            set {
                gameSpeedScale = value;
                if (!PauseSystem.IsPaused) Time.timeScale = GameSpeedScale;
            }
        }

        #if UNITY_EDITOR
        [SerializeField, Range(0.01f, 1f)] float timeScale = 1;
        float oldTimeScale = 1;
        void Update() {
            if (timeScale != oldTimeScale) Time.timeScale = timeScale;
            oldTimeScale = timeScale;
        }
        #endif
    }
}
