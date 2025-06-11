using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    [SerializeField] bool DoPersist = false;
    

    // Start is called before the first frame update
    protected virtual void OnEnable() {
        if (Instance == null) {
            Instance = this as T;
            if (DoPersist) {
                DontDestroyOnLoad(gameObject);
            }
        } else if (Instance != this) {
            Destroy(this.gameObject);
        }
    }
}
