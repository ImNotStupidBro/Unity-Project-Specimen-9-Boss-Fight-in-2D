using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum active
{
    OFF = 0,
    ON = 1
}

public class HandWave : MonoBehaviour
{
    private active activeState = active.OFF;
    // Unity Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    public void Emerge()
    {activeState = active.ON;
        rb.velocity = new Vector2(0, -5);
        
    }
}
