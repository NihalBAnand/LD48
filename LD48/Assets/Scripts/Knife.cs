using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        switch (transform.parent.GetComponent<PlayerController>().facing) //initialize according to direction
        {
            case "Down":
                direction = new Vector3(0, -3f) + transform.parent.transform.position; //ending position
                transform.position = new Vector3(0, -1f) + transform.parent.transform.position; //starting position
                transform.eulerAngles = new Vector3(0, 0, 90); //rotation
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                break;
            case "Up":
                direction = new Vector3(0, 3f) + transform.parent.transform.position;
                transform.position = new Vector3(0, .5f) + transform.parent.transform.position;
                transform.eulerAngles = new Vector3(0, 0, 270);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Left":
                direction = new Vector3(-3f, 0f) + transform.parent.transform.position;
                transform.position = new Vector3(-.5f, 0) + transform.parent.transform.position;
                transform.eulerAngles = new Vector3(0, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Right":
                direction = new Vector3(3f, 0) + transform.parent.transform.position;
                transform.position = new Vector3(.5f, 0) + transform.parent.transform.position;
                transform.localScale = new Vector3(-1, 1);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
        }
        //StartCoroutine(createHitbox());
    }

    // Update is called once per frame
    void Update()
    {
        switch (transform.parent.GetComponent<PlayerController>().facing) //technically obselete but still here cause lazy, just move to the ending point
        {
            case "Down":
                gameObject.transform.position = Vector2.MoveTowards(transform.position, direction, 7f * Time.deltaTime);

                break;
            case "Up":
                gameObject.transform.position = Vector2.MoveTowards(transform.position, direction, 7f * Time.deltaTime);

                break;
            case "Left":
                gameObject.transform.position = Vector2.MoveTowards(transform.position, direction, 7f * Time.deltaTime);

                break;
            case "Right":
                gameObject.transform.position = Vector2.MoveTowards(transform.position, direction, 7f * Time.deltaTime);

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Monster"))
        {
            collision.collider.gameObject.GetComponent<Monster>().health -= 50;
            //StartCoroutine(collision.collider.gameObject.GetComponent<Monster>().flashColor());
            Debug.Log("HIT");

            foreach (string s in transform.parent.GetComponent<PlayerController>().rings) //see if we have rings and add bonuses accordingly
            {
                switch (s)
                {
                    case "Lesser Hnarqu’s Tendril (ring)":
                        collision.collider.gameObject.GetComponent<Monster>().health -= 25;
                        break;
                    case "Greater Hnarqu’s Tendril (ring)":
                        collision.collider.gameObject.GetComponent<Monster>().health -= 50;
                        break;
                    case "Lesser Han’s Claw (ring)":
                        collision.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction / 2);
                        break;
                    case "Greater Han’s Claw (ring)":
                        collision.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
                        break;
                    case "Lesser Ei’lor’s Vine (ring)":
                        collision.collider.gameObject.GetComponent<Monster>().freezeDuration = 2.5f;
                        collision.collider.gameObject.GetComponent<Monster>().frozen = true;
                        break;
                    case "Greater Ei’lor’s Vine (ring)":
                        collision.collider.gameObject.GetComponent<Monster>().freezeDuration = 5f;
                        collision.collider.gameObject.GetComponent<Monster>().frozen = true;
                        break;
                }
            }

            switch (transform.parent.GetComponent<PlayerController>().pendant) //see if we have pendants and add bonuses accordingly
            {
                case "Lesser B’gnu-Thun’s Eye (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 25;
                    break;
                case "Greater B’gnu-Thun’s Eye (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 50;
                    break;
                case "Lesser Yomagn’tho’s Core (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 25;
                    break;
                case "Greater Yomagn’tho’s Core (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 50;
                    break;
                case "Lesser Istasha’s Heart (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 25;
                    break;
                case "Greater Istasha’s Heart (pendant)":
                    collision.collider.gameObject.GetComponent<Monster>().health -= 50;
                    break;
            }

            GameObject.Destroy(gameObject);
        }
        if (collision.collider.gameObject.name.Contains("Sacrifice"))
        {
            transform.parent.GetComponent<PlayerController>().killedPerson = true;
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
        
    }

    
}
