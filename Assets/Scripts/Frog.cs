using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Entity
{
    private Rigidbody2D rbFrog;
    private Animator anim;
    private int collisionTimeCounter = 0;
    [SerializeField] private int FrogLives = 1;

    private void Start()
    {

        anim = GetComponent<Animator>();
        rbFrog = GetComponent<Rigidbody2D>();
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            ++collisionTimeCounter;
            if (collisionTimeCounter == 1)
            {
                Player_control.Instance.GetDamage();
                //Player_control.Instance.HurtThrow();
                //FrogLives--;
                //Debug.Log($"Frog has {FrogLives} lives");
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
            collisionTimeCounter++;
            if (collisionTimeCounter == 1)
            {
                //Player_control.Instance.GetDamage();
                //Player_control.Instance.HurtThrow();
                FrogLives--;
                //Debug.Log($"Frog has {FrogLives} lives");
            }
            if (FrogLives == 0)
            {
                Die();
            }
        }
        collisionTimeCounter = 0;
    }
}
