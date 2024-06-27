using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private Text textMeshScript;
    private int clickedTimes;

    private void Awake()
    {
        textMeshScript = (Text)textMesh.GetComponent(typeof(Text));
    }

    public void IncrementClickedTimesValue()
    {
        ChangeTextCounterValue(++clickedTimes);
    }
    private void ChangeTextCounterValue(int newValue)
    {
        string initialValue = textMeshScript.TextValue;
        textMeshScript.TextValue = initialValue.Split(" ")[0] + " " + newValue.ToString();
        textMeshScript.UpdateGameObjectText();
    }
}
