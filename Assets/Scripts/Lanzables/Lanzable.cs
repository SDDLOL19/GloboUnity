using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzable : MonoBehaviour
{
    public int numeroJugadorContrario;

    void Update()
    {
        this.transform.position += this.transform.right * Time.deltaTime * 9;
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
