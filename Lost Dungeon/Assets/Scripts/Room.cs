using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool explored = false;
    public GameObject explorationMarker;
    //public GameObject unwantedMarker;
    public GameObject roomTile;
    public GameObject player;
    private RaycastHit2D hit;
    private Camera mainCamera;
    public GameObject enemy;

    private void Start()
    {
        mainCamera = Camera.main;
        roomTile = GameObject.Find("Room Template");
        player = GameObject.Find("Player");
        // For parented explorationmarker
        //unwantedMarker = transform.Find("ExplorationMarker(Clone)").gameObject;
        //if (unwantedMarker != null)
        //{
        //    Destroy(unwantedMarker);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && explored == false)
        {

            if (Random.value <= 0.5f)
            {
                Debug.Log("BIG MONEYS");
                player.GetComponent<Player>().gold += Random.Range(10, 200);
            }
            else
            {
                Debug.Log("ENEMY ENCOUNTER");
                //Instantiate(combatUIPrefab, GameObject.Find("Canvas").transform);
                Instantiate(enemy, transform);
            }

            explored = true;
            Instantiate(explorationMarker, transform.position, transform.rotation);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.position.x > transform.position.x + 0.4)
        {
            //Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1), Color.green, 10f);
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("NOTHING THERE ");
                Instantiate(roomTile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            }
        }
        else if (collision.transform.position.x < transform.position.x - 0.4)
        {
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("Generate Tile West");
                Instantiate(roomTile, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation);
            }
        }
        else if (collision.transform.position.y < transform.position.y - 0.4)
        {
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("Generate Tile South");
                Instantiate(roomTile, new Vector3(transform.position.x , transform.position.y - 1, transform.position.z), transform.rotation);
            }
        }
        else if (collision.transform.position.y > transform.position.y + 0.4)
        {
            if (Physics.Linecast(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1)))
            {
                Debug.Log("HIT SOMETHING");
            }
            else
            {
                Debug.Log("Generate Tile North");
                Instantiate(roomTile, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            }
        }
    }
}
