using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimates : MonoBehaviour
{
    public GameObject hitboxUltimatePunch;
    public GameObject objetoLanzable;
    public GameObject objetoBumerang;

    void Ejecutar()
    {
        if (SpritesManager.personajeJugadorUno == 1 || SpritesManager.personajeJugadorUno == 2 || SpritesManager.personajeJugadorUno == 9)
        {
            LanzarObjeto();
        }

        else if (SpritesManager.personajeJugadorUno == 3 || SpritesManager.personajeJugadorUno == 7)
        {
            LanzarBumerang();
        }

        else
        {
            Golpear();
        }
    }

    void LanzarObjeto()
    {
        Instantiate(objetoLanzable, this.transform.position, Quaternion.Euler(this.transform.right));
    }

    void LanzarBumerang()
    {
        Instantiate(objetoBumerang, this.transform.position, Quaternion.Euler(this.transform.right));
    }

    void Golpear()
    {
        hitboxUltimatePunch.SetActive(true);
        Invoke("DejarDeGolpear", 0.5f);
    }

    void DejarDeGolpear()
    {
        hitboxUltimatePunch.SetActive(false);
    }
}
