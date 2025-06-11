using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LoadingBar : MonoBehaviour
{
    
    Slider bar;

    private void Start() {
        bar = GetComponent<Slider>();
    }

    public void setPercentage(float percentage) {
        bar.value = percentage;
    }

}
