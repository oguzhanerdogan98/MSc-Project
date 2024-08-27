using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Import UI library for using UI

public class EndlessRoad : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject roadPrefab; //Road model for spawn again.
    public GameObject AIModel; 
    public GameObject PlayerModel;
    public GameObject AIObstacles; //Parent of objects that control obstacles
    public List<GameObject> ObstacleList;

    [Header("Methods")]
    public CameraFollowing cameraFollowing; 

    [Header("Others")]
    public Transform Player;
    public int InitialRoadCount = 5; //Road Count when game stars. 
    public float RoadLength = 40f; // Specifies how many meters after the new road
    public bool isAISelected = false; //Whe nyou choose AI its turns true

    [Header("UI")]
    public GameObject MainMenu;
    public GameObject InGamePanel;

    //Private Vars
    private Queue<GameObject> roads = new Queue<GameObject>(); // basic queue variable for road collection
    private float spawnX = 0f;
    private float safeZone = 15f;
    private Vector3 playerStartPos;

    private void Start()
    {
        playerStartPos = Player.position;
        for (int i = 0; i < InitialRoadCount; i++) //Generates roads when game starts
        {
            SpawnRoad();
        }
    }

    private void Update()
    {
        if(Player.position.x - safeZone > spawnX - (InitialRoadCount * RoadLength)) 
        {
            SpawnRoad();
            RemoveOldestRoad();
        }
    }
    
    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, new Vector3(spawnX, 0, 0), Quaternion.identity); //Create road object in specified position
        roads.Enqueue(road); // Adds created road to roads queue
        spawnX += RoadLength;
        road.GetComponent<ObstacleSpawner>().SetSpawnPoints(); //adds obstacles 
    }

    private void RemoveOldestRoad()
    {
        GameObject oldRoad = roads.Dequeue(); 
        Destroy(oldRoad, 5f); //destroys old road 5 seconds later.
    }

    public void StartAsAI() //When we press AI button in MainMenu this method run
    {
        isAISelected = true;
        PlayerModel.SetActive(false);
        AIModel.SetActive(true);
        Player.gameObject.SetActive(true);
        MainMenu.SetActive(false);
        AIObstacles.SetActive(true);
        InGamePanel.SetActive(true);
    }

    public void StartAsPlayer() //When we press Player button in Main Menu this method run
    {
        isAISelected = false;
        AIModel.SetActive(false);
        PlayerModel.SetActive(true);
        Player.gameObject.SetActive(true);
        MainMenu.SetActive(false);
        AIObstacles.SetActive(false);
        InGamePanel.SetActive(true);
    }

    public void RestartGame() //When we press Restart button in Game Over screen this method run
    {
        Player.position = playerStartPos;
        cameraFollowing.UpdateCamPos();
        Player.GetComponent<PlayerMovement>().RestartGamePanel.SetActive(false);
        Player.GetComponent<PlayerMovement>().Speed = 8f;
        Player.GetComponent<PlayerMovement>().isGameOver = false;
        Player.GetComponent<PlayerMovement>().Score = 0;
        MainMenu.SetActive(true);
        Player.gameObject.SetActive(false);
        AIModel.SetActive(false);
        PlayerModel.SetActive(false);
        AIObstacles.SetActive(false);
        InGamePanel.SetActive(false);
        for (int i = 0; i < ObstacleList.Count; i++)
        {
            Destroy(ObstacleList[i]);
        }
        ObstacleList.Clear();
        while (roads.Count > 0)
        {
            GameObject road = roads.Dequeue();
            Destroy(road);
        }
        spawnX = 0f;
        for (int i = 0; i < InitialRoadCount; i++) //Generates roads when game starts
        {
            SpawnRoad();
        }
    }
}
