using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCollision2 : MonoBehaviour
{
    [SerializeField] PlayerDos miJugador;
    PlayerUno jugadorUnoScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" && miJugador.energyBar == 100)
        {
            miJugador.EnergyWaste();
            collision.gameObject.GetComponent<PlayerUno>().Aturdirse();
        }
    }
}
