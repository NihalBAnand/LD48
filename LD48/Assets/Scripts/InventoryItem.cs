using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public string itemDesc;

    public GameObject player;
    public GameObject tooltip;
    public GameObject canvas;
    public GameObject descWin;
    public Sprite silhouette;

    public bool isOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        tooltip.SetActive(true); //init tooltip
        tooltip.transform.position = new Vector3(gameObject.transform.position.x + (gameObject.GetComponent<RectTransform>().rect.width * canvas.GetComponent<Canvas>().scaleFactor * 1.3f), gameObject.transform.position.y + (.75f * tooltip.GetComponent<RectTransform>().rect.height * canvas.GetComponent<Canvas>().scaleFactor));
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        tooltip.SetActive(false); //remove tooltip
    }

    void Awake()
    {
        player = GameObject.Find("Player");
        tooltip = GameObject.Find("Tooltip");
        canvas = GameObject.Find("Canvas");
        descWin = GameObject.Find("Descriptions");
    }
    // Start is called before the first frame update
    void Start()
    {
        descWin.SetActive(false);
        tooltip.SetActive(false);
        isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("ring")) // if we're a ring, access the name from player class
        {
            if (player.GetComponent<PlayerController>().rings[System.Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1))] != null)
            {
                itemName = player.GetComponent<PlayerController>().rings[System.Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1))];
            }
            else
            {
                itemName = "No ring equipped";
            }
        }
        else if (gameObject.name.Contains("pendant")) //same thing with pendant and every other item, just done a little different
        {
            if (player.GetComponent<PlayerController>().pendant != "")
            {
                itemName = player.GetComponent<PlayerController>().pendant;
            }
            else
            {
                itemName = "No pendant equipped";
            }
        }
        else if (gameObject.name.Contains("weapon")) // same
        {
            if (player.GetComponent<PlayerController>().weapon != "")
            {
                itemName = player.GetComponent<PlayerController>().weapon;
            }
            else
            {
                itemName = "No weapon equipped";
            }
        }
        else if (gameObject.name.Contains("cloak")) // same
        {
            if (player.GetComponent<PlayerController>().cloak != "")
            {
                itemName = player.GetComponent<PlayerController>().cloak;
            }
            else
            {
                itemName = "No cloak equipped";
            }
        }
        else if (gameObject.name.Contains("circlet")) // same
        {
            if (player.GetComponent<PlayerController>().circlet != "")
            {
                itemName = player.GetComponent<PlayerController>().circlet;
            }
            else
            {
                itemName = "No cloak equipped";
            }
        } 
        else if (gameObject.name.Contains("potion")) // same
        {
            if (player.GetComponent<PlayerController>().potion != "")
            {
                itemName = player.GetComponent<PlayerController>().potion;
            }
            else
            {
                itemName = "No potion equipped";
            }
        }

        switch (itemName) //get item description based on name, so this is where the description is decided
        {
            
            case "Lesser Hnarqu’s Tendril (ring)":
                itemDesc = "This ring channels the power of Hnarqu, Great Old One of the abyss, to inflict some extra damage to enemies.";
                break;
            case "Greater Hnarqu’s Tendril (ring)":
                itemDesc = "This ring channels the power of Hnarqu, Great Old One of the abyss, to inflict a lot of extra damage to enemies.";
                break;
            case "Lesser Han’s Claw (ring)":
                itemDesc = "This ring channels the power of Han, Great Old One of the howling wind and mist, to push away enemies when the wearer attacks.";
                break;
            case "Greater Han’s Claw (ring)":
                itemDesc = "This ring channels the power of Han, Great Old One of the howling wind and mist, to strongly push away enemies when the wearer attacks.";
                break;
            case "Lesser Ei’lor’s Vine (ring)":
                itemDesc = "This ring channels the power of Ei’lor, Great Old One of the jungle, to hold enemies in place with vines for a short duration when the wearer attacks.";
                break;
            case "Greater Ei’lor’s Vine (ring)":
                itemDesc = "This ring channels the power of Ei’lor, Great Old One of the jungle, to hold enemies in place with vines for a long duration when the wearer attacks.";
                break;
            case "Lesser B’gnu-Thun’s Eye (pendant)":
                itemDesc = "This pendant channels the power of B’gnu-Thun, Great Old One of the blizzard, to inflict some extra damage to enemies.";
                break;
            case "Greater B’gnu-Thun’s Eye (pendant)":
                itemDesc = "This pendant channels the power of B’gnu-Thun, Great Old One of the blizzard, to inflict a lot of extra damage to enemies.";
                break;
            case "Lesser Yomagn’tho’s Core (pendant)":
                itemDesc = "This pendant channels the power of Yomagn’tho, Great Old One of the inferno, to inflict some extra damage to enemies.";
                break;
            case "Greater Yomagn’tho’s Core (pendant)":
                itemDesc = "This pendant channels the power of Yomagn’tho, Great Old One of the inferno, to inflict a lot of extra damage to enemies.";
                break;
            case "Lesser Istasha’s Heart (pendant)":
                itemDesc = "This pendant channels the power of Istasha, Great Old One of the darkness, to inflict some extra damage to enemies.";
                break;
            case "Greater Istasha’s Heart (pendant)":
                itemDesc = "This pendant channels the power of Istasha, Great Old One of the darkness, to inflict a lot of extra damage to enemies.";
                break;
            case "Mynoghra's Love":
                itemDesc = "This cursed sword deals great damage to enemies, but requires its weilder to stay near their foes.";
                break;
            case "Atlach-Nacha's Fang":
                itemDesc = "This cursed knife deals less damage to enemies, but allows its weilder to stand back as it flings itself at their foes.";
                break;
            case "Greater Cthulhu’s Crown (circlet)":
                itemDesc = "This circlet channels the maddened power of the Great Lord Cthulhu, teleporting the user to a random location in the room. Press 'E' to use.";
                break;
            case "Greater Yegg-Ha’s Cloak (cloak)":
                itemDesc = "This cloak channels the stealthy power of the Great Lord Yegg-Ha, Lord of the Shadow, allowing the user to become invisible to enemies for some time. Press 'Q' to use.";
                break;
            case "Greater Aphoom-Zhah’s Blood (potion)":
                itemDesc = "This potion bottle carries the blazing power of the Great Lord Aphoom-Zha, Lord of the Flame, destroying all monsters that the user has discovered. Press 'X' to use.";
                break;
            default:
                itemDesc = "An empty slot to store an item. Monsters will sometimes leave items upon death, but not Atlach-Nacha's Fang nor Mynoghra's Love.";
                break;
        }

        if (isOver)
        {
            tooltip.transform.Find("Text").GetComponent<Text>().text = itemName;
            if (Input.GetMouseButtonDown(0)) //open description window on left click
            {
                descWin.SetActive(true);
                descWin.transform.Find("Title").GetComponent<Text>().text = itemName;
                descWin.transform.Find("Description").GetComponent<Text>().text = itemDesc;
            }
            if (Input.GetMouseButtonDown(1)) //throw out item on right click: remove from inventory and replace the sprite with silhouette
            {
                if (gameObject.name.Contains("ring"))
                {
                    if (player.GetComponent<PlayerController>().rings[System.Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1))] != null)
                    {
                        itemName = "No ring equipped";
                        player.GetComponent<PlayerController>().rings[System.Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1))] = null;
                        gameObject.GetComponent<Image>().sprite = silhouette;
                    }
                }
                else if (gameObject.name.Contains("pendant"))
                {
                    if (player.GetComponent<PlayerController>().pendant != "")
                    {
                        itemName = "No pendant equipped";
                        player.GetComponent<PlayerController>().pendant = "";
                        gameObject.GetComponent<Image>().sprite = silhouette;
                    }
                }
                //else if (gameObject.name.Contains("weapon"))
                //{
                //    if (player.GetComponent<PlayerController>().weapon != "")
                //    {
                //        itemName = "No weapon equipped";
                //        player.GetComponent<PlayerController>().weapon = "";
                //        gameObject.GetComponent<Image>().sprite = silhouette;
                //    }
                    
                //}
                else if (gameObject.name.Contains("cloak"))
                {
                    if (player.GetComponent<PlayerController>().cloak != "")
                    {
                        itemName = "No cloak equipped";
                        player.GetComponent<PlayerController>().cloak = "";
                        gameObject.GetComponent<Image>().sprite = silhouette;
                    }

                }
                else if (gameObject.name.Contains("circlet"))
                {
                    if (player.GetComponent<PlayerController>().circlet != "")
                    {
                        itemName = "No circlet equipped";
                        player.GetComponent<PlayerController>().circlet = "";
                        gameObject.GetComponent<Image>().sprite = silhouette;
                    }

                }
                else if (gameObject.name.Contains("potion"))
                {
                    if (player.GetComponent<PlayerController>().potion != "")
                    {
                        itemName = "No potion equipped";
                        player.GetComponent<PlayerController>().potion = "";
                        gameObject.GetComponent<Image>().sprite = silhouette;
                    }

                }
            }
        }
        
    }
}
