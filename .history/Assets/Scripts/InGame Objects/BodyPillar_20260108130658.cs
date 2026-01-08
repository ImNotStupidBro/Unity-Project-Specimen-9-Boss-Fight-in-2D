using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPillar : MonoBehaviour
{
    [SerializeField] private float 
        riseSpeed = 5f,
        lifetime = 3f;
    private SpriteRenderer[] sprites;
    private BoxCollider2D col;
    public GameObject dangerZonePrefab;
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;

        StartCoroutine(SpawnBodyPillar());
    }

    private void Rise()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            //Upon calling Rise, have all sprites stretched horizonally to indicate emergence
            sprite.transform.localScale = Vector2.Lerp(
                new Vector2(1.5f, 0.5f), // Start scale
                new Vector2(1f, 1f), // Target scale
                0.5f); // Time factor (0 to 1)
            sprite.enabled = true;
        }
        col.enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, riseSpeed);
    }

    public IEnumerator SpawnBodyPillar()
    {
        Rise();
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}