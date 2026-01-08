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
    private int HP = 3;
    private int numOfFlashes = 10;
    private int direction = -1; // 1 for right, -1 for left

    // Unity Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private BoxCollider2D hurtboxcol;
    private BoxCollider2D hitboxcol;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        spriteRend = GetComponentInParent<SpriteRenderer>();
        hitboxcol = GetComponent<BoxCollider2D>();
        hurtboxcol = GameObject.Find;
    }

    void Update()
    {
        
    }
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
        for(int i = 0; i < numOfFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfFlashes );
        }
        Physics2D.IgnoreLayerCollision(8, 6, false);
        direction = temp;
    }

    public void Dead()
    {
        hitboxcol.enabled = false;
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
