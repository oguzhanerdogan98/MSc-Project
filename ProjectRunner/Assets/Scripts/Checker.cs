using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Checker : MonoBehaviour
{
    public float AbleRoad1Z; // position 1 wwhere ai can go
    public float AbleRoad2Z; // position 2 wwhere ai can go
    public Transform Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) // AI controller hits the obstacle this method runs.
        {
            var rand = Random.Range(0, 2);
            if(rand == 0)
            {
                Player.DOMoveZ(AbleRoad1Z, 0.5f);
            }
            else if (rand == 1)
            {
                Player.DOMoveZ(AbleRoad2Z, 0.5f);
            }
        }
    }
}
