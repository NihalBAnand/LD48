using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Aight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().gotArtifact = true;
            Destroy(gameObject);
        }
    }
}
