using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int pos;
    public List<string> doors = new List<string>(); // list of doors in room by direction
    public int roomLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Don't touch, this works
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            if (System.Math.Abs(collision.gameObject.transform.position.x) > System.Math.Abs(collision.gameObject.transform.position.y)) {
                if (collision.gameObject.transform.position.x > 0)
                {
                    GameObject.FindGameObjectWithTag("roomgen").GetComponent<RoomGenerator>().UpdateMovement("East");
                    collision.gameObject.transform.position = new Vector3(-6, 0, 0);
                }
                if (collision.gameObject.transform.position.x < 0)
                {
                    GameObject.FindGameObjectWithTag("roomgen").GetComponent<RoomGenerator>().UpdateMovement("West");
                    collision.gameObject.transform.position = new Vector3(6, 0, 0);
                }
            }
            if (System.Math.Abs(collision.gameObject.transform.position.x) < System.Math.Abs(collision.gameObject.transform.position.y))
            {
                if (collision.gameObject.transform.position.y > 0)
                {
                    GameObject.FindGameObjectWithTag("roomgen").GetComponent<RoomGenerator>().UpdateMovement("North");
                    collision.gameObject.transform.position = new Vector3(0, -3, 0);
                }
                if (collision.gameObject.transform.position.y < 0)
                {
                    GameObject.FindGameObjectWithTag("roomgen").GetComponent<RoomGenerator>().UpdateMovement("South");
                    collision.gameObject.transform.position = new Vector3(0, 3, 0);
                }
            }
        }
    }
}
