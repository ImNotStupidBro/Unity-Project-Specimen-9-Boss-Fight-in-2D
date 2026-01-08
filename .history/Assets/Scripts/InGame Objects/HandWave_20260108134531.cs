using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWave : MonoBehaviour
{
    private int movementSpeed = 5;
    private float 
        waitDuration = 1.5f,
        stallDuration = 0.5f;
    // Unity Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    [private Transform pivotPoint;
    public GameObject dangerZonePrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        pivotPoint = GetComponentInChildren<Transform>();
        col.enabled = false;

        StartCoroutine(SpawnHandWave());
    }
    // HandWave Methods
    private void Emerge()
    {
        col.enabled = true;
        rb.velocity = new Vector2(0, -movementSpeed);
    }
    private void Retract()
    {
        col.enabled = false;
        rb.velocity = new Vector2(0, movementSpeed);
    }

    private IEnumerator EmergeAndRetract(float waitTime)
    {
        Emerge();
        //Stop movement after reaching pivot point
        while(transform.position.y < pivotPoint.position.y)
        {
            yield return null;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);
        Retract();
        //After retracting below pivot point, destroy object
        while(transform.position.y > pivotPoint.position.y - 1)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

    public IEnumerator SpawnHandWave()
    {
        yield return new WaitForSeconds(waitDuration);
        StartCoroutine(EmergeAndRetract(stallDuration));
    }

    // Collision Detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(10);
        }
    }
}
