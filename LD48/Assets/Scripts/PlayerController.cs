using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    void Start()
    {
        health = 7;
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
        canStart = false;

        dispInventory = false;

        rings = new string[3];

        facing = "Down";
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!opcont.GetComponent<OPController>().paused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dispInventory = !dispInventory;
            }

            inventoryObj.SetActive(dispInventory);
            if (!dispInventory)
            {
                tooltip.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && canTalk)
            {
                GameObject.Find("UIController").GetComponent<UIController>().CreateTextbox(cultText);
                if (opcont.GetComponent<OPController>().globalLevel == 1)
                {
                    weapon = "Fang of Atlach-Nacha";
                    weaponDisp.GetComponent<Image>().sprite = knife;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                attack();
            }

            if (health > 0)
                GameObject.Find("HealthBar").GetComponent<Image>().sprite = healthSprites[health - 1];
            else
                Debug.Log("Player died");

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
    }

    void FixedUpdate()
    {
        if (!opcont.GetComponent<OPController>().paused)
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
        if (collision.gameObject.name.Contains("Cult"))
        {
            canTalk = true;
            cultText = collision.gameObject.GetComponent<Cultist>().text;
        }
        else if(collision.gameObject.tag.Equals("Monster"))
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
    private IEnumerator flashColor()
    {
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 105, 105, 255);
        yield return new WaitForSecondsRealtime(.2f);
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    void attack()
    {
        if (weapon == "sword")
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
        else if (weapon.Contains("Fang"))
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
}
