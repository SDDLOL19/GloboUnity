using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControler : MonoBehaviour
{
    bool vidaglobo; 
    void Update()
    {
        AnimacionGlobo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameObject.SetActive(false); Para hacer que desaparezca
        //GameObject scripter = GameObject.Find("Scripter");//Busca en nuestra escena un objeto que se llame scripter
        //scripter.GetComponent<ScoreManager>().RaiseScore(1);
        ScoreManager.scoreManager.RaiseScore(1);
        vidaglobo = true;

    }
    public void AnimacionGlobo()
    {
        if (vidaglobo)
        {
            gameObject.GetComponent<Animator>().SetBool("GloboMuerte", true);
        }
    }
    
    public void Finish()
    {
        Destroy(transform.parent.gameObject);
    }
}
    