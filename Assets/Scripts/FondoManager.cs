using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{
    public SpriteRenderer fondo;
    public SpriteRenderer[] plataformas;
    public SpriteRenderer plataformaGrande;
    public SpriteRenderer[] pinchos;

    public Sprite[] spritesFondos;
    public Sprite[] spritesPlataformas;
    public Sprite[] spritesplataformaGrande;
    public Sprite[] spritesPinchos;

    public AudioClip[] canciones;
    public AudioSource emiteMusica;

    private void Awake()
    {
        emiteMusica.clip = canciones[SpritesManager.fondoEscenario - 1];
    }

    void Start()
    {
        emiteMusica.Play();
        fondo.sprite = spritesFondos[SpritesManager.fondoEscenario-1];

        for (int i = 0; i < plataformas.Length; i++)
        {
            plataformas[i].sprite = spritesPlataformas[SpritesManager.fondoEscenario-1];
        }

        plataformaGrande.sprite = spritesplataformaGrande[SpritesManager.fondoEscenario-1];

        for (int i = 0; i < pinchos.Length; i++)
        {
            pinchos[i].sprite = spritesPinchos[SpritesManager.fondoEscenario-1];
        }
    }
}
