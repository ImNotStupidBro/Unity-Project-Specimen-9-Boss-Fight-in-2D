using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingProjectile : MonoBehaviour
{
    private float 
        fallSpeed = 8f,
        stallDuration = 1.0f;
    // Unity Components
    private Rigidbody2D rb;
    private CapsuleColliderCollider2D col;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    void Update()
    {
        
    }
}
