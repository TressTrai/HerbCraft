using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Translator : MonoBehaviour
{
    public TextMeshProUGUI textArea;
    public string ru;
    public string en;
    public string tr;

    private string lang = "en";

    private void Awake()
    {
        lang = "ru"; //��� ����� �������� �� ������
        if (lang == "ru")
        {
            textArea.text = ru;
        }
        else
        {
            if (lang == "tr")
            {
                textArea.text = tr;
            }
            else
            {
                textArea.text = en;
            }
        }
    }
}
