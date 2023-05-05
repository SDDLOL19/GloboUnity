using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public static Shadows me;
    public GameObject sombra;
    public List<GameObject> pool = new List<GameObject>();//Lista para almacenar todas las sobras
    private float cronometro;//Float para el tiempo en el que se activan las sombras
    public float speed; //para regularlo
    public Color _color;

    private void Awake()
    {
        me = this; //Con esto podemos llamar al script Shadows desde cualquier otro script
    }
    public GameObject GetShadows()
    {
        for (int i = 0; i < pool.Count; i++)//Tamaño de nuestra lista
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;//le damos nuestra posicion
                pool[i].transform.rotation = transform.rotation;//le damos nuestra rotacion
                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;//que el sprite sea igual al nuestro
                pool[i].GetComponent<Solid>()._color = _color;//accedemos a su codigo para el color sea igual al nuestro
                return pool[i];//lo devolvemos
            }
        }
        GameObject obj = Instantiate(sombra, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;//que el sprite sea igual al nuestro
        obj.GetComponent<Solid>()._color = _color;//color igaul al nuestro
        pool.Add(obj);//lo añadimos a la lista
        return obj;

    }
    public void Sombras_skill()
    {
        cronometro += speed * Time.deltaTime;
        if (cronometro>1)
        {
            GetShadows();
            cronometro = 0;
        }
    }
}
