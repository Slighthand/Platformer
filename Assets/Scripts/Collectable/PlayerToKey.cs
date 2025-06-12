using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject[] AllKeys;
    public GameObject NearestKey;
    float distance;
    float nearestDistance = 10000;
    // Start is called before the first frame update
    void Start()
    {
        AllKeys = GameObject.FindGameObjectsWithTag("Key");

        for(int i = 0; i < AllKeys.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, AllKeys[i].transform.position);

            if(distance < nearestDistance)
            {
                NearestKey = AllKeys[i];
                nearestDistance = distance;
            }

        }
            


    }

}
