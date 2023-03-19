using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movement = new Vector3(0, 0);
    Rigidbody2D playerRigidbody;
    public int speed;
    public int jumpForce;

    public GameObject spawnPoint;
    bool canMove;

    void Start()
    {
        canMove = true;
        playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            PlayerControl();
        }
    }

    void PlayerControl()
    {
        playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            movement = new Vector2(-speed, 0);
            PlayerMovement();
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement = new Vector2(speed, 0);
            PlayerMovement();
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            PlayerJump();
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {

        }
    }

    void PlayerMovement()
    {
        playerRigidbody.velocity += movement;
    }

    void PlayerJump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
    }

    void DeathSystem()
    {
        canMove = false;
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        playerRigidbody.velocity = new Vector2(0, 0);
        this.transform.position = spawnPoint.transform.position;
        Invoke("ControlSystem", 1.0f);
    }

    void ControlSystem()
    {
        canMove = !canMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            DeathSystem();
        }
    }
}
