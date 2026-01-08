using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

enum PlayerPhysicalState
{
    Grounded = 0,
    Airborne = 1
}
enum PlayerInteractionState
{
    Idle = 0,
    Walking = 1,
    Jumping = 2,
    Attacking = 3
}
enum PlayerWellnessState
{
    Vulnerable = 0,
    Invulnerable = 1,
    Dead = 2
}

public class Player : MonoBehaviour
{
    // Player Statistics
    [SerializeField] PlayerInteractionState interactionState = PlayerInteractionState.Idle;
    [SerializeField] PlayerPhysicalState physicalState = PlayerPhysicalState.Airborne;
    [SerializeField] PlayerWellnessState wellnessState = PlayerWellnessState.Vulnerable;
    private float Horizontal_SPD = 3f;
    private float Vertical_SPD = 7f;
    [SerializeField] private int HP = 100;
    //private int ATK_Power = 1; Will be used in future implementations
    public Vector2 ATK_Size;
    private bool posLock = false;
    [SerializeField] private float iFrameTimer = 2.0f;
    private int numOfFlashes = 20;

    // Unity Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private Animator anim;
    //[SerializeField] private Collider2D[] colliders;
    private BoxCollider2D physicalCollider;
    //private BoxCollider2D interactionCollider;
    public GameObject ATKHitBoxInstance;
    public LayerMask enemies;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //colliders = GetComponentsInChildren<Collider2D>();
        physicalCollider = GetComponentInChildren<BoxCollider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        //interactionCollider = colliders[1] as BoxCollider2D;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(wellnessState == PlayerWellnessState.Dead) { return; }
        // Handle Movement
        MoveHorizontal();

        Jump();

        // Handle Attacking
        AttackCheck();

        AnimatorUpdater();
    }

    // Update Methods
    private void MoveHorizontal()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0 && !posLock)
        {
            if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            else if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
            interactionState = PlayerInteractionState.Walking;
            rb.velocity = new Vector2(horizontalInput * Horizontal_SPD, rb.velocity.y);
        }
        else
        {
            if(rb.velocity.x <= 0.01f && rb.velocity.x >= -0.01f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                interactionState = PlayerInteractionState.Idle;
            }
        }
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && (interactionState != PlayerInteractionState.Jumping || interactionState != PlayerInteractionState.Attacking) && physicalState == PlayerPhysicalState.Grounded && !posLock)
        {
            rb.AddForce(new Vector2(0, Vertical_SPD), ForceMode2D.Impulse);
            interactionState = PlayerInteractionState.Jumping;
            physicalState = PlayerPhysicalState.Airborne;
        }
    }
    private void AttackCheck()
    {
        if(Input.GetButtonDown("Fire1") && interactionState != PlayerInteractionState.Jumping && physicalState == PlayerPhysicalState.Grounded)
        {
            rb.velocity = new Vector2(0, 0);
            posLock = true;
            interactionState = PlayerInteractionState.Attacking;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            if(rb.velocity.x == 0)
            {
                interactionState = PlayerInteractionState.Idle;
            } else
            {
                interactionState = PlayerInteractionState.Walking;
            }
        }
    }
    private void AnimatorUpdater()
    {
        // Update Animator Parameters based on physicalState and interactionState
        anim.SetInteger("physicalEnumState", (int)physicalState);
        anim.SetInteger("interactionEnumState", (int)interactionState);
        anim.SetInteger("x_velocity", (int)rb.velocity.x);
        anim.SetFloat("y_velocity", rb.velocity.y);
        anim.SetBool("leftmousebutton_active", Input.GetButton("Fire1"));
    }

    // Action Methods
    private void AxeAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(ATKHitBoxInstance.transform.position, ATK_Size, enemies);

        foreach(Collider2D enemyGameObject in enemy)
        {
            if (enemyGameObject.tag == "Minion")
            {
                enemyG
            } else if (enemyGameObject.tag == "Specimen_9")
            {
                
            } else if (enemyGameObject.tag == "VolleyOrb")
            {
                enemyGameObject.GetComponent<VolleyOrb>().ReflectOrb();
            }
        }
    }
    // Other Methods
    private void UnlockPosition()
    {
        posLock = false;
    }
    private IEnumerator Invulnerable()
    {
        gameObject.layer = LayerMask.NameToLayer("Intangible");
        for(int i = 0; i < numOfFlashes; i++)
        {
            spriteRend.color = new Color(0, 0, 0, 0.0f);
            yield return new WaitForSeconds( iFrameTimer / (numOfFlashes * 4) );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds( iFrameTimer / (numOfFlashes * 4) );
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
        wellnessState = PlayerWellnessState.Vulnerable;
    }
    public void DamagePlayer(int DMG) 
    {
        HP -= DMG;
        Debug.Log("Player took " + DMG + " damage.");
        if(HP <= 0) 
        {
            gameObject.layer = LayerMask.NameToLayer("Intangible");
            wellnessState = PlayerWellnessState.Dead;
            Debug.Log("Player has died.");
        } else {
            anim.SetTrigger("damaged");
            wellnessState = PlayerWellnessState.Invulnerable;
            StartCoroutine(Invulnerable());
        }
    }

    // Collision Methods
    // Using Collision methods for simplicity; can be optimized with Raycasts or GroundCheck objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            //interactionState = PlayerInteractionState.Idle;
            physicalState = PlayerPhysicalState.Grounded;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ATKHitBoxInstance.transform.position, ATK_Size);
    }
}
