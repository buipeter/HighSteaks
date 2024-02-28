using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool useOffsetValues;

    public float rotateSpeed;

    void Start()
    {
        if (!useOffsetValues)
        {
            offset = player.position - transform.position;
        }
        
    }

    void Update()
    {
        // get the X position of the mouse & rotates the player
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        player.Rotate(vertical, 0, 0);

        // moves the camera based on the current rotation of the player and the original offset
        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = player.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        transform.LookAt(player);
    }
}
