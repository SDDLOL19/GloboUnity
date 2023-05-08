using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzable : MonoBehaviour
{
    public int numeroJugadorContrario;
    public Transform player;

    void Update()
    {
        if (player.localScale.x == -1)
        {
            this.transform.position += (-player.right) * Time.deltaTime * 9;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        else
        {
            this.transform.position += player.right * Time.deltaTime * 9;
        }
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
