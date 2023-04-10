using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    int estadoGlobo; //0 es sin tocar, 1 es turno jugador 1, 2 es turno jugador 2
    SpriteRenderer spriteGlobo;
    [SerializeField] GameManager manejadorPartida;
    Rigidbody2D fisicasGlobo;
    [SerializeField] Transform spawnerGlobo;

    void Start()
    {
        Spawn();
        estadoGlobo = 0;
        fisicasGlobo = GetComponent<Rigidbody2D>();
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

        else if (estadoGlobo == 0)
        {
            spriteGlobo.color = new Vector4(1, 1, 1, 1);
        }
    }

    void Spawn()
    {
        estadoGlobo = 0;
        this.transform.position = spawnerGlobo.position;
    }

    void Explode()
    {
        manejadorPartida.SumarPuntos(estadoGlobo);
        DesactivarFisicas();
        Spawn();
    }

    void RecibirCabezazo()
    {

    }

    void ActivarFisicas()
    {
        fisicasGlobo.gravityScale = 1;
    }

    void DesactivarFisicas()
    {
        fisicasGlobo.Sleep();
        fisicasGlobo.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && fisicasGlobo.velocity.y < 0)  //Si se choca contra el suelo mientras cae */
        {
            Debug.Log("Hey all, Scott here");
            Explode();
        }

        if (collision.gameObject.tag == "DeathZone")
        {
            Debug.Log("Exploté por deathzone");
            Explode();
        }

        if (estadoGlobo == 1)      //Si el jugador rojo golpea el globo rojo se vuelve azul y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 2;
            }

            else if (collision.gameObject.tag == "Cabeza1" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 2;
                RecibirCabezazo();
            }
        }

        else if (estadoGlobo == 2) //Si el jugador azul golpea el globo azul se vuelve rojo anaranjado y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 1;
            }

            else if (collision.gameObject.tag == "Cabeza2" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 1;
                RecibirCabezazo();
            }
        }

        else if (estadoGlobo == 0)        //Si un jugador golpea el globo gris se vuelve del color del rival y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 2;
                ActivarFisicas();
            }

            else if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 1;
                ActivarFisicas();
            }

            else if (collision.gameObject.tag == "Cabeza1" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 2;
                ActivarFisicas();
                RecibirCabezazo();
            }

            else if (collision.gameObject.tag == "Cabeza2" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 1;
                ActivarFisicas();
                RecibirCabezazo();
            }
        }
    }
}
