using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    public GameObject obj;
    private Collider2D objColl;

    private bool detection = false;

    private void Start()
    {
        objColl = obj.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == objColl)
        {
            Debug.Log("объект в зоне");
            detection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == objColl)
        {
            Debug.Log("объект не в зоне");
            detection = false;
        }
    }

    public bool GetDetection()
    {
        return detection;
    }
}
