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
        HP = 3,
        movementSpeed = 2,
        numOfIFlashes = 10;
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
            Knockback((transform.position.x - playerPos.position.x) / Mathf.Abs(transform.position.x - playerPos.position.x));
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
        Physics2D.IgnoreLayerCollision(8, 6, true);
        for(int i = 0; i < numOfIFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfIFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfIFlashes );
        }
        Physics2D.IgnoreLayerCollision(8, 6, false);
        currentState = wellnessState.Vulnerable;
        movementSpeed = 2;
    }
    private IEnumerator DeathDelay()
    {
        spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void Dead()
    {
        currentState = wellnessState.Dead;
        hitboxcol.enabled = false;
        hurtboxcol.enabled = false;
        rb.gravityScale = 0;
        StartCoroutine(DeathDelay());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(5);
        }
    }
}
