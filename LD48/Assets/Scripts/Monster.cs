using System.Collections;
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

    public bool frozen;
    public float freezeDuration;

    public bool targetInRange;
    public float targetDist;

    public string[] itemNames;
    public List<Sprite> itemSprites;
    public GameObject item;
    void Start()
    {
        speed = 2f;
        targetDist = 3;

        player = GameObject.Find("Player");
        List<GameObject> rooms = GameObject.Find("Room Generator").GetComponent<RoomGenerator>().rooms; //randomize position within room
        gameObject.transform.position = new Vector3(Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.x + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.x - 1), Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.y + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.y - 1));
    }
    

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) //death stuff
        {
            System.Random rand = new System.Random();
            int genItem = rand.Next(0, 4);
            if (genItem == 3) //33% chance to drop an item
            {
                bool validFound = false;
                int itemNum = 0;
                int genPend = rand.Next(0, 4 - level); //33% chance to be allowed to generate a pendant
                int genEpic = rand.Next(0, 13 - level); //8.3% chance to be allowed to generate an epic item (cloak, circlet, potion)
                while (!validFound) //keeps randomizing until it finds a good item
                {
                    itemNum = rand.Next(0, itemNames.Length);
                    //if the level is too low to spawn a greater item, this isn't a valid spawn
                    if (itemNames[itemNum].Contains("Greater") && level < 3)
                    {
                        continue;
                    }
                    //if we aren't allowed to generate an epic, or the level is too low, this isn't a valid spawn either
                    else if (genEpic != 1 && (itemNames[itemNum].Contains("cloak") || itemNames[itemNum].Contains("circlet") || itemNames[itemNum].Contains("potion")) && level < 5)
                    {
                        continue;
                    }
                    //if we aren't allowed to generate a pendant, this isn't a valid spawn either
                    else if (genPend != 1 && itemNames[itemNum].Contains("pendant"))
                    {
                        continue;
                    }
                    //if we're all good, this is a valid spawn
                    else
                    {
                        validFound = true;
                    }
                }
                //instantiate the item
                GameObject temp = Instantiate(item);
                temp.transform.position = gameObject.transform.position;
                temp.GetComponent<SpriteRenderer>().sprite = itemSprites[itemNum];
                temp.GetComponent<ItemObj>().itemName = itemNames[itemNum];

            }
            //destroy ourselves
            Destroy(gameObject);
        }
        //determine look based on level
        switch (level)
        {
            case 1:
                gameObject.GetComponent<Animator>().Play("Hoggoth");
                break;
            default:
                gameObject.GetComponent<Animator>().Play("Hoggoth");
                break;
        }

        //flip to face the player if we have detected them
        if (targetInRange)
        {
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }

        //check if we're in range of player
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < targetDist) targetInRange = true;
        
        //this is for the vines effect from the ring
        if (frozen)
        {
            StartCoroutine(freeze(freezeDuration));
            frozen = false;
        }
    }

    private void FixedUpdate()
    {
        //START - this is obselete code, but it's not doing harm so eh, let's keep it here
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 10f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name.Contains("Player"))
            {
                Debug.Log("Noticed Player!!");
            }
        }
        //END
        //if player's in range, move towards em
        if (targetInRange)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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

    //flash colors when hurt
    private IEnumerator flashColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("hello");
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //for vines effect
    private IEnumerator freeze(float d)
    {
        yield return new WaitForSeconds(d);
    }

    //flash color if we get hit by a knife or a sword 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.name.Contains("knife") || collision.collider.gameObject.name.Contains("sword"))
        {
            StartCoroutine(flashColor());
            Debug.Log("Hit" + collision.collider.gameObject.name);
        }
    }

}
