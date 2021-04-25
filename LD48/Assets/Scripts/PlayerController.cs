using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float health;

    Rigidbody2D rigidbody;
    
    public GameObject inRangeEnemy;
    
    void Start()
    {
        health = 100f;
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make player movement independent of resolution
        if (inRangeEnemy)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inRangeEnemy.GetComponent<Monster>().health -= 20;
                Debug.Log("Player Attack \n" + inRangeEnemy.GetComponent<Monster>().health);
            }
        }
    }

    private void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))
        {
            inRangeEnemy = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))
        {
            inRangeEnemy = null;
        }
    }
}
