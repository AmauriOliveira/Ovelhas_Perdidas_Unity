using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPos : MonoBehaviour
{
    public Text txt;
    public Touch toque;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)//verifica quantos toque foi feito
        {
            toque = Input.GetTouch(0);//pega o primeiro toque
            if (toque.phase == TouchPhase.Began)
            {//começou a tocar
                txt.text = toque.position.ToString();//joga na tela
            }
        }
    }
}
