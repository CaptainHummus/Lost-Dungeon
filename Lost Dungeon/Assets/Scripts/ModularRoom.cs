using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularRoom : MonoBehaviour
{
    [SerializeField]
    private bool explored = false;
    [SerializeField]
    GameObject litTorch = null;
    [SerializeField]
    private int targetNumberOfDoors;
    private int currentNumberOfDoors;

    private Camera mainCamera;

    [SerializeField]
    GameObject roomPrefab;
    private GameObject tempRoom;


    [SerializeField]
    public bool northOpen = false;
    [SerializeField]
    bool eastOpen = false;
    [SerializeField]
    bool southOpen = false;
    [SerializeField]
    bool westOpen = false;

    [SerializeField]
    GameObject northWallReference = null;
    [SerializeField]
    GameObject eastWallReference = null;
    [SerializeField]
    GameObject southWallReference = null;
    [SerializeField]
    GameObject westWallReference = null;


    void Start()
    {
        roomPrefab = (GameObject)Resources.Load("ModularRoom");
   
        Debug.Log(targetNumberOfDoors);
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (targetNumberOfDoors == 0)
        {
            targetNumberOfDoors = Random.Range(1, 5);
        }
        else
        {
            UpdateWall(northOpen, northWallReference);
            UpdateWall(eastOpen, eastWallReference);
            UpdateWall(southOpen, southWallReference);
            UpdateWall(westOpen, westWallReference);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (explored)
        {
        }
        else
        {
            RevealAdjacentRooms();
            ExploreRoom();
        }
    }

    private void RevealAdjacentRooms()
    {
        if (northOpen)
        {
            if (!Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1)))
            {
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x,
                                               transform.position.y + 1,
                                               transform.position.z)
                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('S');
            }
        }
        if (eastOpen)
        {
            if (!Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1)))
            {
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x + 1,
                                               transform.position.y,
                                               transform.position.z)
                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('W');
            }
        }
        if (southOpen)
        {
            if (!Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 1)))
            {
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x,
                                               transform.position.y - 1,
                                               transform.position.z)
                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('N');
            }
        }
        if (westOpen)
        {
            if (!Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 1)))
            {
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x - 1,
                                               transform.position.y,
                                               transform.position.z)
                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('E');
            }
        }
    }

    void ExploreRoom()
    {
        litTorch.SetActive(true);
        explored = true;
    }

    void NewTile(char entranceDirection)
    {
        northOpen = false;
        eastOpen = false;
        southOpen = false;
        westOpen = false;
        switch (entranceDirection)
        {
            case 'N':
                northOpen = true;
                break;
            case 'E':
                eastOpen = true;
                break;
            case 'S':
                southOpen = true;
                break;
            case 'W':
                westOpen = true;
                break;
            default:
                Debug.Log("Invalid Entrance Direction");
                break;
        }
        currentNumberOfDoors = 1;
        RandomizeOpenings();
        UpdateWall(northOpen, northWallReference);
        UpdateWall(eastOpen, eastWallReference);
        UpdateWall(southOpen, southWallReference);
        UpdateWall(westOpen, westWallReference);
    }

    void RandomizeOpenings()
    {
        while (currentNumberOfDoors < targetNumberOfDoors)
        {
            int _random = Random.Range(1, 5);
            Debug.Log("_random");
            switch (_random)
            {
                case 1:
                    if (!northOpen)
                    {
                        northOpen = true;
                        currentNumberOfDoors++;
                    }
                    break;
                case 2:
                    if (!eastOpen)
                    {
                        eastOpen = true;
                        currentNumberOfDoors++;
                    }
                    break;
                case 3:
                    if (!southOpen)
                    {
                        southOpen = true;
                        currentNumberOfDoors++;
                    }
                    break;
                case 4:
                    if (!westOpen)
                    {
                        westOpen = true;
                        currentNumberOfDoors++;
                    }
                    break;
                default:
                    Debug.LogError("Faulty random number in room generation");
                    break;
            }
        }


        //if (!eastOpen && UnityEngine.Random.value > 0.5f)
        //{
        //    eastOpen = true;
        //}
        //if (!southOpen && UnityEngine.Random.value > 0.5f)
        //{
        //    southOpen = true;
        //}
        //if (!westOpen && UnityEngine.Random.value > 0.5f)
        //{
        //    westOpen = true;
        //}
    }

    void UpdateWall(bool directionOpen, GameObject wallReference)
    {
        wallReference.SetActive(!directionOpen);
    }
}
