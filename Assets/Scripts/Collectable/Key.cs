using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string _colour;
    [SerializeField]
    private PlayerInventory _playerInventory;
    [SerializeField]
    private GameObject _door;
    private bool _doorDestroyed;

    public string GetColour()
    {
        return _colour;
    }

    private void Start()
    {
    }
    private void Update()
    {
        if(_playerInventory.Count == 5 && !_doorDestroyed)
        {
            _doorDestroyed = true;
            Destroy(_door);
        }
    }

    public static void BubbleSort(string[] keys)
    {
        for (int j = 0; j < keys.Length; j++)
        {
            bool swapped = false;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (string.Compare(keys[i], keys[i + 1]) > 0)
                {
                    string tmp = keys[i];
                    keys[i] = keys[i + 1];
                    keys[i + 1] = tmp;
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }
    }
}
