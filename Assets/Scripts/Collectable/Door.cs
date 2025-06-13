using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string Type;
    [SerializeField]
    private bool _isOpen = false;

    public Door(string type)
    {
        this.Type = type;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }

    public virtual void Unlock(bool unlocked)
    {
        if (unlocked)
            _isOpen = true;
    }
}
