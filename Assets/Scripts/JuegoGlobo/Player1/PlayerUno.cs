using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUno : Player      //Clase player para que coja todo lo básico y poder cambiar los controles
{
    private void Awake()
    {
        numeroPlayer = 1;
        numeroEnemigo = 2;
        personajesAnimados = spriteManejador.personajesAnimados;

        numeroSkin = SpritesManager.personajeJugadorUno;

        PonerSkinAdecuada();
    }

    void Update()
    {
        if (canMove == true)
        {
            MovementSystem();

            if (Keyboard.current.vKey.wasPressedThisFrame)
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

        if (Keyboard.current.wKey.wasPressedThisFrame)
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

        if (Keyboard.current.bKey.isPressed)
        {
            DashMove();
        }

        if (Keyboard.current.bKey.wasReleasedThisFrame)
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

        if (Keyboard.current.aKey.isPressed)
        {
            playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
            this.transform.localScale = new Vector3(-1, 1, 1); //para dar la vuelta al personaje y todos sus aspectos
        }

        if (Keyboard.current.dKey.isPressed)
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

            if (dashTime < 0.35f)
            {
                if (Keyboard.current.dKey.isPressed)
                {
                    dashing = true;
                    playerRigid.velocity = Vector2.right * speed * 2;
                }

                else if (Keyboard.current.aKey.isPressed)
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
        if (Keyboard.current.sKey.wasPressedThisFrame && GameManager.ultimateEnergy[0] >= 5)
        {
            GameManager.ultimateEnergy[0] = 0;
            playerAnim.Play("Ultimate");
            this.gameObject.GetComponent<Ultimates>().Ejecutar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ultimate2")
        {
            DeathSystem();
        }
    }
}
