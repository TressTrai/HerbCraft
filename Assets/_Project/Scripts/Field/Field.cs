using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    private GameObject obj;
    private Collider2D objColl;
    private Collider2D polygon;

    private bool detection = false;
    Bounds bounds;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("PlayerBody");
        objColl = obj.GetComponent<Collider2D>();
        polygon = GetComponent<Collider2D>();
        bounds = polygon.bounds;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == objColl)
        {
            detection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == objColl)
        {
            detection = false;
        }
    }

    public bool GetDetection()
    {
        return detection;
    }

    public Vector2 GetRandomPoint()
    {
        Vector2 randomPoint;

        do
        {
            randomPoint = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
        } while (!polygon.OverlapPoint(randomPoint));

        return randomPoint;
    }
}
