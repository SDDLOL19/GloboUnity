using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Shader myMaterial;
    public Color _color;
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myMaterial = Shader.Find("GUI/Text Shader");
    }
    void ColorSprite()//Para la sombra
    {
        myRenderer.material.shader = myMaterial;//my render tendra el shader de mi material
        myRenderer.color = _color; //El color de mi render sea igual a nuestra variable color
    }

    // Update is called once per frame
    void Update()
    {
        ColorSprite();
    }
    public void Finish()
    {
        gameObject.SetActive(false);
    }
}
