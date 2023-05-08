using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SpritesManager.personajeJugadorUno = 5;
        SpritesManager.personajeJugadorDos = 5;
        SpritesManager.fondoEscenario = 2;

        ColocarRebordesJugadorUno();
        ColocarRebordesJugadorDos();
        ColocarRebordesFondo();
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

        if (Input.GetKeyDown(KeyCode.Escape)) // Keyboard.current.escapeKey.wasPressedThisFrame
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
            if (Input.GetKeyDown(KeyCode.Space)) //Keyboard.current.enterKey.wasPressedThisFrame
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
        if (Input.GetKeyDown(InputStuff.Arriba[0])) //Keyboard.current.wKey.wasPressedThisFrame
        {
            SpritesManager.personajeJugadorUno -= 3;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Ulti[0])) //Keyboard.current.sKey.wasPressedThisFrame
        {
            SpritesManager.personajeJugadorUno += 3;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Izquierda[0])) //Keyboard.current.aKey.wasPressedThisFrame
        {
            SpritesManager.personajeJugadorUno--;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Derecha[0]))
        {
            SpritesManager.personajeJugadorUno++;
            LimitarVariablesJugadorUno();
            ColocarRebordesJugadorUno();
            ActualizarPersonajes();
        }

        //Controles jugador 2
        if (Input.GetKeyDown(InputStuff.Arriba[1]))
        {
            SpritesManager.personajeJugadorDos -= 3;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Ulti[1]))
        {
            SpritesManager.personajeJugadorDos += 3;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Izquierda[1]))
        {
            SpritesManager.personajeJugadorDos--;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

        if (Input.GetKeyDown(InputStuff.Derecha[1]))
        {
            SpritesManager.personajeJugadorDos++;
            LimitarVariablesJugadorDos();
            ColocarRebordesJugadorDos();
            ActualizarPersonajes();
        }

    }

    void ControlesOpcionesFondo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpritesManager.fondoEscenario = 1;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SpritesManager.fondoEscenario = 2;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SpritesManager.fondoEscenario = 3;
            LimitarVariablesFondo();
            ColocarRebordesFondo();
        }
    }

    void ColocarRebordesJugadorUno()
    {
        rebordes[0].transform.position = new Vector3 (posicionPersonajes[SpritesManager.personajeJugadorUno].transform.position.x, posicionPersonajes[SpritesManager.personajeJugadorUno].transform.position.y, rebordes[0].transform.position.z);
    }

    void ColocarRebordesJugadorDos()
    {
        rebordes[1].transform.position = new Vector3(posicionPersonajes[SpritesManager.personajeJugadorDos].transform.position.x, posicionPersonajes[SpritesManager.personajeJugadorDos].transform.position.y, rebordes[1].transform.position.z);
    }

    void ColocarRebordesFondo()
    {
        rebordes[2].transform.position = new Vector3(posicionEscenarios[SpritesManager.fondoEscenario].transform.position.x, posicionEscenarios[SpritesManager.fondoEscenario].transform.position.y, rebordes[1].transform.position.z);
    }
}
