using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzable : MonoBehaviour
{
    public int numeroJugadorContrario;
    public Transform player;
    Vector3 direccíon;

    private void Start()
    {
        if (player.localScale.x == -1)
        {
            direccíon = -player.right;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        else
        {
            direccíon = player.right;
        }
    }

    void Update()
    {
        this.transform.position += direccíon * Time.deltaTime * 9;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player" + numeroJugadorContrario)
        {
            Destroy(this.gameObject);
        }
    }
}
