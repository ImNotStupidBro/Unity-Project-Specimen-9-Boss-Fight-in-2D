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
    private CapsuleCollider2D col;
    private GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        col.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        StartCoroutine(StallBeforeFall());
    }

    public IEnumerator StallBeforeFall()
    {
        yield return new WaitForSeconds(stallDuration);
        col.enabled = true;
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(10);
        } else if(collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
