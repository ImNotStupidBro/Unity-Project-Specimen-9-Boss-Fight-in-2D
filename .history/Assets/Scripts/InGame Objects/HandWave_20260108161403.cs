using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWave : MonoBehaviour
{
    private float 
        movementSpeed = 8f,
        waitDuration = 1.5f,
        stallDuration = 0.5f;
    // Unity Components
    private Rigidbody2D rb;
    private BoxCollider2D col;
    [SerializeField] private Vector2 pivotPoint;
    public GameObject dangerZonePrefab;
    private GameObject dangerZoneInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        pivotPoint = new Vector2(transform.position.x, transform.position.y + 2.0f);

        StartCoroutine(SpawnHandWave());
    }
    // HandWave Methods
    private void Emerge()
    {
        rb.velocity = new Vector2(0, movementSpeed);
    }
    private void Retract()
    {
        rb.velocity = new Vector2(0, -movementSpeed);
    }

    private IEnumerator EmergeAndRetract(float waitTime)
    {
        Emerge();
        col.e
        //Stop movement after reaching pivot point
        while(transform.position.y < pivotPoint.y)
        {
            yield return null;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);
        col.enabled = false;
        Retract();
        //After retracting below pivot point, destroy object
        while(transform.position.y > pivotPoint.y - 2.0f)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

    public IEnumerator SpawnHandWave()
    {
        //Spawn Danger Zone
        dangerZoneInstance = Instantiate(
            dangerZonePrefab, 
            new Vector3(transform.position.x, transform.position.y + 0.55f, -2.1f), 
            Quaternion.identity);
        SpriteRenderer dangerZoneInstanceRenderer = dangerZoneInstance.GetComponent<SpriteRenderer>();
        dangerZoneInstanceRenderer.size = new Vector2(2.0f, 1.0f);
        yield return new WaitForSeconds(waitDuration);
        Destroy(dangerZoneInstance);
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
