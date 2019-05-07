using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField]
    private Event debugEvent = null;

    [SerializeField]
    private bool eventDebugMode = false;

    [SerializeField]
    private List<ModularRoom> roomList = new List<ModularRoom>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            //LoadScene("Win Scene");
            ClearRooms();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else if (Input.GetKeyDown("c"))
        {
            if (eventDebugMode)
            {
                EventHandler.instance.OverrideEvent(debugEvent);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddRoomToList(GameObject room)
    {
        roomList.Add(room.GetComponent<ModularRoom>());
    }

    public void ClearRooms()
    {
        roomList = new List<ModularRoom>();
    }

    public void CheckRooms()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            if (!roomList[i].explored)
            {
                return;
            }
        }

        Debug.Log("ALL ROOMS ARE EXPLORED");
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i].gameObject);
        }
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 0, 0);
        // TODO Add event for teleportation

    }

}
