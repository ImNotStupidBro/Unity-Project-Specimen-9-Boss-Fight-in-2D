using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWave : MonoBehaviour
{
    private int movementSpeed = 5;
    // Unity Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Transform pivotPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        pivotPoint = GetComponentInChildren<Transform>();
        col.enabled = false;
    }

    void Update()
    {
        
    }

    public void Emerge()
    {
        col.enabled = true;
        rb.velocity = new Vector2(0, -movementSpeed);
    }
    public void Retract()
    {
        col.enabled = false;
        rb.velocity = new Vector2(0, movementSpeed);
    }

    private IEnumerator EmergeAndRetract(float waitTime)
    {
        Emerge();
        //Stop movement after reaching pivot point
        while(transform.position.y > pivotPoint.position.y)

        yield return new WaitForSeconds(waitTime);
        Retract();
    }
}
