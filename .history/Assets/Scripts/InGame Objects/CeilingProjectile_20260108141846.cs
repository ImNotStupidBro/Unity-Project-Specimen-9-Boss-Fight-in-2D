using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingProjectile : MonoBehaviour
{
    [SerializeField] private float 
        fallSpeed,
        stallDuration,
        stallDurationMin,
        stallDurationMax;
    // Unity Components
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    public GameObject dangerZonePrefab;
    private GameObject dangerZoneInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        col.enabled = false;

        StartCoroutine(StallBeforeFall());
    }

    public IEnumerator StallBeforeFall()
    {
        stallDuration = Random.Range(stallDurationMin, stallDurationMax);
        dangerZoneInstance = Instantiate(dangerZonePrefab, new Vector2(transform.position.x, 5), Quaternion.identity);
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
