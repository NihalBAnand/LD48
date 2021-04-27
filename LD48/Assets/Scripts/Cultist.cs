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
            case 2:
                text = new List<string>(new string[] {"...You have done well to make it this far, initiate...", "...Your next task will prove your valor...", "...Defeat 25 of the monsters lurking within the realm beyond the SUMMONING CIRCLE and find the SIGIL OF RETURN...", "...The mercy of the GREAT ONES be with you." });
                break;
            case 3:
                text = new List<string>(new string[] { "...Your worthiness is proven, brother...", "...AZATHOTH desires an ancient artifact, retrieved from within the realm beyond the SUMMONING CIRCLE...", "...Find it, and once again locate the SIGIL OF RETURN...", "...The mercy of the GREAT ONES be with you." });
                break;
            case 4:
                text = new List<string>(new string[] { "...AZATHOTH is pleased with your service...", "...This task will prove your devotion...", "...Find the SUMMONING SIGIL and follow the instructions of the BROTHER you find there...", "...The mercy of the GREAT ONES be with you." });
                break;
            case 5:
                text = new List<string>(new string[] { "...Your devotion is proven, Brother...", "...AZATHOTH requires one more task before his design is completed...", "...Venture once more to the realm beyond the SUMMONING CIRCLE, and locate the SIGIL OF RETURN...", "...It is there that AZATHOTH’S RETURN shall take place...", "...The glory of the GREAT ONES be with you." });
                break;
        }

        if (gameObject.name.Contains("Sacrifice"))
        {
            text = new List<string>(new string[] { "AZATHOTH DEMANDS A SACRIFICE.", "SHE STILL LIVES. KILL THIS WOMAN AND OPEN THE GATES OF MADNESS UPON ALL MANKIND." });
        }

        if (gameObject.name.Contains("Self"))
        {
            text = new List<string>(new string[] { "...AZATHOTH requires a show of DEVOTION...", "...Slice off your hand and present it to the GREAT ONE!" });
        }
    }
}
