using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheckerObjectX : MonoBehaviour
{
    public Transform player;

    private void Update() // jsut for move ai controllers
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
