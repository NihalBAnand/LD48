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

    public bool paused;
    // Start is called before the first frame update
    void Awake()
    {
        inLevel = false;
        justInLevel = false;
        globalLevel = 1;

        roomGenScript = roomGen.GetComponent<RoomGenerator>();

        paused = false;

        //GameObject.Find("Canvas").SetActive(false);
    }

    private void Start()
    {
        roomGen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
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
}
