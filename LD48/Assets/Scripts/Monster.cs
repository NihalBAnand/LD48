using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] Images;
    public int level;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        level = 3;
        Debug.Log(level);
        spriteRenderer.sprite = Images[level - 1];
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
