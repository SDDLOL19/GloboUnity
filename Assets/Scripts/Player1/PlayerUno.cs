using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUno : Player      //Clase pplayer para que coja todo lo b�sico y poder cambiar los controles
{
    void Update()
    {
        if (canMove == true)
        {
            MovementSystem();

            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                NormalPunching();
            }
        }

        AnimationSystem();
        EnergySystem();
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
            EnergyWaste();
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
                    Shadows.me.Sombras_skill();
                }

                else if (Keyboard.current.aKey.isPressed)
                {
                    dashing = true;
                    playerRigid.velocity = Vector2.left * speed * 2;
                    Shadows.me.Sombras_skill();
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

    void ActivarColision()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                GetComponent<Collider2D>().isTrigger = true;
                Invoke("ActivarColision", 0.3f);
            }
        }
    }
}
