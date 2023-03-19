using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    bool canJump;

    public Rigidbody2D CR;
    public Animator anim;
    //private Collider2D coll;
    //private enum Estado { Idle, Move, Saltar, Caer, Dash }

    public bool Dash;
    public float Dash_T;

    public bool SaltarAni;
    public bool CaidaAni;

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
        AnimacionSalto();


    }
    public void Movimiento()
    {
        if (Keyboard.current.leftArrowKey.isPressed && !Dash)
        {
            CR.AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            anim.SetBool("moving", true); //Establecerle la animcaion de caminar
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //para dar la vuelta al personaje
        }
        if (Keyboard.current.rightArrowKey.isPressed && !Dash)
        {
            CR.AddForce(new Vector2(1000f * Time.deltaTime, 0));
            anim.SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if(CR.velocity.x != 0)
        {
            anim.SetBool("moving", false);

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
    public void AnimacionSalto()
    {
        if (CR.velocity.y < 0.1)
        {
            SaltarAni = true;
            gameObject.GetComponent<Animator>().SetBool("saltar", true);
        }
        if (CR.velocity.y > -0.1)
        {
            CaidaAni = true;
            gameObject.GetComponent<Animator>().SetBool("caer", true);
        }
    }

    public void DashMove()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Dash_T += 1 * Time.deltaTime;
            if (Dash_T < 0.35f && Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            {

                Dash = true;
                anim.SetBool("dash", true);
                transform.Translate(Vector3.right * 300f * Time.deltaTime);
                Shadows.me.Sombras_skill();
            }
            if (Dash_T < 0.35f && Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
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
            Dash_T = 0;
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
