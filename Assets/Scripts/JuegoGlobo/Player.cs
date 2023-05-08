using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpritesManager spriteManejador;
    protected RuntimeAnimatorController[] personajesAnimados;

    public bool canJump;
    public bool canMove;
    public bool canDash;
    public bool dashing;

    [SerializeField] GameObject punchCollision;
    [SerializeField] GameObject spawnPoint;

    public Rigidbody2D playerRigid;
    public Animator playerAnim;

    public int numeroPlayer;
    public int numeroEnemigo;
    public int numeroSkin;


    public int cantidadSaltos;
    public float dashTime;
    public float energyBar = 100;
    public float speed = 6;
    [SerializeField] float jumpForce = 12;

    private void Start()
    {
        canMove = true;
        canJump = true;
        canDash = true;
        cantidadSaltos = 0;
        punchCollision.SetActive(false);   //Colision desactivada para evitar que reaccionen a ella al empezar
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        PlayerSpawn();
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

    public void Salto()
    {
        playerAnim.Play("Saltar");
        playerRigid.velocity = Vector2.up * jumpForce;
        canJump = false;
        cantidadSaltos++;
    }

    public void NormalPunching()     //Hace el pu�etazo. El detectar el jugador contrario lo hace el collision2D del TriggerPunch
    {
        canMove = false;
        if (energyBar == 100)
        {
            playerAnim.Play("Pu�etazo");
        }

        else
        {
            playerAnim.Play("Pu�oGlobo");
        }

        punchCollision.SetActive(true);   //Activa la colision del pu�o
    }

    public void DesactivarPunch()         //Desactiva la colisi�n del pu�o
    {
        punchCollision.SetActive(false);
        playerAnim.Play("Idle");
        PermitirMovimiento();
    }

    public void Aturdirse()
    {
        if (canMove == true)
        {
            canMove = false;

            if (GameManager.ultimateEnergy[numeroEnemigo - 1] < 5)
            {
                GameManager.ultimateEnergy[numeroEnemigo - 1]++;
            }
            
            playerAnim.SetBool("stunted", true);
            playerAnim.Play("Stun");
            Invoke("Desaturdirse", 0.8f);
        }
    }

    void Desaturdirse()
    {
        playerAnim.SetBool("stunted", false);
        PermitirMovimiento();
    }

    public void EnergySystem()       //Sistema de energ�a
    {
        energyBar = Mathf.Clamp(energyBar, 0, 100);     //Limita la variable para que no baje de 0 o suba de 100

        if (energyBar < 100)
        {
            energyBar += Time.deltaTime * (100 / 1.5f);   //Carga la energ�a en 1,5 segundos
        }
    }

    public void EnergyWaste()
    {
        energyBar = 0;
    }

    protected void DeathSystem()
    {
        canMove = false;
        playerRigid.gravityScale = 0;
        playerRigid.velocity = Vector2.zero;
        playerAnim.SetBool("dying", true);
        playerAnim.Play("Muerte");
    }

    public void PlayerSpawn()
    {
        playerAnim.SetBool("dying", false);
        dashing = false;
        playerAnim.Play("Idle");
        playerRigid.gravityScale = 3;
        this.transform.position = spawnPoint.transform.position;
        Invoke("PermitirMovimiento", 1.0f);
    }

    public void PermitirMovimiento()
    {
        canMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            canJump = true;
            cantidadSaltos = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            canJump = false;
        }
    }

    
}
