using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string _colour;

    public string GetColour()
    {
        return _colour;
    }

    public void SetColour(string colour)
    {
        this._colour = colour;
    }
}
