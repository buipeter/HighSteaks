using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool useOffsetValues;

    public float rotateSpeed;
    public Transform cameraPivot;

    void Start()
    {
        if (!useOffsetValues)
        {
            offset = player.position - transform.position;
        }

        cameraPivot.transform.position = player.transform.position;
        cameraPivot.transform.parent = player.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        // get the X position of the mouse & rotates the pivot
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        // get the Y position of the mouse & rotates the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        cameraPivot.Rotate(vertical, 0, 0);

        // moves the camera based on the current rotation of the player and the original offset
        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = cameraPivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        if (transform.position.y < player.position.y - .5f)
        {
            int y = 1;
        }
        transform.LookAt(player);
    }
}
