using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This whole class is basically the same as the knife, so just refer to that
/// </summary>

public class Sword : MonoBehaviour
{
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        switch (transform.parent.GetComponent<PlayerController>().facing)
        {
            case "Down":
                direction = new Vector2(0, -1);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                break;
            case "Up":
                direction = new Vector2(0, 1);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Left":
                direction = new Vector2(-1, -.25f);
                gameObject.transform.localScale = new Vector2(-1, 1);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Right":
                direction = new Vector2(1, 0);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
        }
        StartCoroutine(createHitbox());
    }

    // Update is called once per frame
    void Update()
    {
        switch (transform.parent.GetComponent<PlayerController>().facing)
        {
            case "Down":
                gameObject.transform.position = direction + (Vector2)transform.parent.position;
                
                break;
            case "Up":
                gameObject.transform.position = direction + (Vector2)transform.parent.position;
                
                break;
            case "Left":
                gameObject.transform.position = direction + (Vector2)transform.parent.position;
                
                break;
            case "Right":
                gameObject.transform.position = direction + (Vector2)transform.parent.position;
                
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Monster"))
        {
            collision.collider.gameObject.GetComponent<Monster>().health -= 75;
            //StartCoroutine(collision.collider.gameObject.GetComponent<Monster>().flashColor());
            Debug.Log("HIT");

            foreach (string s in transform.parent.GetComponent<PlayerController>().rings)
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

            switch (transform.parent.GetComponent<PlayerController>().pendant)
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
        }
    }

    IEnumerator createHitbox()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
 
}
