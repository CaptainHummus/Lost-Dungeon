﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularRoom : MonoBehaviour
{
    public bool explored = false;
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

    [SerializeField]
    GameObject chestReference = null;


    void Start()
    {
        roomPrefab = (GameObject)Resources.Load("ModularRoom");

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
        if (Random.value < 0.1)
        {
            chestReference.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!explored)
        {
            RevealAdjacentRooms();
            ExploreRoom();
            EventHandler.instance.LoadRandomEvent();
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
                GameManager.instance.AddRoomToList(tempRoom);
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
                GameManager.instance.AddRoomToList(tempRoom);
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
                GameManager.instance.AddRoomToList(tempRoom);
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
                GameManager.instance.AddRoomToList(tempRoom);
            }
        }
    }

    void ExploreRoom()
    {
        litTorch.SetActive(true);
        explored = true;
        GameManager.instance.CheckRooms();
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
                Debug.LogError("Invalid Entrance Direction");
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
    }

    void UpdateWall(bool directionOpen, GameObject wallReference)
    {
        wallReference.SetActive(!directionOpen);
    }
}
