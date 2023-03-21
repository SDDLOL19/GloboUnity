using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrasilenaMal : MonoBehaviour
{
    bool canJump;

    public Rigidbody2D CR;
    public Animator anim;
    //private Collider2D coll;
    //private enum Estado { Idle, Move, Saltar, Caer, Dash }

    public bool Dash;
    public float Dash_Time;

    private void Start()
    {
        CR = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movimiento();
        Salto();
        DashMove();
        AnimationSystem();
    }

    public void AnimationSystem()
    {
        if (!Dash)
        {
            if (CR.velocity.x == 0)
            {
                anim.SetBool("moving", false);
            }

            if (CR.velocity.x != 0)
            {
                anim.SetBool("moving", true); //Establecerle la animcaion de caminar
            }
        }

        if (CR.velocity.y < 0)
        {
            gameObject.GetComponent<Animator>().SetBool("caer", true);
        }
    }

    public void Movimiento()
    {
        if (Keyboard.current.leftArrowKey.isPressed && !Dash)
        {
            CR.AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //para dar la vuelta al personaje
        }
        if (Keyboard.current.rightArrowKey.isPressed && !Dash)
        {
            CR.AddForce(new Vector2(1000f * Time.deltaTime, 0));
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void Salto()
    {
        if (Keyboard.current.upArrowKey.isPressed && canJump)
        {
            canJump = false;
            CR.AddForce(new Vector2(0, 400f));

        }
    }

    public void DashMove()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Dash_Time += 1 * Time.deltaTime;
            if (Dash_Time < 0.35f && Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            {

                Dash = true;
                anim.SetBool("dash", true);
                transform.Translate(Vector3.right * 300f * Time.deltaTime);
                Shadows.me.Sombras_skill();
            }
            if (Dash_Time < 0.35f && Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            {
                Dash = true;
                anim.SetBool("dash", true);
                transform.Translate(Vector3.left * 300f * Time.deltaTime);
                Shadows.me.Sombras_skill();
            }
            else
            {
                Dash = false;
                anim.SetBool("dash", false);
            }

        }
        else
        {
            Dash = false;
            anim.SetBool("dash", false);
            Dash_Time = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            canJump = true;

        }
    }

}
