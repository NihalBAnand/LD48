using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //do the combat stoof here, GL
    }
}
