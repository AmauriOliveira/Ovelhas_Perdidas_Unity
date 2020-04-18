using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Plataforma.Move
{

    public class EsquerdaA : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public static bool moveEsquerda;

        public void OnPointerDown(PointerEventData eventData)
        {
            moveEsquerda = true;
        }

        //Do this when the mouse click on this selectable UI object is released.
        public void OnPointerUp(PointerEventData eventData)
        {
            moveEsquerda = false;
        }
    }
}