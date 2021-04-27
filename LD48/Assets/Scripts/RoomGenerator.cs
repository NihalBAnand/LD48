using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomGenerator : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room5;
    public GameObject room6;
    public GameObject room7;
    public GameObject room8;
    public GameObject room9;
    public GameObject room10;
    public GameObject room11;
    public GameObject finalRoom;
    public List<GameObject> roomTypes = new List<GameObject>();

    public GameObject player;

    public GameObject monster;
    public List<GameObject> monsters = new List<GameObject>();

    public GameObject artifact;

    private int globalLevel;
    public int roomsGenerated;

    [SerializeField]
    public Vector2Int curRoomPos = new Vector2Int(0, 0);
    
    public Room curRoom;
    string dir = "";

    private void Awake()
    {
        //instantiate list of room prefabs
        roomTypes.Add(room1);
        roomTypes.Add(room2);
        roomTypes.Add(room3);
        roomTypes.Add(room4);
        roomTypes.Add(room5);
        roomTypes.Add(room6);
        roomTypes.Add(room7);
        roomTypes.Add(room8);
        roomTypes.Add(room9);
        roomTypes.Add(room10);
        roomTypes.Add(room11);
        roomTypes.Add(finalRoom);
    }

    // Start is called before the first frame update
    void Start()
    {
        

        

        roomsGenerated = 0;

        //initialize first room
        //rooms.Add(Instantiate(roomTypes[0]));
        //rooms[0].GetComponent<Room>().pos = new Vector2Int(0, 0);

        //monsters.Add(Instantiate(monster));
        //monsters[0].GetComponent<Monster>().level = 3;
        //monsters[0].transform.parent = rooms[0].transform;

        //curRoomPos = rooms[0].GetComponent<Room>().pos;
        //curRoom = rooms[0].GetComponent<Room>();

        globalLevel = 1;

        
    }

    //move into a new room when player walks through a door
    public void UpdateMovement(string direction)
    {
        Debug.Log(direction);
        if (direction == "North" && curRoom.doors.Contains("North"))
        {
            curRoomPos.y++;
            Debug.Log(curRoomPos);
            dir = "South";
        }
        else if (direction == "South" && curRoom.doors.Contains("South"))
        {
            curRoomPos.y--;
            Debug.Log(curRoomPos);
            dir = "North";
        }
        else if (direction == "East" && curRoom.doors.Contains("East"))
        {
            curRoomPos.x++;
            Debug.Log(curRoomPos);
            dir = "West";
        }
        else if (direction == "West" && curRoom.doors.Contains("West"))
        {
            curRoomPos.x--;
            Debug.Log(curRoomPos);
            dir = "East";
        }
    }

    // Update is called once per frame
    void Update()
    {
        globalLevel = GameObject.Find("OP Controller").GetComponent<OPController>().globalLevel;
        //search for existing rooms at the new position -- this assumes UpdateMovement has been called. If not, this does nothing
        bool foundRoom = false;
        foreach (GameObject g in rooms)
        {
            if (g.GetComponent<Room>().pos == curRoomPos)
            {
                g.SetActive(true);
                curRoom = g.GetComponent<Room>();
                foundRoom = true;
            }
            else
            {
                g.SetActive(false);
            }
        }
        //generate a new room if it doesn't exist -- same as above, and don't touch this logic, it works
        if (!foundRoom)
        {
            GameObject temp = null;
            bool validRoomFound = false;
            while (!validRoomFound)
            {
                //generate a random room to try to create
                System.Random rand = new System.Random();
                int roomNum = rand.Next(0, 12);

                bool allGood = true;
                //checks if the random room type has a door to where we're coming from
                if (!roomTypes[roomNum].GetComponent<Room>().doors.Contains(dir))
                {
                    allGood = false;
                }
                if (roomTypes[roomNum].name.Contains("FinalRoom") && roomsGenerated < globalLevel * 4)
                {
                    allGood = false;
                }
                //run validity checks for neighboring rooms -- don't worry about it
                foreach (GameObject room in rooms)
                {
                    if (room.GetComponent<Room>().pos.y + 1 == curRoomPos.y && room.GetComponent<Room>().pos.x == curRoomPos.x)
                    {
                        if (room.GetComponent<Room>().doors.Contains("North") && !roomTypes[roomNum].GetComponent<Room>().doors.Contains("South"))
                        {
                            allGood = false;
                        }
                        if (!room.GetComponent<Room>().doors.Contains("North") && roomTypes[roomNum].GetComponent<Room>().doors.Contains("South"))
                        {
                            allGood = false;
                        }
                    }
                    if (room.GetComponent<Room>().pos.y - 1 == curRoomPos.y && room.GetComponent<Room>().pos.x == curRoomPos.x)
                    {
                        if (room.GetComponent<Room>().doors.Contains("South") && !roomTypes[roomNum].GetComponent<Room>().doors.Contains("North"))
                        {
                            allGood = false;
                        }
                        if (!room.GetComponent<Room>().doors.Contains("South") && roomTypes[roomNum].GetComponent<Room>().doors.Contains("North"))
                        {
                            allGood = false;
                        }
                    }
                    if (room.GetComponent<Room>().pos.y == curRoomPos.y && room.GetComponent<Room>().pos.x + 1 == curRoomPos.x)
                    {
                        if (room.GetComponent<Room>().doors.Contains("East") && !roomTypes[roomNum].GetComponent<Room>().doors.Contains("West"))
                        {
                            allGood = false;
                        }
                        if (!room.GetComponent<Room>().doors.Contains("East") && roomTypes[roomNum].GetComponent<Room>().doors.Contains("West"))
                        {
                            allGood = false;
                        }
                    }
                    if (room.GetComponent<Room>().pos.y == curRoomPos.y && room.GetComponent<Room>().pos.x - 1 == curRoomPos.x)
                    {
                        if (room.GetComponent<Room>().doors.Contains("West") && !roomTypes[roomNum].GetComponent<Room>().doors.Contains("East"))
                        {
                            allGood = false;
                        }
                        if (!room.GetComponent<Room>().doors.Contains("West") && roomTypes[roomNum].GetComponent<Room>().doors.Contains("East"))
                        {
                            allGood = false;
                        }
                    }
                }

                if (allGood)
                {
                    temp = Instantiate(roomTypes[roomNum]);
                    validRoomFound = true;

                    temp.GetComponent<Room>().roomLevel = rand.Next(1, 6) + globalLevel; //randomize room level
                    int numMon = rand.Next(0, (int)temp.GetComponent<Room>().roomLevel / 2); //randomize number of monsters
                    for (int i = 0; i < numMon; i++)
                    {
                        int monNum = rand.Next(0, 1);//still a 50% chance to spawn a monster at all on this pass of the loop
                        if (monNum == 0)
                        {
                            GameObject temp2 = Instantiate(monster);

                            temp2.GetComponent<Monster>().level = rand.Next(globalLevel, temp.GetComponent<Room>().roomLevel);
                            temp2.GetComponent<Monster>().health = temp2.GetComponent<Monster>().level * 50;
                            temp2.transform.parent = temp.transform;
                            monsters.Add(temp2);
                        }
                    }

                    if (GameObject.Find("OP Controller").GetComponent<OPController>().globalLevel == 3)
                    {
                        int artifactNum = rand.Next(0, 51);
                        if (artifactNum == 2)
                        {
                            Instantiate(artifact);
                        }
                    }
                    roomsGenerated++;
                }
            }
            //finish generation
            temp.GetComponent<Room>().pos = curRoomPos;
            temp.transform.localScale = new Vector3(.406f, .355f, 1);
            
            curRoom = temp.GetComponent<Room>();
            rooms.Add(temp);
        }
        dir = "";
    }
}
