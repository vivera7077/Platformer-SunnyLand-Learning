using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_script : MonoBehaviour
{
    private int collisionTimeCounter = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            ++collisionTimeCounter;
            if (collisionTimeCounter == 1)
            {
                Player_control.Instance.GetDamage();
                Player_control.Instance.HurtThrow();
            }

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionTimeCounter = 0;
    }
}
