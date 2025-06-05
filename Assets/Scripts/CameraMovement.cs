using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    public float zOffset = -10f;
    public float smoothSpeed = 0.125f; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, zOffset);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        }
    }
}