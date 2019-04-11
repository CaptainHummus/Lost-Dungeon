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
            Debug.Log("Player has entered");
            explored = true;
            Instantiate(explorationMarker, transform.position, transform.rotation);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.position.x > transform.position.x + 0.4)
        {
            Debug.DrawLine(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1), Color.green, 10f);
            hit = Physics2D.Linecast(mainCamera.transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1));
            if (hit)
            {
                Debug.Log("HIT SOMETHING");
                Debug.Log(hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("NOTHING THERE");
                Instantiate(roomTile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);

            }
            //hit = Physics2D.Raycast(transform.position, transform.right, 10f);
            //Debug.Log(hit.transform.name);
            //Debug.DrawLine(transform.position, transform.right, Color.green, 10f);

            //if (Physics2D.Raycast(player.transform.position, player.transform.right, 10f))
            //{
            //    Debug.DrawLine(player.transform.position, player.transform.right, Color.green, 10f);
            //    Debug.Log("Hit something");
            //    Instantiate(roomTile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            //}
            //else
            //{
            //    Instantiate(roomTile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            //}

        }
        else if (collision.transform.position.x < transform.position.x - 0.4)
        {
            Debug.Log("Generate Tile West");
            Instantiate(roomTile, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation);
        }
        else if (collision.transform.position.y < transform.position.y - 0.4)
        {
            Debug.Log("Generate Tile South");
            Instantiate(roomTile, new Vector3(transform.position.x , transform.position.y - 1, transform.position.z), transform.rotation);
        }
        else if (collision.transform.position.y > transform.position.y + 0.4)
        {
            Debug.Log("Generate Tile North");
            Instantiate(roomTile, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        }
    }
}
