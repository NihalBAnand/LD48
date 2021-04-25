using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCircle : MonoBehaviour
{
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
        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.transform.position = new Vector3(0, 0, 0);
            GameObject.Find("OP Controller").GetComponent<OPController>().enterLevel();
            
        }
    }
}
