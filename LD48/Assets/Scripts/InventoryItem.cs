using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;

    public GameObject player;
    public GameObject tooltip;
    public GameObject canvas;
    public Sprite silhouette;

    public bool isOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        tooltip.SetActive(true);
        tooltip.transform.position = new Vector3(gameObject.transform.position.x + (gameObject.GetComponent<RectTransform>().rect.width * canvas.GetComponent<Canvas>().scaleFactor * 1.3f), gameObject.transform.position.y + (.75f * tooltip.GetComponent<RectTransform>().rect.height * canvas.GetComponent<Canvas>().scaleFactor));
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        tooltip.SetActive(false);
    }

    void Awake()
    {
        player = GameObject.Find("Player");
        tooltip = GameObject.Find("Tooltip");
        canvas = GameObject.Find("Canvas");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        tooltip.SetActive(false);
        isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("ring"))
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
        else if (gameObject.name.Contains("pendant"))
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
        
        if (isOver)
        {
            tooltip.transform.Find("Text").GetComponent<Text>().text = itemName;
            if (Input.GetMouseButtonDown(1))
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
            }
        }
    }
}
