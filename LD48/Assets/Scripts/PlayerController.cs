using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int health;

    Rigidbody2D rigidbody;
    
    public GameObject inRangeEnemy;

    public List<Sprite> healthSprites;

    public bool canStart;
    public List<string> cultText;

    public bool dispInventory;
    public GameObject inventoryObj;

    //public List<Sprite> ringSprites;
    public List<GameObject> ringDisp;
    public string[] rings;

    //public List<Sprite> pendantSprites;
    public GameObject pendDisp;
    public string pendant;

    void Start()
    {
        health = 7;
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
        canStart = false;

        dispInventory = false;

        rings = new string[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dispInventory = !dispInventory;
        }

        inventoryObj.SetActive(dispInventory);

        if (Input.GetKeyDown(KeyCode.Return) && canStart)
        {
            GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(cultText);

        }

        if (health > 0)
            GameObject.Find("HealthBar").GetComponent<Image>().sprite = healthSprites[health - 1];
        else
            Debug.Log("Player died");
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Cult"))
        {
            canStart = true;
            cultText = collision.gameObject.GetComponent<Cultist>().text;
        }
        else if(collision.gameObject.tag.Equals("Monster"))
        {
            health -= 1;
            StartCoroutine(flashColor());
            Debug.Log("Monster Attack");
            Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Cult"))
        {
            canStart = false;
        }
    }
}
