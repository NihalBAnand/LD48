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
        if (collision.gameObject.tag == "Player")
        {
            bool added = false;
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
            if (itemName.ToLower().Contains("ring"))
            {
                for (int i = 0; i < p.rings.Length; i++)
                {
                    if (p.rings[i] == null)
                    {
                        p.rings[i] = itemName;
                        p.ringDisp[i].GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                        added = true;
                        break;
                    }
                }
                if (added) GameObject.Destroy(gameObject);
            }
            if (itemName.ToLower().Contains("pendant"))
            {
                if (p.pendant == "")
                {
                    p.pendant = itemName;
                    p.pendDisp.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
