using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRmK : MonoBehaviour
{
    public float distance = 10f;
    public bool Go = false;
    public Touch toque;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)//verifica quantos toque foi feito
        {
            toque = Input.GetTouch(0);//pega o primeiro toque
            if (toque.phase == TouchPhase.Began && Go == false)
            {//começou a 
                Go = true;
            }
            else
            {
                Go = false;
            }

            if (Go == true)
            {
                Vector3 touchPosition = new Vector3(toque.position.x, toque.position.y, distance); // переменной записываються координаты мыши по иксу и игрику
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(touchPosition); // переменной - объекту присваиваеться переменная с координатами мыши
                transform.position = objPosition; // и собственно объекту записываються координаты
            }
        }
    }


    /*  void Update()
     {

         if (Input.GetMouseButton(1) && Go == false)
         {
             Go = true;
         }
         else
         {
             Go = false;
         }

         if (Go == true)
         {
             Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // переменной записываються координаты мыши по иксу и игрику
             Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
             transform.position = objPosition; // и собственно объекту записываються координаты
         }
     } */
}

