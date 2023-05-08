using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    bool partidaAcabada = false;

    public static int[] ultimateEnergy = new int[] { 0, 0 };
    public SpriteRenderer ultimate1;
    public SpriteRenderer ultimate2;
    public Sprite[] spritesUlti;

    [SerializeField] Canvas Pausa;
    [SerializeField] Canvas GanadorUno;
    [SerializeField] Canvas GanadorDos;
    [SerializeField] Canvas Empate;
    bool pausado = false;

    private void Awake()
    {
        Application.targetFrameRate = framerate;
        Pausa.gameObject.SetActive(pausado);
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

        if (Input.GetKeyDown(KeyCode.Escape) && !partidaAcabada) //Keyboard.current.escapeKey.wasPressedThisFrame
        {
            pausado = !pausado;
            ComprobarPausa();
        }
    }

    void MostrarHud()
    {
        textoPuntosJugadorUno.text = puntosJugadorUno.ToString("0"); //Para que no se muestren decimales en el hud
        textoPuntosJugadorDos.text = puntosJugadorDos.ToString("0");
        textoCronometro.text = cronometro.ToString("0");

        ultimate1.sprite = spritesUlti[ultimateEnergy[0]];
        ultimate2.sprite = spritesUlti[ultimateEnergy[1]];
    }

    void ComprobarPausa()
    {
        Pausa.gameObject.SetActive(pausado);

        if (pausado)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    void ReanudarTiempo()
    {
        Time.timeScale = 1;
    }

    public void Pausar()
    {
        pausado = true;
        ComprobarPausa();
    }

    public void Reanudar()
    {
        pausado = false;
        ComprobarPausa();
    }

    void AcabarPartida()
    {
        partidaAcabada = true;

        if (puntosJugadorUno > puntosJugadorDos)
        {
            GanadorUno.gameObject.SetActive(true);
        }

        else if (puntosJugadorDos > puntosJugadorUno)
        {
            GanadorDos.gameObject.SetActive(true);
        }

        else
        {
            Empate.gameObject.SetActive(true);
        }

        Invoke("CargarMenuPrincipal", 3);
    }

    public void CargarMenuPrincipal()
    {
        ReanudarTiempo();
        SceneManager.LoadScene(0);
    }

    public void CargarSeleccionarPersonaje()
    {
        ReanudarTiempo();
        SceneManager.LoadScene(1);
    }

    public void SumarPuntos(int estadoGlobo)
    {
        if (estadoGlobo == 2)
        {
            puntosJugadorDos++;
        }

        else if (estadoGlobo == 1)
        {
            puntosJugadorUno++;
        }
    }
}
