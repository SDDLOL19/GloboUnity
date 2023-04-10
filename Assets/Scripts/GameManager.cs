using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;
    [SerializeField] TextMeshProUGUI textoPuntosJugadorUno;
    [SerializeField] TextMeshProUGUI textoPuntosJugadorDos;
    [SerializeField] TextMeshProUGUI textoCronometro;
    public int puntosJugadorUno = 0;
    public int puntosJugadorDos = 0;
    public float cronometro = 180;     //Tres minutos en segundos

    private void Awake()
    {
        Application.targetFrameRate = framerate;
    }

    void Update()
    {
        MostrarHud();
        cronometro -= Time.deltaTime;
        cronometro = Mathf.Clamp(cronometro, 0, 180);      

        if (cronometro <= 0)
        {
            AcabarPartida();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    void MostrarHud()
    {
        textoPuntosJugadorUno.text = puntosJugadorUno.ToString("0"); //Para que no se muestren decimales en el hud
        textoPuntosJugadorDos.text = puntosJugadorDos.ToString("0");
        textoCronometro.text = cronometro.ToString("0");
    }

    void AcabarPartida()
    {
        SceneManager.LoadScene(0);
    }

    public void SumarPuntos(int estadoGlobo)
    {
        if (estadoGlobo == 1)
        {
            puntosJugadorDos++;
        }

        else if (estadoGlobo == 2)
        {
            puntosJugadorUno++;
        }
    }
}
