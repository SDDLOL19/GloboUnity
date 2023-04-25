using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCollision : MonoBehaviour
{
    Player miJugador;
    [SerializeField] string jugadorEnemigo = "Player";

    private void Start()
    {
        miJugador = this.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == jugadorEnemigo && miJugador.energyBar == 100)
        {
            miJugador.EnergyWaste();
            collision.gameObject.GetComponent<Player>().Aturdirse();
        }
    }
}
