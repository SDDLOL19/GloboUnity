using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPersonajes : MonoBehaviour
{
    [SerializeField] Sprite[] spritesPersonajes;
    [SerializeField] SpriteRenderer[] spritesJugadores;
    [SerializeField] GameObject[] posicionOpciones;
    [SerializeField] GameObject[] posicionEscenarios;
    [SerializeField] GameObject[] rebordes;

    bool PersonajeUnoSeleccionado;
    bool PersonajeDosSeleccionado;
    bool EscenarioSeleccionado;

    private void Awake()
    {
        SpritesManager.personajeJugadorUno = 1;
        SpritesManager.personajeJugadorDos = 1;
        SpritesManager.fondoEscenario = 1;
        PersonajeUnoSeleccionado = false;
        PersonajeDosSeleccionado = false;
        EscenarioSeleccionado = false;
    }

    void Update()
    {
        PosicionRebordes();
        ControlesOpcionesPersonajes();
        LimitarVariables();

    }

    void LimitarVariables()
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

        //Límites jugador 2
        if (SpritesManager.personajeJugadorDos < 0)
        {
            SpritesManager.personajeJugadorDos = 0;
        }

        if (SpritesManager.personajeJugadorDos > spritesPersonajes.Length - 1)
        {
            SpritesManager.personajeJugadorDos = (spritesPersonajes.Length - 1);
        }

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
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno += 3;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno--;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorUno++;
        }

        //Controles jugador 2
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos -= 3;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos += 3;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos--;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SpritesManager.personajeJugadorDos++;
        }
    }

    void ControlesOpcionesFondo()
    {

    }

    void PosicionRebordes()
    {
        rebordes[0].transform.position = posicionOpciones[SpritesManager.personajeJugadorUno].transform.position;
        rebordes[1].transform.position = posicionOpciones[SpritesManager.personajeJugadorDos].transform.position;
        rebordes[2].transform.position = posicionOpciones[SpritesManager.fondoEscenario].transform.position;
    }
}
