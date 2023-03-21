using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrasilenaMal : MonoBehaviour
{
    bool canJump;

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
        Movimiento();
        Salto();
        DashMove();
        AnimationSystem();
    }

    public void AnimationSystem()
    {
        if (!Dash)
        {
            if (RigidBras.velocity.x == 0)
            {
                AnimBras.SetBool("moving", false);
            }

            if (RigidBras.velocity.x != 0)
            {
                AnimBras.SetBool("moving", true); //Establecerle la animcaion de caminar
            }

            if (RigidBras.velocity.y < 0)
            {
                gameObject.GetComponent<Animator>().SetBool("caer", true);
            }
        }

        else
        {
            AnimBras.SetBool("dash", false);
        }
    }

    public void Movimiento()
    {
        RigidBras.velocity = Vector2.zero;

        if (Keyboard.current.leftArrowKey.isPressed && !Dash)
        {
            RigidBras.velocity = Vector2.left * speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //para dar la vuelta al personaje
        }

        if (Keyboard.current.rightArrowKey.isPressed && !Dash)
        {
            RigidBras.velocity = Vector2.right * speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void Salto()
    {
        if (Keyboard.current.upArrowKey.isPressed && canJump)
        {
            canJump = false;
            RigidBras.AddForce(new Vector2(0, 400f));
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
                AnimBras.SetBool("dash", true);
                transform.Translate(Vector3.right * 300f * Time.deltaTime);
                Shadows.me.Sombras_skill();
            }

            if (Dash_Time < 0.35f && Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
            {
                Dash = true;
                AnimBras.SetBool("dash", true);
                transform.Translate(Vector3.left * 300f * Time.deltaTime);
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
            AnimBras.SetBool("dash", false);
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
