using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum wellnessState
{
    Vulnerable = 0,
    Invulnerable = 1,
    Dead = 2
}

public class Minion : MonoBehaviour
{
    // Minion Statistics
    private wellnessState currentState = wellnessState.Vulnerable;
    [SerializeField] private int 
        HP,
        movementSpeed,
        numOfIFlashes;

    [SerializeField] private float DeathAnimDuration;
    // Unity Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private BoxCollider2D hurtboxcol;
    private BoxCollider2D hitboxcol;
    private GameObject player;
    private Transform playerPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        hurtboxcol = GetComponentInChildren<BoxCollider2D>();
        hitboxcol = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;
    }
    void Update()
    {
        MoveTowardPlayer();
        HitBoxToggle(currentState);
    }
    // Minion Methods
    private void MoveTowardPlayer()
    {
        Vector2 directionToPlayer = (playerPos.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x * movementSpeed, rb.velocity.y);
        if(directionToPlayer.x < 0)
        {
            spriteRend.flipX = false;
        }
        else if(directionToPlayer.x > 0)
        {
            spriteRend.flipX = true;
        }
    }
    public void DamageEnemy(int DMG) 
    { 
        HP -= DMG;
        movementSpeed = 0;
        if (HP <= 0) { 
            Invoke("Dead", 0.0f); 
        } else {
            StartCoroutine(DamageFlicker());
        }
    }
    public void Knockback(float direction)
    {
        rb.AddForce(new Vector2(direction * 5, 2), ForceMode2D.Impulse);
    }
    private void HitBoxToggle(wellnessState state)
    {
        hitboxcol.enabled = (state == wellnessState.Vulnerable) ? true : false;
    }
    private IEnumerator DamageFlicker()
    {
        currentState = wellnessState.Invulnerable;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for(int i = 0; i < numOfIFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfIFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfIFlashes );
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        currentState = wellnessState.Vulnerable;
        yield return new WaitForSeconds(0.0f);
        movementSpeed = 2;
    }
    private IEnumerator DeathAnim()
    {
        spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        rb.velocity = new Vector2(0, -5);
        yield return new WaitForSeconds(DeathAnimDuration);
        Destroy(gameObject);
    }
    public void Dead()
    {
        currentState = wellnessState.Dead;
        hitboxcol.enabled = false;
        hurtboxcol.enabled = false;
        rb.gravityScale = 0;
        StartCoroutine(DeathAnim());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(5);
        }
    }
}
