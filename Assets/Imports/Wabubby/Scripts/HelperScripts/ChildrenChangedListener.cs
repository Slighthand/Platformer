using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class ChildrenChangedListener : MonoBehaviour
{

    public UnityEvent OnChildrenChanged;

    // for some weird reason, probably C# reflection, this must be made public it works with subclasses.
    public void OnTransformChildrenChanged() {
        Debug.Log("bing bong bing bong alert alert");
        OnChildrenChanged.Invoke();
    }
}