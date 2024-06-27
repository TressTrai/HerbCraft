using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField]
    private string text;
    private TextMeshProUGUI textMeshPro;

    public string TextValue
    {
        get { return text; }
        set { text = value; }
    }

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateGameObjectText();
    }
    public void UpdateGameObjectText()
    {
        textMeshPro.text = text;
    }
}
