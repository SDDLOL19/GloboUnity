using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    [SerializeField] int estadoGlobo; //0 es sin tocar, 1 es turno jugador 1, 2 es turno jugador 2
    [SerializeField] float escalaGravedad;
    [SerializeField] float fuerzaCabezazo;
    [SerializeField] Vector2 fuerzaPunch;
    SpriteRenderer spriteGlobo;
    [SerializeField] GameManager manejadorPartida;
    Animator animacionGlobo;
    [SerializeField] Sprite spriteBase;
    Rigidbody2D fisicasGlobo;
    [SerializeField] Transform spawnerGlobo;

    public AudioSource cabum;
    public AudioClip pum;

    void Start()
    {       
        estadoGlobo = 0;
        animacionGlobo = GetComponent<Animator>();
        fisicasGlobo = GetComponent<Rigidbody2D>();
        spriteGlobo = GetComponent<SpriteRenderer>();
        Spawn();
    }

    private void Update()
    {
        if (estadoGlobo == 1)
        {
            spriteGlobo.color = new Vector4(1, 0.19f, 0.05f, 1);
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

    public void Spawn()
    {
        animacionGlobo.SetBool("GloboMuerte", false);
        estadoGlobo = 0;
        this.transform.position = spawnerGlobo.position;      
    }

    void Explode()
    {
        manejadorPartida.SumarPuntos(estadoGlobo);
        DesactivarFisicas();
        animacionGlobo.SetBool("GloboMuerte", true);
        cabum.clip = pum;
        cabum.Play();
    }

    void RecibirCabezazo()
    {
        fisicasGlobo.velocity = new Vector2(0, fuerzaCabezazo);
    }

    void RecibirPunch(int velocityPlayer)
    {
        if (velocityPlayer < 0)   //Si el jugador se estaba moviendo a la izquierda cuando golpea el globo este se mueve hacia la izquierda
        {
            fisicasGlobo.velocity = new Vector2(-fuerzaPunch.x, fuerzaPunch.y);
        }

        else if (velocityPlayer > 0)    //Si el jugador se estaba moviendo a la derecha cuando golpea el globo este se mueve hacia la derecha
        {
            fisicasGlobo.velocity = fuerzaPunch;
        }

        else   //Si el jugador no se estaba moviendo cuando golpea el globo este solo sube un poco
        {
            fisicasGlobo.velocity = new Vector2(fisicasGlobo.velocity.x, fuerzaPunch.y);
        }
    }

    void ActivarFisicas()
    {
        fisicasGlobo.gravityScale = escalaGravedad;
    }

    void DesactivarFisicas()
    {
        fisicasGlobo.Sleep();
        fisicasGlobo.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform") && fisicasGlobo.velocity.y < 0)  //Si se choca contra el suelo mientras cae */
        {
            Debug.Log("Hey all, Scott here");
            Explode();
        }

        if (collision.gameObject.tag == "DeathZone")
        {
            Debug.Log("Exploté por deathzone");
            Explode();
        }

        if (estadoGlobo == 2)      //Si el jugador rojo golpea el globo rojo se vuelve azul y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 1;
                RecibirPunch((int)collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.x);
            }

            else if (collision.gameObject.tag == "Cabeza1" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 1;
                RecibirCabezazo();
            }
        }

        else if (estadoGlobo == 1) //Si el jugador azul golpea el globo azul se vuelve rojo anaranjado y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 2;
                RecibirPunch((int)collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.x);
            }

            else if (collision.gameObject.tag == "Cabeza2" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 2;
                RecibirCabezazo();
            }
        }

        else if (estadoGlobo == 0)        //Si un jugador golpea el globo gris se vuelve del color del rival y cambia su velocity
        {
            if (collision.gameObject.tag == "Punch1")
            {
                estadoGlobo = 1;
                ActivarFisicas();
                RecibirPunch((int)collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.x);
            }

            else if (collision.gameObject.tag == "Punch2")
            {
                estadoGlobo = 2;
                ActivarFisicas();
                RecibirPunch((int)collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.x);
            }

            else if (collision.gameObject.tag == "Cabeza1" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 1;
                ActivarFisicas();
                RecibirCabezazo();
            }

            else if (collision.gameObject.tag == "Cabeza2" && collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y >= 0)  //Si el jugador choca su cabeza con el globo y está subiendo el globo sube
            {
                estadoGlobo = 2;
                ActivarFisicas();
                RecibirCabezazo();
            }
        }
    }
}
