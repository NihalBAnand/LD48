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

    public bool targetInRange;

    public string[] itemNames;
    public List<Sprite> itemSprites;
    public GameObject item;
    void Start()
    {
        speed = 2f;

        player = GameObject.Find("Player");
        List<GameObject> rooms = GameObject.Find("Room Generator").GetComponent<RoomGenerator>().rooms;
        gameObject.transform.position = new Vector3(Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.x + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.x - 1), Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.y + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.y - 1));
    }
    

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            System.Random rand = new System.Random();
            int itemNum = rand.Next(0, itemNames.Length);
            GameObject temp = Instantiate(item);
            temp.GetComponent<SpriteRenderer>().sprite = itemSprites[itemNum];
            temp.GetComponent<ItemObj>().itemName = itemNames[itemNum];

            Destroy(gameObject);
        }
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

    private IEnumerator flashColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("hello");
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

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
