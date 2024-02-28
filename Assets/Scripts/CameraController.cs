using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Start()
    {
        offset = player.position - transform.position;
        
    }

    void Update()
    {
        transform.position = player.position - offset;
        transform.LookAt(player);
    }
}
