using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Entity
{

    private Rigidbody2D cherryrb;
    private int collisionTimeCounter = 0;

    void Start()
    {
        cherryrb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            ++collisionTimeCounter;
           if (collisionTimeCounter == 1)
            {
                Player_control.Instance.Heal();
                Die();

            }
        }
        
    }
}
