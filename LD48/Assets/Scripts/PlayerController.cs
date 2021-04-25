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

    void Start()
    {
        health = 7;
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
        canStart = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(new List<string>(new string[] { "...AZATHOTH demands a sacrifice...", "...And it shall be YOU!", "Loser lmfao" }));

            if (inRangeEnemy)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    inRangeEnemy.GetComponent<Monster>().health -= 20;
                    Debug.Log("Player Attack \n" + inRangeEnemy.GetComponent<Monster>().health);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && canStart)
        {
            GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(new List<string>(new string[] { "...AZATHOTH demands a trial...", "...A test of will and devotion...", "...We have selected you to undergo it...", "...Walk into the SUMMONING CIRCLE to begin your trial...", "...The mercy of the OLD ONES be upon you." }));

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
        if (collision.gameObject.name.Contains("Cult_Leader"))
        {
            canStart = true;
        }
        else if(collision.gameObject.tag.Equals("Monster"))
        {
            health -= 1;
            Debug.Log("Monster Attack");
            Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Cult_Leader"))
        {
            canStart = false;
        }
    }
}
