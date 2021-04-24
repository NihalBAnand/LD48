using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        speed = .02f;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make player movement independent of resolution
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (System.Math.Abs(h) > 0.1 & System.Math.Abs(v) > 0.1)
        { 
            h /= 1.41f;
            v /= 1.41f;
        }
        
        transform.Translate(new Vector2( h * speed, v * speed));
    }
}
