using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDos : Player      //Clase pplayer para que coja todo lo básico y poder cambiar los controles
{
    private void Awake()
    {
        numeroPlayer = 2;
        numeroEnemigo = 1;
        personajesAnimados = spriteManejador.personajesAnimados;

        numeroSkin = SpritesManager.personajeJugadorDos;

        PonerSkinAdecuada();
        GameManager.ultimateEnergy[1] = 0;
    }

    void Update()
    {
        if (canMove == true)
        {
            MovementSystem();

            if (Input.GetKeyDown(InputStuff.Golpe[1]) && canMove)
            {
                NormalPunching();
            }

            Ultimate();
        }

        AnimationSystem();
        EnergySystem();
    }

    void PonerSkinAdecuada()
    {
        this.GetComponent<Animator>().runtimeAnimatorController = personajesAnimados[numeroSkin];
    }

    void MovementSystem()
    {
        MovimientoLateral();

        if (Input.GetKeyDown(InputStuff.Arriba[1]))
        {
            if (canJump && cantidadSaltos == 0)
            {
                Salto();
            }

            else if (cantidadSaltos == 1 && energyBar == 100)
            {
                EnergyWaste();
                Salto();
            }
        }

        if (Input.GetKey(InputStuff.Dash[1]))
        {
            DashMove();
        }

        if (Input.GetKeyUp(InputStuff.Dash[1]))
        {
            dashing = false;
            canDash = true;
            dashTime = 0;

            if (energyBar == 100)
            {
                EnergyWaste();
            }     
        }
    }

    void MovimientoLateral()
    {
        playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);

        if (Input.GetKey(InputStuff.Izquierda[1]))
        {
            playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
            this.transform.localScale = new Vector3(-1, 1, 1); //para dar la vuelta al personaje y todos sus aspectos
        }

        if (Input.GetKey(InputStuff.Derecha[1]))
        {
            playerRigid.velocity = new Vector2(speed, playerRigid.velocity.y);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void DashMove()
    {
        if (canDash && energyBar == 100)
        {
            dashTime += Time.deltaTime;

            if (Input.GetKeyDown(InputStuff.Dash[1]))
            {
                miSonidito.clip = sonidos[3];
                miSonidito.Play();
            }

            if (dashTime < 0.35f)
            {
                if (Input.GetKey(InputStuff.Derecha[1]))
                {
                    dashing = true;
                    playerRigid.velocity = Vector2.right * speed * 2;
                }

                else if (Input.GetKey(InputStuff.Izquierda[1]))
                {
                    dashing = true;
                    playerRigid.velocity = Vector2.left * speed * 2;
                }
            }

            else if (dashTime >= 0.35f)
            {
                dashing = false;
                canDash = false;
                EnergyWaste();
            }
        }
    }

    void Ultimate()
    {
        if (Input.GetKeyDown(InputStuff.Ulti[1]) && GameManager.ultimateEnergy[1] >= 5)
        {
            GameManager.ultimateEnergy[1] = 0;
            playerAnim.Play("Ultimate");
            this.gameObject.GetComponent<Ultimates>().Ejecutar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ultimate1")
        {
            DeathSystem();
        }

        if (collision.gameObject.tag == "DeathZone")
        {
            DeathSystem();
        }
    }
}
