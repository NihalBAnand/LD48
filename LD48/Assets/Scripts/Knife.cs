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
        switch (transform.parent.GetComponent<PlayerController>().facing)
        {
            case "Down":
                direction = new Vector3(0, -3f) + transform.parent.transform.position;
                transform.position = new Vector3(0, -1) + transform.parent.transform.position;
                transform.eulerAngles = new Vector3(0, 0, 90);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                break;
            case "Up":
                direction = new Vector3(0, 3f) + transform.parent.transform.position;
                transform.position = new Vector3(0, 1) + transform.parent.transform.position;
                transform.eulerAngles = new Vector3(0, 0, 270);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Left":
                direction = new Vector3(-3f, 0f) + transform.parent.transform.position;
                transform.position = new Vector3(-1, 0) + transform.parent.transform.position;
                transform.eulerAngles = new Vector3(0, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
            case "Right":
                direction = new Vector3(3f, 0) + transform.parent.transform.position;
                transform.position = new Vector3(1, 0) + transform.parent.transform.position;
                transform.localScale = new Vector3(-1, 1);
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                break;
        }
        //StartCoroutine(createHitbox());
    }

    // Update is called once per frame
    void Update()
    {
        switch (transform.parent.GetComponent<PlayerController>().facing)
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
            Debug.Log("HIT");
        }
    }

    IEnumerator createHitbox()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
