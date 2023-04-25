using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerDos : Player      //Clase pplayer para que coja todo lo básico y poder cambiar los controles
{
    void Update()
    {
        if (canMove == true)
        {
            MovementSystem();

            if (Keyboard.current.kKey.wasPressedThisFrame)
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

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
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

        if (Keyboard.current.lKey.isPressed)
        {
            DashMove();
        }

        if (Keyboard.current.lKey.wasReleasedThisFrame)
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

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
            this.transform.localScale = new Vector3(-1, 1, 1); //para dar la vuelta al personaje y todos sus aspectos
        }

        if (Keyboard.current.rightArrowKey.isPressed)
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
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    dashing = true;
                    playerRigid.velocity = Vector2.right * speed * 2;
                    Shadows.me.Sombras_skill();
                }

                else if (Keyboard.current.leftArrowKey.isPressed)
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
}
