using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{

    public GameObject sacrifice;
    public GameObject cultist;
    public GameObject opcont;
    OPController op;
    // Start is called before the first frame update
    void Start()
    {
        op = opcont.GetComponent<OPController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (op.globalLevel)
        {
            case 4:
                cultist.SetActive(true);
                sacrifice.SetActive(false);
                break;
            case 5:
                cultist.SetActive(false);
                sacrifice.SetActive(true);
                break;
            default:
                cultist.SetActive(false);
                sacrifice.SetActive(false);
                break;
        }
    }
}
