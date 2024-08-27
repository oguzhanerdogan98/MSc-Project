using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> Obstacles;
    public List<GameObject> SpawnPoints;

    public void SetSpawnPoints() //We have 3 different type obstacles this method select ones randomly and create.
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            var random = Random.Range(0, 3);
            var obj = Instantiate(Obstacles[random], SpawnPoints[i].transform.position, Quaternion.identity);
            obj.SetActive(true);
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
            gameManager.GetComponent<EndlessRoad>().ObstacleList.Add(obj);
        }
    }
}
