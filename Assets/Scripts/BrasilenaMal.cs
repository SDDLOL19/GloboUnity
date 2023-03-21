using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrasilenaMal : MonoBehaviour
{
    bool canJump = true;

    Rigidbody2D RigidBras;
    [SerializeField] Animator AnimBras;
    public bool Dash;
    float Dash_Time;
    [SerializeField] float speed;

    private void Start()
    {
        RigidBras = GetComponent<Rigidbody2D>();
        AnimBras = GetComponent<Animator>();
    }

    void Update()
    {
        if (AnimBras.GetBool("caer") == false || AnimBras.GetBool("dash") == false)
        {
            Movimiento();
        }
        Salto();
        DashMove();
        AnimationSystem();
    }

    public void AnimationSystem()
    {
        if (!Dash)
        {
            AnimBras.SetBool("dash", false);

            if (AnimBras.GetBool("caer") == false)
            {
                if (RigidBras.velocity.x == 0)
                {
                    AnimBras.SetBool("moving", false);
                }

                if (RigidBras.velocity.x != 0)
                {
                    AnimBras.SetBool("moving", true); //Establecerle la animcaion de caminar
                }
            }    

            if (RigidBras.velocity.y > 0)
            {
                AnimBras.SetBool("saltar", true);
                AnimBras.SetBool("caer", false);
            }

            if (RigidBras.velocity.y == 0)
            {
                AnimBras.SetBool("saltar", false);
                AnimBras.SetBool("caer", false);
            }

            if (RigidBras.velocity.y < 0)
            {
                AnimBras.SetBool("saltar", false);
                AnimBras.SetBool("caer", true);
            }
        }

        else
        {
            AnimBras.SetBool("dash", true);
        }
    }

    public void Movimiento()
    {
        RigidBras.velocity = new Vector2(0, RigidBras.velocity.y);

        if (Keyboard.current.leftArrowKey.isPressed && !Dash)
        {
            RigidBras.velocity = new Vector2(-speed, RigidBras.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //para dar la vuelta al personaje
        }

        if (Keyboard.current.rightArrowKey.isPressed && !Dash)
        {
            RigidBras.velocity = new Vector2 (speed, RigidBras.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void Salto()
    {
        if (Keyboard.current.upArrowKey.isPressed && canJump)
        {
            canJump = false;
            RigidBras.velocity = Vector2.up * speed;
        }
    }

    public void DashMove()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Dash_Time += Time.deltaTime;
            if (Dash_Time < 0.35f && Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            {
                Dash = true;
                RigidBras.velocity = Vector2.right * speed * 2;
                Shadows.me.Sombras_skill();
            }

            if (Dash_Time < 0.35f && Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            {
                Dash = true;
                RigidBras.velocity = Vector2.left * speed * 2;
                Shadows.me.Sombras_skill();
            }

            else
            {
                Dash = false;
                AnimBras.SetBool("dash", false);
            }
        }

        else
        {
            Dash = false;
            Dash_Time = 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

}
