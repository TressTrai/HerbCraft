using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : Button
{
    public GameObject UI_1;
    public GameObject UI_2;
    public GameObject UI_3;
    public GameObject UI_ALL;

    public void NextUI_1()
    {
        if (UI_1.active == true)
        {

            UI_1.SetActive(false);
            UI_2.SetActive(true);
        }
        else if (UI_2.active == true)
        {
            UI_2.SetActive(false);
            UI_3.SetActive(true);
        }
        
        else
        {
            UI_ALL.SetActive(false);
        }

    }

}
