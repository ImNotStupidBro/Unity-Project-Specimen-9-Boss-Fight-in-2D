using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum wellnessState
{
    Vulnerable = 0,
    Invulnerable = 1,
    Hurt = 2,
    Dead = 3
}

public class Minion : MonoBehaviour
{
    // Minion Statistics
    private wellnessState currentState = wellnessState.Vulnerable;
    private int 
        HP = 3,
        direction = -1,
        movementSpeed = 2,
        numOfIFlashes = 10;
    // Unity Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private BoxCollider2D hurtboxcol;
    private BoxCollider2D hitboxcol;
    private Transform playerTransform;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        spriteRend = GetComponentInParent<SpriteRenderer>();
        hitboxcol = GetComponent<BoxCollider2D>();
        hurtboxcol = GameObject.Find("Physical Collision Box").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }
    // Minion Methods
    public void DamageEnemy(int DMG) 
    { 
        HP -= DMG;
        
        if (HP <= 0) { 
            Invoke("Dead", 0.0f); 
        } else {
            StartCoroutine(DamageFlicker());
        }
    }

    private IEnumerator DamageFlicker()
    {
        int temp = direction;
        direction = 0;
        Physics2D.IgnoreLayerCollision(8, 6, true);
        for(int i = 0; i < numOfIFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfIFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfIFlashes );
        }
        Physics2D.IgnoreLayerCollision(8, 6, false);
        direction = temp;
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
        hitboxcol.enabled = false;
        hurtboxcol.enabled = false;
        rb.gravityScale = 0;
        StartCoroutine(DeathDelay());
    }
}
