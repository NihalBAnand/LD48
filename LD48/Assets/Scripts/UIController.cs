using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject textbox;
    GameObject textfield;

    public bool tStarted;
    int pages;
    public int curPage;
    List<string> text;
    // Start is called before the first frame update
    void Start()
    {
        textbox = GameObject.Find("Textbox");
        textfield = GameObject.Find("tboxText");
        tStarted = false;

        textbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        textbox.SetActive(false);

        if (tStarted)
        {
            textbox.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                curPage++;
            }
            if (curPage >= pages)
            {
                tStarted = false;
            }
            textfield.GetComponent<Text>().text = text[curPage];
        }
    }

    public void CreateTextbox(List<string> text)
    {
        this.pages = text.Count;
        this.text = text;
        curPage = 0;

        tStarted = true;
    }

    
}
