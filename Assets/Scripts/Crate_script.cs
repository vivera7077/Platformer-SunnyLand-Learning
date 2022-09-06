using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate_script : Entity
{
    private Rigidbody2D craterb;
    public GameObject cherryPrefab;
    
    void Start()
    {
        craterb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Player_control.Instance.gameObject)
        {
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                Die();
                Instantiate(cherryPrefab, craterb.transform.position, Quaternion.identity);
            }*/
            Die();
            Instantiate(cherryPrefab, craterb.transform.position, Quaternion.identity);

        }
    }
}
