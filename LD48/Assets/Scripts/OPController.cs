using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPController : MonoBehaviour
{
    public bool inLevel;
    public bool justInLevel;
    public int globalLevel;

    public GameObject startingRoom;
    
    public GameObject roomGen;

    RoomGenerator roomGenScript;

    public GameObject confirmQuit;
    public GameObject pauseMenu;
    public GameObject controls;

    public bool paused;
    public bool started;
    // Start is called before the first frame update
    void Awake()
    {
        inLevel = false;
        justInLevel = false;
        globalLevel = 0;

        roomGenScript = roomGen.GetComponent<RoomGenerator>();

        paused = false;

        //GameObject.Find("Canvas").SetActive(false);
    }

    private void Start()
    {
        roomGen.SetActive(false);
        startingRoom.SetActive(false);
        pauseMenu.SetActive(false);
        confirmQuit.SetActive(false);
        controls.SetActive(false);
        Time.timeScale = 0;
        GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(new List<string>(new string[] { "I’m an agent of the Bureau, have been for five years now. My mission this time is to infiltrate a potentially dangerous \"religious terrorist organization.\"", "What that really means is that I gotta bust a cult.", "That’s the thing about this job, you never know what’s next.", "Anyway, I gotta earn their trust and get to the bottom of what they’re doing.", "HINT: press ‘escape’ to bring up a pause menu.", "Press ‘enter’ to continue." }));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //pause
        {
            paused = !paused;
        }
        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        if (!started && Input.GetKeyDown(KeyCode.Return)) //end intro and start game
        {
            started = true;
            Time.timeScale = 1;
            GameObject.Find("UIController").GetComponent<UIController>().curPage = 1000; //alows to skip intro
            GameObject.Find("Image").SetActive(false); //removes black background
            exitLevel();
        }
    }

    public void enterLevel()
    {
        startingRoom.SetActive(false);
        roomGen.GetComponent<RoomGenerator>().roomsGenerated = 0;
        roomGen.GetComponent<RoomGenerator>().rooms.Clear();
        roomGen.GetComponent<RoomGenerator>().monsters.Clear();
        

        roomGenScript.rooms.Add(Instantiate(roomGenScript.roomTypes[0]));
        Debug.Log("hello");
        roomGenScript.rooms[0].GetComponent<Room>().pos = new Vector2Int(0, 0);

        roomGenScript.curRoomPos = roomGenScript.rooms[0].GetComponent<Room>().pos;
        roomGenScript.curRoom = roomGenScript.rooms[0].GetComponent<Room>();
        roomGenScript.rooms[0].transform.localScale = new Vector3(1.2f, 1.2f, 1);
        roomGen.SetActive(true);
        
        
    }

    public void exitLevel()
    {
        roomGen.SetActive(false);
        roomGen.GetComponent<RoomGenerator>().roomsGenerated = 0;
        roomGen.GetComponent<RoomGenerator>().rooms.Clear();
        roomGen.GetComponent<RoomGenerator>().monsters.Clear();
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Room"));
        startingRoom.SetActive(true);
        globalLevel++;
    }

    public void Resume()
    {
        paused = false;
    }

    public void Controls()
    {
        if (controls.activeInHierarchy)
        {
            controls.SetActive(false);
        }
        else
        {
            controls.SetActive(true);
        }
    }

    public void Quit()
    {
        confirmQuit.SetActive(true);
    }

    public void NoQuit()
    {
        confirmQuit.SetActive(false);
    }

    public void ConfirmQuit()
    {
        Application.Quit();
    }
}
