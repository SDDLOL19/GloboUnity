using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaCarga : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CargarJuego", 5);
    }

    void CargarJuego()
    {
        SceneManager.LoadScene(3);
    }
}
