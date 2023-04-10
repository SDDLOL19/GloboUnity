using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloboEnergia : MonoBehaviour
{
    float altura;
    float alturaMax = 100;
    float alturaMin = -82;
    [SerializeField] Player miJugador;
    Animator globoAnimator;

    private void Start()
    {
        globoAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        altura = (miJugador.energyBar * 1.82f) - 82;
        altura = Mathf.Clamp(altura, alturaMin, alturaMax);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, altura, this.transform.localPosition.z);

        if (miJugador.energyBar <= 1)
        {
            Explotar();
        }
    }

    public void Explotar()
    {
        globoAnimator.Play("BarraExplosion");
    }

    public void Reaparecer()
    {
        globoAnimator.Play("Idle");
    }
}
