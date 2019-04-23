using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularRoom : MonoBehaviour
{
    private int numberOfDoors;
    [SerializeField]
    private bool explored = false;
    [SerializeField]
    GameObject litTorch = null;

    private Camera mainCamera;

    [SerializeField]
    GameObject roomPrefab = null;
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
        mainCamera = Camera.main;
        RandomizeOpenings();


        UpdateWall(northOpen, northWallReference);
        UpdateWall(eastOpen, eastWallReference);
        UpdateWall(southOpen, southWallReference);
        UpdateWall(westOpen, westWallReference);
    }

    void Update()
    {
        
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
            //Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1), Color.green, 10f);
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("NOTHING THERE ");
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x,
                                                               transform.position.y + 1,
                                                               transform.position.z)
                                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('S');

            }
        }
        if (eastOpen)
        {
            //Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1), Color.green, 10f);
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("NOTHING THERE ");
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x + 1,
                                                               transform.position.y,
                                                               transform.position.z)
                                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('W');
            }
        }
        if (southOpen)
        {
            //Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 1), Color.green, 10f);
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("NOTHING THERE ");
                tempRoom = Instantiate(roomPrefab, new Vector3(transform.position.x,
                                                               transform.position.y - 1,
                                                               transform.position.z)
                                                               , transform.rotation);
                tempRoom.GetComponent<ModularRoom>().NewTile('N');
            }
        }
        if (westOpen)
        {
            //Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 1), Color.green, 10f);
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("NOTHING THERE ");
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
        RandomizeOpenings();
        UpdateWall(northOpen, northWallReference);
        UpdateWall(eastOpen, eastWallReference);
        UpdateWall(southOpen, southWallReference);
        UpdateWall(westOpen, westWallReference);
    }

    void RandomizeOpenings()
    {
        if (!northOpen && UnityEngine.Random.value > 0.5f)
        {
            northOpen = true;
        }
        if (!eastOpen && UnityEngine.Random.value > 0.5f)
        {
            eastOpen = true;
        }
        if (!southOpen && UnityEngine.Random.value > 0.5f)
        {
            southOpen = true;
        }
        if (!westOpen && UnityEngine.Random.value > 0.5f)
        {
            westOpen = true;
        }
    }

    void UpdateWall(bool directionOpen, GameObject wallReference)
    {
        if (directionOpen)
        {
            wallReference.SetActive(false);
        }
        else
        {
            wallReference.SetActive(true);
        }
    }
}
