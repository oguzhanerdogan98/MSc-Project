using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform Player;
    public Vector3 OffSet;
    public float SmoothSpeed = 0.125f;
    public Vector3 CameraStartPos;


    private void Start()
    {
        CameraStartPos = transform.position;
        OffSet = transform.position - Player.position; //offset player and camera
    }
    private void Update()
    {
        Vector3 desiredPosition = Player.position + OffSet;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed); //smoothly moves the camera
        transform.position = smoothedPosition;
    }

    public void UpdateCamPos() //this method using when game restarts. Teleport camera to start position
    {
        transform.position = CameraStartPos;
    }
}
