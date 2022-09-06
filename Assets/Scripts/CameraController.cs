using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 position;
    private float dumping = 1.5f;

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Player_control>().transform;
        }
    }

    private void Update()
    {
        position = player.position;
        position.z = -1;
        position.y += 0.7f;
        position.x += 0.5f;

        transform.position = Vector3.Lerp(transform.position, position, dumping * Time.deltaTime);
    }
}
