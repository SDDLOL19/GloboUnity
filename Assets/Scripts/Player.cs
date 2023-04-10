using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    public bool canJump;
    public bool canMove;
    public bool canDash;
    public bool dashing;

    [SerializeField] GameObject punchCollision;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] TextMeshProUGUI textoEnergy;

    public Rigidbody2D playerRigid;
    Animator playerAnim;

    public int cantidadSaltos;
    public float dashTime;
    public float energyBar = 100;
    public float speed = 6;
    [SerializeField] float jumpForce = 12;

    private void Start()
    {
        canMove = true;
        canJump = true;
        cantidadSaltos = 0;
        punchCollision.SetActive(false);   //Colision desactivada para evitar que reaccionen a ella al empezar
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
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
        playerRigid.velocity = Vector2.up * jumpForce;
        canJump = false;
        cantidadSaltos++;
    }

    public void NormalPunching()     //Hace el puñetazo. El detectar el jugador contrario lo hace el collision2D del TriggerPunch
    {
        canMove = false;
        punchCollision.SetActive(true);   //Activa la colision del puño
        Invoke("DesactivarPunch", 0.21f);
    }

    void DesactivarPunch()         //Desactiva la colisión del puño
    {
        punchCollision.SetActive(false);
        PermitirMovimiento();
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
        PermitirMovimiento();
    }

    public void EnergySystem()       //Sistema de energía
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
        Invoke("PermitirMovimiento", 1.0f);
    }

    void PermitirMovimiento()
    {
        canMove = true;
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
