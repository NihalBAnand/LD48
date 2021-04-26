﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] Images;
    
    public SpriteRenderer spriteRenderer;

    public GameObject player;

    public int level;
    public float health;

    public float speed;
    
    public Vector2 target;

    public bool targetInRange;
    void Start()
    {
        health = 100f;
        speed = 2f;

        player = GameObject.Find("Player");
    }
    

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) Destroy(gameObject);
        switch (level)
        {
            case 1:
                gameObject.GetComponent<Animator>().Play("Hoggoth");
                break;
            default:
                gameObject.GetComponent<Animator>().Play("Hoggoth");
                break;
        }
       
        if (gameObject.transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 2) targetInRange = true;
        
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 10f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name.Contains("Player"))
            {
                Debug.Log("Noticed Player!!");
            }
        }
        if (targetInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.tag.Equals("Player")){
            target = collision.transform.position;
            targetInRange = true;
            //collision.gameObject.GetComponent<PlayerController>().
            
        }
        *//*Debug.Log("Monster Attack");
        Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);*//*
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag.Equals("Player"))
        {
            targetInRange = false;
        }
    }*/
    
}
