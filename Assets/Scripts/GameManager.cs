using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;

    private void Awake()
    {
        Application.targetFrameRate = framerate;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
}
