using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int health;

    Rigidbody2D rigidbody;
    
    public GameObject inRangeEnemy;

    public List<Sprite> healthSprites;

    public bool canStart;
    public bool canTalk;
    public List<string> cultText;

    public bool canFinish;

    public bool dispInventory;
    public GameObject inventoryObj;
    public GameObject tooltip;

    //public List<Sprite> ringSprites;
    public List<GameObject> ringDisp;
    public string[] rings;

    //public List<Sprite> pendantSprites;
    public GameObject pendDisp;
    public string pendant;

    public string facing;
    public Animator anim;

    public GameObject opcont;

    public GameObject sword;
    public bool attacking;
    public string weapon;
    public GameObject weaponDisp;
    public Sprite knife;
    public Sprite swSprite;
    public GameObject knifePre;

    public GameObject cloakDisp;
    public string cloak;

    public GameObject circletDisp;
    public string circlet;

    public GameObject potionDisp;
    public string potion;

    public GameObject circletCooldownDisp;
    public GameObject cloakCooldownDisp;
    public GameObject potionCooldownDisp;
    public bool onCircletCooldown = false;
    public bool onPotionCooldown = false;
    public bool onCloakCooldown = false;

    public int monstersKilled = 0;
    public bool gotArtifact = false;
    public bool cutHand = false;
    public bool killedPerson = false;
    
    void Start()
    {
        health = 7;
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
        canStart = false;

        circletCooldownDisp.SetActive(false);
        cloakCooldownDisp.SetActive(false);
        potionCooldownDisp.SetActive(false);

        dispInventory = false;

        rings = new string[3];

        facing = "Down";
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (!opcont.GetComponent<OPController>().paused) //pause updates if game is paused
        {
            if (Input.GetKeyDown(KeyCode.I)) //toggle inventory
            {
                dispInventory = !dispInventory;
            }

            inventoryObj.SetActive(dispInventory); 
            if (!dispInventory) //turn off tooltip if inventory is closed
            {
                tooltip.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && canTalk) //talk to (technically) any cultist
            {
                GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(cultText);
                if (opcont.GetComponent<OPController>().globalLevel == 1) //unlock summoning circle if we talk to cultist
                {
                    canStart = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && circlet != "" && !onCircletCooldown) //activate circlet ability
            {
                List<GameObject> rooms = GameObject.Find("Room Generator").GetComponent<RoomGenerator>().rooms; //teleport to random position
                gameObject.transform.position = new Vector3(Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.x + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.x - 1), Random.Range(rooms[0].GetComponent<SpriteRenderer>().bounds.min.y + 1, rooms[0].GetComponent<SpriteRenderer>().bounds.max.y - 1));
                onCircletCooldown = true;
                StartCoroutine(circletCooldown(5));//start cooldown 5 secs long
            }

            if (Input.GetKeyDown(KeyCode.Q) && cloak != "" && !onCloakCooldown) //activate cloak ability
            {
                foreach(GameObject monster in GameObject.Find("Room Generator").GetComponent<RoomGenerator>().monsters)
                {
                    monster.GetComponent<Monster>().targetDist = 0; //set it so monsters can't see anything
                }
                onCloakCooldown = true; //start cloak cooldown 15 secs long
                StartCoroutine(cloakCooldown(15));
                StartCoroutine(cloakEffect(5)); //start cloak effect 5 secs long
            }

            if (Input.GetKeyDown(KeyCode.X) && potion != "" && !onPotionCooldown) //activate potion ability
            {
                foreach (GameObject monster in GameObject.Find("Room Generator").GetComponent<RoomGenerator>().monsters)
                {
                    Destroy(monster); //destroy all monsters that have been spawned
                }
                onPotionCooldown = true; //start cooldown 20 secs long
                StartCoroutine(potionCooldown(20));
            }

            if (Input.GetMouseButtonDown(0))
            {
                attack(); //attack on click
            }

            if (health > 0) //update health bar
                GameObject.Find("HealthBar").GetComponent<Image>().sprite = healthSprites[health - 1];
            else
                Debug.Log("Player died");

            //get facing direciton and play anims
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                facing = "Left";
                anim.Play("Player_Left");
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                facing = "Right";
                anim.Play("Player_Right");
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                facing = "Up";
                anim.Play("Player_Up");
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                facing = "Down";
                anim.Play("Player_Down");
            }
            else
            {
                anim.Play("Player_Idle_" + facing);
            }
        }

        switch (opcont.GetComponent<OPController>().globalLevel)
        {
            case 1:
                canFinish = true;
                break;
            case 2:
                if (monstersKilled >= 25)
                {
                    canFinish = true;
                }
                else
                {
                    canFinish = false;
                }
                break;
            case 3:
                canFinish = gotArtifact;
                break;
            case 4:
                canFinish = cutHand;
                break;
            case 5:
                canFinish = killedPerson;
                break;
        }
    }

    void FixedUpdate()
    {
        if (!opcont.GetComponent<OPController>().paused) //movement
        {
            //Store user input as a movement vector
            Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            //Apply the movement vector to the current position, which is
            //multiplied by deltaTime and speed for a smooth MovePosition
            rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Cult")) //detect collision with cult member
        {
            canTalk = true;
            cultText = collision.gameObject.GetComponent<Cultist>().text;
        }
        else if(collision.gameObject.tag.Equals("Monster")) //detect collision with monster
        {
            health -= 1;
            StartCoroutine(flashColor());
            Debug.Log("Monster Attack");
            Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Cult"))
        {
            canTalk = false;
        }
    }
    private IEnumerator flashColor() //flash healthbar color, used when taking damage
    {
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 105, 105, 255);
        yield return new WaitForSecondsRealtime(.2f);
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    void attack()
    {
        if (weapon.Contains("Love")) //sword
        {
            if (GameObject.Find("sword(Clone)") == null)
            {
                GameObject newSword = Instantiate(sword);

                newSword.transform.parent = gameObject.transform;
                if (newSword.activeSelf)
                {
                    newSword.GetComponent<Animator>().Play("sword");
                }

                GameObject.Destroy(newSword, newSword.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            }
        }
        else if (weapon.Contains("Fang")) //sword
        {
            if (GameObject.Find("knife(Clone)") == null)
            {
                GameObject newKnife = Instantiate(knifePre);

                newKnife.transform.parent = gameObject.transform;
                if (newKnife.activeSelf)
                {
                    newKnife.GetComponent<Animator>().Play("knife");
                }

                GameObject.Destroy(newKnife, newKnife.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            }
        }
    }


    //cooldowns -- all the same just diff variable names
    private IEnumerator circletCooldown(int len)
    {
        circletCooldownDisp.GetComponent<Text>().text = name + "Circlet Cooldown Time Remaining: " + len; //set inital text
        circletCooldownDisp.SetActive(true);
        for (int i = 0; i <= len; i++) //wait seconds and update text while we're still waiting for cooldown
        {
            circletCooldownDisp.GetComponent<Text>().text = name + "Circlet Cooldown Time Remaining: " + (len - i);
            yield return new WaitForSeconds(1);
        }
        circletCooldownDisp.SetActive(false);//clean up
        onCircletCooldown = false;
    }

    private IEnumerator cloakCooldown(int len)
    {
        cloakCooldownDisp.GetComponent<Text>().text = name + "Cloak Cooldown Time Remaining: " + len;
        cloakCooldownDisp.SetActive(true);
        for (int i = 0; i <= len; i++)
        {
            circletCooldownDisp.GetComponent<Text>().text = name + "Cloak Cooldown Time Remaining: " + (len - i);
            yield return new WaitForSeconds(1);
        }
        cloakCooldownDisp.SetActive(false);
        onCloakCooldown = false;
    }

    private IEnumerator potionCooldown(int len)
    {
        potionCooldownDisp.GetComponent<Text>().text = name + "Potion Cooldown Time Remaining: " + len;
        potionCooldownDisp.SetActive(true);
        for (int i = 0; i <= len; i++)
        {
            circletCooldownDisp.GetComponent<Text>().text = name + "Potion Cooldown Time Remaining: " + (len - i);
            yield return new WaitForSeconds(1);
        }
        potionCooldownDisp.SetActive(false);
        onPotionCooldown = false;
    }

    private IEnumerator cloakEffect(int len)
    {
        yield return new WaitForSeconds(len);
        foreach (GameObject monster in GameObject.Find("Room Generator").GetComponent<RoomGenerator>().monsters)
        {
            monster.GetComponent<Monster>().targetDist = 3; //give monsters sight back after cloak effect
        }
    }
}
