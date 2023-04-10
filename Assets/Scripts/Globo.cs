using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    int estadoGlobo; //0 es sin tocar, 1 es turno jugador 1, 2 es turno jugador 2
    SpriteRenderer spriteGlobo;
    GameManager manejadorPartida;

    void Start()
    {
        estadoGlobo = 0;
        spriteGlobo = GetComponent<SpriteRenderer>();
        spriteGlobo.color = new Vector4(1, 1, 1, 1);
    }

    private void Update()
    {
        if (estadoGlobo == 1)
        {
            spriteGlobo.color = new Vector4(1, 0.47f, 0.13f, 1);
        }
        else if (estadoGlobo == 2)
        {
            spriteGlobo.color = new Vector4(0, 0.8f, 1, 1);
        }
    }

    void Explode()
    {
        manejadorPartida.SumarPuntos(estadoGlobo);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            Explode();
        }

        if (collision.gameObject.tag == "Ground" && collision.transform.position.y < this.transform.position.y)  //Si se choca contra el suelo y el suelo está debajo suya explota 
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estadoGlobo == 1)      //Si el jugador rojo golpea el globo rojo se vuelve azul y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 2;
            }

            else if (collision.gameObject.tag == "Cabeza1")
            {
                estadoGlobo = 2;
            }
        }

        else if (estadoGlobo == 2) //Si el jugador azul golpea el globo azul se vuelve rojo anaranjado y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 1;
            }

            else if (collision.gameObject.tag == "Cabeza2")
            {
                estadoGlobo = 1;
            }
        }

        else if (estadoGlobo == 0)        //Si un jugador golpea el globo gris se vuelve del color del rival y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 2;
            }

            else if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 1;
            }

            else if (collision.gameObject.tag == "Cabeza1")
            {
                estadoGlobo = 2;
            }

            else if (collision.gameObject.tag == "Cabeza2")
            {
                estadoGlobo = 1;
            }
        }
    }
}
