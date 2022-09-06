using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_control : MonoBehaviour
{
    [SerializeField] AudioSource jumpSuond;
    [SerializeField] int JumpForce = 250;
    [SerializeField] float MoveSpeed = 1.5f;
    [SerializeField] private Image[] hearts;
    [SerializeField] private int health;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    public Rigidbody2D rb;
    private Vector2 moveVector;
    private Animator anim;
    private SpriteRenderer sr;
    public static Player_control Instance { get; set; }
    private int throwForce = 100;
    private int lives = 5;
    public bool isClimb;
    private float climbSpeed;
    private float normalSpeed = 1.5f;

    private void Awake()
    {
        health = lives;
        Instance = this;
    }

    private void Start()
    {
        MoveSpeed = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        Run();
        Reflect();
        RestartLevel();
        if(isClimb)
        {
            Climbing();
        }
        
    }

    private void Update()
    {
        CheckingGround();
        //Jump();
        CheckingLadder();
        

        if(health > lives)
        {
            health = lives;
        }
        CheckLives();
        

    }

    private bool FaceRight = true;
    public void Reflect()
    {
        if((moveVector.x > 0 && !FaceRight) || (moveVector.x < 0 && FaceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            FaceRight = !FaceRight;
        }
    }


    private void Run()
    {
        //runningSound.Play();
        moveVector.x = rb.velocity.x;
        anim.SetFloat("Run", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
        //Debug.Log($"Vector is {moveVector.x}");
        
    }

    public void RunRight()
    {
        
        MoveSpeed = normalSpeed;
    }
    public void RunLeft()
    {
       
        MoveSpeed = -normalSpeed;
    }

    public void OnButtonUp()
    {
        MoveSpeed = 0f;

    }
    public void OnLadderButtonUp()
    {
        climbSpeed = 0f;
        isClimb = false;
    }

    public void OnJumpButtonDown()
    {
        if (onGround || (++jumpCount < maxJumpValue))
        {
            //jumpSuond.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * JumpForce);
            Debug.Log($"Vector Y is {moveVector.y}");
        }
        if (onGround)
        {
            jumpCount = 0;
        }

        //if (onLadder == true)
        //{
        //    moveVector.y = rb.velocity.y;
        //    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * MoveSpeed * 0.6f);
        //}
    }

    private void Climbing()
    {
        
        if (onLadder == true)
        {
            
            moveVector.y = rb.velocity.y;
            rb.velocity = new Vector2(0f, climbSpeed);
            Debug.Log($"Vector Y is {moveVector.y}");
        }
        
    }

    public void OnLadderButtonDown()
    {
        climbSpeed = 1f;
        isClimb = true;
    }



    public void OnButtonDown()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Invoke("IgnoreLayerOff", 0.5f);
    }

    private void CheckLives()
    {
        for (int h = 0; h < hearts.Length; h++)
        {
            if (h < health)
            {
                hearts[h].sprite = aliveHeart;
            }
            /*else
            {
                hearts[h].sprite = deadHeart;
            }*/

            if (h < lives)
            {
                hearts[h].enabled = true;
            }
            else
            {
                hearts[h].enabled = false;
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);

        }

        if(Input.GetKeyDown(KeyCode.Space) && (onGround || (++jumpCount < maxJumpValue)))
        {
            //jumpSuond.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * JumpForce);
        }
        if (onGround)
        {
            jumpCount = 0;
        }
        
    }
    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public bool onGround;
    public bool onLadder;
    public Transform GroundCheck;
    public Transform LadderCheck;
    public float checkRadius = 0.09f;
    public LayerMask Ground;
    public LayerMask Ladder;
    private int jumpCount = 0;
    public int maxJumpValue = 2;
    public int score = 0;

    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }

    private void CheckingLadder()
    {
        onLadder = Physics2D.OverlapCircle(LadderCheck.position, checkRadius, Ladder);
        
    }


    public void GetDamage()
    {
        lives -= 1;
        Debug.Log($"You have {lives} lives");
    }
    public void Heal()
    {
        lives += 1;
        Debug.Log($"After healing you have {lives} lives");
    }

    public void GetScore()
    {
        score += 1;
        Debug.Log($"Your score: {score}");
    }
    
    public void HurtThrow()
    {
        rb.AddForce(Vector2.up * throwForce);
    }

    private void RestartLevel()
    {
        if (lives < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Death()
    {
        lives = 0;
    }


    
}
