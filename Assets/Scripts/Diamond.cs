using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Entity
{
    public static Diamond Instance { get; set; }
    private Rigidbody2D gemrb;
    public GameObject gem;
    private int collisionTimeCounter = 0;
    void Start()
    {
        gemrb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            ++collisionTimeCounter;
            if (collisionTimeCounter == 1)
            {
                Player_control.Instance.GetScore();
                Die();
            }
        }

    }
}
