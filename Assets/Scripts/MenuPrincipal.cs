using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Canvas controles;
    public Button[] botones = new Button[2];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//Keyboard.current.escapeKey.wasPressedThisFrame
        {
            QuitarControles();
        }
    }

    public void CargarControles()
    {
        botones[0].interactable = false;
        botones[1].interactable = false;
        controles.gameObject.SetActive(true);
    }

    void QuitarControles()
    {
        botones[0].interactable = true;
        botones[1].interactable = true;
        controles.gameObject.SetActive(false);
    }

    public void CargarJuego()
    {
        SceneManager.LoadScene(1);
    }
}
