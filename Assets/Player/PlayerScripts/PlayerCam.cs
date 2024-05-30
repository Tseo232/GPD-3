using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCam : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Transform player;
    public Transform cameraPivot; // Reference to the camera pivot
    public float sensitivity = 1f;
    public float minY = -30f;
    public float maxY = 70f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (cinemachineVirtualCamera == null || player == null || cameraPivot == null)
            return;

        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Rotate the player around the Y-axis (horizontal rotation)
        player.rotation = Quaternion.Euler(0f, rotationX, 0f);

        // Rotate the camera pivot around the X-axis (vertical rotation)
        cameraPivot.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }
}
