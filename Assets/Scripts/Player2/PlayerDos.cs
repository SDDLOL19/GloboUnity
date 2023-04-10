using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerDos : MonoBehaviour
{
    bool canJump;
    public bool canMove;
    bool canDash;
    public bool dashing;

    [SerializeField] GameObject punchCollision;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] TextMeshProUGUI textoEnergy;

    Rigidbody2D playerRigid;
    Animator playerAnim;

    [SerializeField] int cantidadSaltos;
    float dashTime;
    public float energyBar = 100;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    private void Start()
    {
        canMove = true;
        canJump = true;
        cantidadSaltos = 0;
        punchCollision.SetActive(false);   //Colision desactivada para evitar que reaccionen a ella al empezar
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationSystem();
        if (canMove == true)
        {
            MovementSystem();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                NormalPunching();
            }   
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

        if (Mouse.current.rightButton.isPressed)
        {
            DashMove();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
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

    void Salto()
    {
        playerRigid.velocity = Vector2.up * jumpForce;
        canJump = false;
        cantidadSaltos++;
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

    void NormalPunching()     //Hace el puñetazo. El detectar el jugador contrario lo hace el collision2D del TriggerPunch
    {
        canMove = false;
        punchCollision.SetActive(true);   //Activa la colision del puño
        Invoke("DesactivarPunch", 0.21f);
    }

    void DesactivarPunch()         //Desactiva la colisión del puño
    {
        punchCollision.SetActive(false);
        canMove = true;
    }

    public void Aturdirse()
    {
        if (canMove == true)
        {
            canMove = false;
            Invoke("Desaturdirse", 1);
        }      
    }

    void Desaturdirse()
    {
        canMove = true;
    }

    void EnergySystem()       //Sistema de energía
    {
        textoEnergy.text = energyBar.ToString("0"); //Para que no se muestren decimales en el hud
        energyBar = Mathf.Clamp(energyBar, 0, 100);     //Limita la variable para que no baje de 0 o suba de 100

        if (energyBar < 100)
        {
            energyBar += Time.deltaTime * (100 / 1.5f);   //Carga la energía en 1,5 segundos
        }
    }

    public void EnergyWaste()
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

    private void OnCollisionStay2D(Collision2D collision)
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
