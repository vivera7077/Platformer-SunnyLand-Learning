using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum_control : Entity
{
    private Rigidbody2D rbOpp;
    public Transform[] moveSpots;
    public float livesForDeathAnim;
    private int point;
    private Animator anim;
    public float MoveSpeed = 1f;
    private Vector2 dir;
    private bool dirRight = true;
    private SpriteRenderer sr;
    private int collisionTimeCounter = 0;
    public GameObject gemPrefab;
    private Animator oppAnim;
    [SerializeField] private int OppLives = 1;

    private void Start()
    {
        rbOpp = GetComponent<Rigidbody2D>();
        oppAnim = GetComponent<Animator>();
        dir = transform.right;
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Update()
    {     
        
        
    }

    public void ReflectOpp()
    {
        if ((Vector2.Distance(transform.position, moveSpots[point].position) < 0.2f))
        {
            transform.localScale *= new Vector2(-1, 1);
            dirRight = !dirRight;
        }
    }


    
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[point].position, MoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[point].position) < 0.2f)
        {

            ReflectOpp();
            if (point > 0)
            {
                point = 0;
            }
            else if (point < 1)
            {
                point = 1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            collisionTimeCounter++;
            if (collisionTimeCounter == 1)
            {
                Player_control.Instance.GetDamage();
                Player_control.Instance.HurtThrow();
                //OppLives--;
                Debug.Log($"Opossum has {OppLives} lives");
            }
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionTimeCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            ++collisionTimeCounter;
            if (collisionTimeCounter <= 1)
            {
                //Player_control.Instance.GetDamage();
                Player_control.Instance.HurtThrow();
                OppLives--;
                Debug.Log($"Opossum has {OppLives} lives");
            }
            if (OppLives == 0)
            {
                //oppAnim.SetFloat ("DeathAnim", livesForDeathAnim);
                Die();
                Instantiate(gemPrefab, rbOpp.transform.position, Quaternion.identity);
            
            
            }
        }
        collisionTimeCounter = 0;
    }

    /*public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.09f;
    public LayerMask Ground;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }*/
}
