using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_script : Entity
{
    private Rigidbody2D doorrb;

    void Start()
    {
        doorrb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((Player_control.Instance.score >= 4) )
        {
            Die();
        }
    }

}
