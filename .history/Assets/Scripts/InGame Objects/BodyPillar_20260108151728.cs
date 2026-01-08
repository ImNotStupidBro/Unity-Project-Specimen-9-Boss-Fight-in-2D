using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPillar : MonoBehaviour
{
    [SerializeField] private float 
        riseSpeed = 5f,
        lifetime = 5f,
        waitDuration = 1.0f;
    private SpriteRenderer[] sprites;
    private Rigidbody2D [] spriteRBs;
    private BoxCollider2D col;
    public GameObject dangerZonePrefab;
    [SerializeField] private Transform loopStartPoint;
    [SerializeField] private Transform loopEndPoint;
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        spriteRBs = GetComponentsInChildren<Rigidbody2D>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;

        StartCoroutine(SpawnBodyPillar());
    }

    void Update()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            if(sprite.gameObject.transform.position.y >= loopEndPoint.position.y)
            {
                //Loop the pillar's position between the two points
                sprite.gameObject.transform.position = new Vector2(
                    sprite.gameObject.transform.position.x,
                    loopStartPoint.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(15);
        }
    }

    private void Rise()
    {
        foreach (Rigidbody2D rb in spriteRBs)
        {
            rb.velocity = new Vector2(0, riseSpeed);
        }
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
            //Upon calling Rise, have all sprites stretched horizonally to indicate emergence
            sprite.transform.localScale = Vector2.Lerp(
                new Vector2(1.0f, 1.0f), // Start scale
                new Vector2(1.0f, 1.0f), // Target scale
                0.1f); // Time factor (0 to 1)
        }
        col.enabled = true;
    }
    private IEnumerator ExpandPillar()

    public IEnumerator SpawnBodyPillar()
    {
        yield return new WaitForSeconds(waitDuration);
        Rise();
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}