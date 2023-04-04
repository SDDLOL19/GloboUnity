using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerSystem : MonoBehaviour
{
    bool canJump;
    bool canMove;
    bool canDash;
    public bool dashing;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] TextMeshProUGUI textoEnergy;

    Rigidbody2D playerRigid;
    Animator playerAnim;

    [SerializeField] int cantidadSaltos;
    float dashTime;
    float energyBar = 100;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    private void Start()
    {
        canMove = true;
        canJump = true;
        cantidadSaltos = 0;
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationSystem();
        if (canMove == true)
        {
            MovementSystem();
        }
        EnergySystem();
    }

    public void AnimationSystem()
    {
        if (!dashing)
        {
            playerAnim.SetBool("dash", false);

            if (playerAnim.GetBool("caer") == false)
            {
                if (playerRigid.velocity.x == 0)
                {
                    playerAnim.SetBool("moving", false);
                }

                if (playerRigid.velocity.x != 0)
                {
                    playerAnim.SetBool("moving", true); //Establecerle la animcaion de caminar
                }
            }

            if (playerRigid.velocity.y > 0)
            {
                playerAnim.SetBool("saltar", true);
                playerAnim.SetBool("caer", false);
            }

            if (playerRigid.velocity.y == 0)
            {
                playerAnim.SetBool("saltar", false);
                playerAnim.SetBool("caer", false);
            }

            if (playerRigid.velocity.y < 0)
            {
                playerAnim.SetBool("saltar", false);
                playerAnim.SetBool("caer", true);
            }
        }

        else
        {
            playerAnim.SetBool("dash", true);
        }
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

        if (canDash && energyBar == 100)
        {
            DashMove();
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            dashing = false;
            canDash = true;
            dashTime = 0;
        }
    }

    void MovimientoLateral()
    {
        playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            playerRigid.velocity = new Vector2(-speed, playerRigid.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //para dar la vuelta al personaje
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            playerRigid.velocity = new Vector2(speed, playerRigid.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Salto()
    {
        playerRigid.velocity = Vector2.up * jumpForce;
        canJump = false;
        cantidadSaltos++;
    }

    void DashMove()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            dashTime += Time.deltaTime;

            if (dashTime < 0.35f && Keyboard.current.rightArrowKey.isPressed)
            {
                dashing = true;
                playerRigid.velocity = Vector2.right * speed * 2;
                Shadows.me.Sombras_skill();
            }

            else if (dashTime < 0.35f && Keyboard.current.leftArrowKey.isPressed)
            {
                dashing = true;
                playerRigid.velocity = Vector2.left * speed * 2;
                Shadows.me.Sombras_skill();
            }

            EnergyWaste();
        }
    }

    void EnergySystem()
    {
        textoEnergy.text = energyBar.ToString();
        energyBar = Mathf.Clamp(energyBar, 0, 100);

        if (energyBar < 100)
        {
            energyBar += Time.deltaTime * (100 / 1.5f);
        }
    }

    void EnergyWaste()
    {
        energyBar = 0;
    }

    void DeathSystem()
    {
        canMove = false;
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        playerRigid.velocity = Vector2.zero;
        this.transform.position = spawnPoint.transform.position;
        Invoke("ControlSystem", 1.0f);
    }

    void ControlSystem()
    {
        canMove = !canMove;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            cantidadSaltos = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            DeathSystem();
        }
    }

}
