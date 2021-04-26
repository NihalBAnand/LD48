using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObj : MonoBehaviour
{
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // if we collide with the player
        {
            bool added = false; // see if there's an open slot in the rings array in player, then join it if there is
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
            if (itemName.ToLower().Contains("ring")) //this only applies if we're a ring
            {
                for (int i = 0; i < p.rings.Length; i++)
                {
                    if (p.rings[i] == null)
                    {
                        p.rings[i] = itemName;
                        p.ringDisp[i].GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; //add ourselves to inventory sprite
                        added = true;
                        break;
                    }
                }
                if (added) GameObject.Destroy(gameObject);// get rid of the in-world version if we're in the player array
            }
            if (itemName.ToLower().Contains("pendant")) // similar deal to the ring, but there's only one slot so we just check that one
            {
                if (p.pendant == "")
                {
                    p.pendant = itemName;
                    p.pendDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    GameObject.Destroy(gameObject);
                }
            }
            if (itemName.Contains("Fang")) //the trend continues down
            {
                if (p.weapon == "")
                {
                    p.weapon = "Atlach-Nacha's Fang";
                    p.weaponDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(gameObject);
                }
            }
            if (itemName.Contains("Mynoghra")) //yep
            {
                if (p.weapon == "")
                {
                    p.weapon = "Mynoghra's Love";
                    p.weaponDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(gameObject);
                }
            }
            if (itemName.Contains("circlet")) //uh-huh
            {
                if (p.circlet == "")
                {
                    p.circlet = itemName;
                    p.weaponDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(gameObject);
                }
            }
            if (itemName.Contains("cloak")) //mm-hmm
            {
                if (p.cloak == "")
                {
                    p.cloak = itemName;
                    p.weaponDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(gameObject);
                }
            }
            if (itemName.Contains("potion")) //yessir
            {
                if (p.potion == "")
                {
                    p.potion = itemName;
                    p.weaponDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(gameObject);
                }
            }
        }
    }
}
