using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimates : MonoBehaviour
{
    public GameObject hitboxUltimatePunch;
    public GameObject objetoLanzable;

    public Sprite[] spritesLanzables;

    Player thisPlayer;

    private void Awake()
    {
        hitboxUltimatePunch.SetActive(false);
        thisPlayer = this.gameObject.GetComponent<Player>();
    }

    public void Ejecutar()
    {
        thisPlayer.canMove = false;

        if (thisPlayer.numeroSkin == 0 || thisPlayer.numeroSkin == 1 || thisPlayer.numeroSkin == 2 || thisPlayer.numeroSkin == 3 || thisPlayer.numeroSkin == 7 || thisPlayer.numeroSkin == 9)
        {
            LanzarObjeto();
        }

        else
        {
            Golpear();
        }
    }

    void LanzarObjeto()
    {
        GameObject objetoLanzado;

        objetoLanzado = Instantiate(objetoLanzable, this.transform.position + new Vector3(0,0.4f,0), Quaternion.Euler(this.transform.up));
        objetoLanzado.tag = "Ultimate" + thisPlayer.numeroPlayer;
        objetoLanzado.GetComponent<SpriteRenderer>().sprite = spritesLanzables[thisPlayer.numeroSkin];
        objetoLanzado.GetComponent<Lanzable>().numeroJugadorContrario = thisPlayer.numeroEnemigo;
        objetoLanzado.GetComponent<Lanzable>().player = thisPlayer.transform;
        Invoke("DejarDeGolpear", 0.5f);
    }

    void Golpear()
    {
        hitboxUltimatePunch.SetActive(true);
        Invoke("DejarDeGolpear", 0.5f);
    }

    void DejarDeGolpear()
    {
        hitboxUltimatePunch.SetActive(false);
        thisPlayer.playerAnim.Play("Idle");
        thisPlayer.PermitirMovimiento();
    }
}
