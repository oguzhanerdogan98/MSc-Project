using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour // This method is not used. I gave up afterwards
{
    public float Speed = 8f;

    private void Update()
    {
        MoveStraight();
    }
    private void MoveStraight()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime;
    }
}
