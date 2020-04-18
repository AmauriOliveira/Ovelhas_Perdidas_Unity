using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Plataforma.Move
{
    
    public class Direita : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public static bool moveDireita;
        public void OnPointerDown(PointerEventData eventData)
        {
            moveDireita = true;
            Esquerda.moveEsquerda = false;
        }

        //Do this when the mouse click on this selectable UI object is released.
        public void OnPointerUp(PointerEventData eventData)
        {
            moveDireita = false;
        }

    }
}
