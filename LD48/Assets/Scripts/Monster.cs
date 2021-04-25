using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] Images;
    
    public SpriteRenderer spriteRenderer;

    public int level;
    public float health;
    void Start()
    {
        health = 100f;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (level > 0)
            spriteRenderer.sprite = Images[level - 1];

        
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")){
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
            StartCoroutine(flashColor());
        }
        Debug.Log("Monster Attack");
        Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);
    }

    private IEnumerator flashColor()
    {
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 105, 105, 255);
        yield return new WaitForSecondsRealtime(.2f);
        GameObject.Find("HealthBar").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
