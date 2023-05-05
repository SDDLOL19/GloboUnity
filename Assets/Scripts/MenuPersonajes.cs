using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPersonajes : MonoBehaviour
{
    [SerializeField] Sprite[] spritesPersonajes;
    [SerializeField] SpriteRenderer[] spritesJugadores;
    [SerializeField] GameObject[] posicionPersonajes;
    [SerializeField] GameObject[] posicionEscenarios;
    [SerializeField] GameObject[] rebordes;

    private void Awake()
    {
        SpritesManager.personajeJugadorUno = 0;
        SpritesManager.personajeJugadorDos = 0;
        SpritesManager.fondoEscenario = 0;
    }

    private void Start()
    {
        ActualizarPersonajes();
    }

    void Update()
    {
        ControlesOpcionesPersonajes();
        ControlesOpcionesFondo();        
        EmpezarPartida();

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }

    void ActualizarPersonajes()
    {
        spritesJugadores[0].sprite = spritesPersonajes[SpritesManager.personajeJugadorUno];
        spritesJugadores[1].sprite = spritesPersonajes[SpritesManager.personajeJugadorDos];
    }

    void EmpezarPartida()
    {
        if (SpritesManager.personajeJugadorUno != 0 && SpritesManager.personajeJugadorDos != 0 && SpritesManager.fondoEscenario != 0)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    void LimitarVariablesJugadorUno()
    {
        //Límites jugador 1
        if (SpritesManager.personajeJugadorUno < 0)
        {
            SpritesManager.personajeJugadorUno = 0;
        }

        if (SpritesManager.personajeJugadorUno > spritesPersonajes.Length - 1)
        {
            SpritesManager.personajeJugadorUno = (spritesPersonajes.Length - 1);
        }
    }

    void LimitarVariablesJugadorDos()
    {
        //Límites jugador 2
        if (SpritesManager.personajeJugadorDos < 0)
        {
            SpritesManager.personajeJugadorDos = 0;
        }

        if (SpritesManager.personajeJugadorDos > spritesPersonajes.Length - 1)
        {
            SpritesManager.personajeJugadorDos = (spritesPersonajes.Length - 1);
        }
    }

    void LimitarVariablesFondo()
    {
        //Límites fondos
        if (SpritesManager.fondoEscenario < 0)
        {
            SpritesManager.personajeJugadorUno = 0;
        }

        if (SpritesManager.fondoEscenario > posicionEscenarios.Length - 1)
        {
            SpritesManager.personajeJugadorUno = (posicionEscenarios.Length - 1);
        }
    }

    void ControlesOpcionesPersonajes()
    {
        //Controles jugador 1
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno -= 3;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno += 3;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno--;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno++;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        //Controles jugador 2
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos -= 3;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos += 3;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos--;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos++;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }
    }

    void ControlesOpcionesFondo()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            SpritesManager.fondoEscenario = 1;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            SpritesManager.fondoEscenario = 2;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame || Keyboard.current.numpad3Key.wasPressedThisFrame)
        {
            SpritesManager.fondoEscenario = 3;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }
    }

    void ColocarRebordesJugadorUno()
    {
        rebordes[0].transform.position = posicionPersonajes[SpritesManager.personajeJugadorUno].transform.position;
    }

    void ColocarRebordesJugadorDos()
    {
        rebordes[1].transform.position = posicionPersonajes[SpritesManager.personajeJugadorDos].transform.position;
    }

    void ColocarRebordesFondo()
    {
        rebordes[2].transform.position = posicionEscenarios[SpritesManager.fondoEscenario].transform.position;
    }
}
