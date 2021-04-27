using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    public List<string> text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameObject.Find("OP Controller").GetComponent<OPController>().globalLevel) //set dialogue lines for every level
        {
            case 1:
                text = new List<string>(new string[] { "...The children of AZATHOTH welcome you, initiate…", "...Your trial is to undergo a series of ordeals, designed to test your will and devotion...",
                    "...Find the SIGIL OF RETURN, identical to the SUMMONING CIRCLE before you...", "...Take your pick of ATLACH-NACHA'S FANG or MYNOGHRA'S LOVE, to protect yourself on the way...",
                    "...Your first task begins as you enter the SUMMONING CIRCLE...", "...The mercy of the GREAT ONES be with you."});
                break;
        }

        if (gameObject.name.Contains("Sacrifice"))
        {
            text = new List<string>(new string[] { "AZATHOTH DEMANDS A SACRIFICE", "KILL THIS WOMAN AND OPEN THE GATES OF MADNESS UPON ALL MANKIND" });
        }

        if (gameObject.name.Contains("Self"))
        {
            text = new List<string>(new string[] { "...AZATHOTH requires a show of DEVOTION...", "...Slice off your hand and present it to the GREAT ONE!" });
        }
    }
}
