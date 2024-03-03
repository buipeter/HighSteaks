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
    public float maxLookUp;
    public float maxLookDown;
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = player.position - transform.position;
        }

        cameraPivot.transform.position = player.transform.position;
        cameraPivot.transform.parent = player.transform;
        Cursor.lockState = CursorLockMode.Locked;
        //maxLookDown = 70f;
        //maxLookUp = 70f;
    }

    void Update()
    {
        if (PauseMenu.isPaused || GameManager.isLevelComplete)
        {
            // turns off camera functionality when game is paused or level is compeleted
        }
        else
        {
            // get the X position of the mouse & rotates the pivot
            float horizontal = Input.GetAxisRaw("Mouse X") * rotateSpeed;
            player.Rotate(0, horizontal, 0);

            // get the Y position of the mouse & rotates the pivot
            float vertical = -Input.GetAxisRaw("Mouse Y") * rotateSpeed;
            cameraPivot.Rotate(vertical, 0, 0);

            // limit range of motion of the camera - rotation up and down
            if (cameraPivot.rotation.eulerAngles.x > maxLookDown && cameraPivot.rotation.eulerAngles.x < 180f)
            {
                cameraPivot.rotation = Quaternion.Euler(maxLookDown, 0, 0);
            }
            if (cameraPivot.rotation.eulerAngles.x > 180f && cameraPivot.rotation.eulerAngles.x < 360f - maxLookUp)
            {
                cameraPivot.rotation = Quaternion.Euler(360f - maxLookUp, 0, 0);
            }

            // moves the camera based on the current rotation of the player and the original offset
            float desiredYAngle = player.eulerAngles.y;
            float desiredXAngle = cameraPivot.eulerAngles.x;
            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position = player.position - (rotation * offset);

            // limit range of motion of the camera - y position 
            // TODO: camera will still clip through objects. may or may not need to be fixed
            float cappedY = Mathf.Max(player.position.y - .5f, transform.position.y);
            transform.position = new Vector3(transform.position.x, cappedY, transform.position.z);
            transform.LookAt(player);
        }
    }
}
