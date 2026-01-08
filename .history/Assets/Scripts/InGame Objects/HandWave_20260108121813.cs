using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWave : MonoBehaviour
{
    private int movementSpeed = 5;
    // Unity Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    void Update()
    {
        
    }

    public void Emerge()
    {
        col.enabled = true;
        rb.velocity = new Vector2(0, -5);
    }
    public void Retract()
    {
        col.enabled = false;
        rb.velocity = new Vector2(0, 5);
    }

    private IEnumerator EmergeAndRetract(float waitTime)
    {
        Emerge();
        yield return new WaitForSeconds(waitTime);
        Retract();
    }
}
