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
    private wellnessState currentState = wellnessState.Vulnerable;
    private int HP = 3;
    private int numOfFlashes = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator DamageFlicker()
    {
        int temp = facingDirection;
        facingDirection = 0;
        Physics2D.IgnoreLayerCollision(8, 6, true);
        for(int i = 0; i < numOfFlashes; i++)
        {
            spriteRend.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds( 1 / numOfFlashes );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( 1 / numOfFlashes );
        }
        Physics2D.IgnoreLayerCollision(8, 6, false);
        facingDirection = temp;
    }
}
