using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Plataforma.Move
{
    public class Atirar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public static bool tocandoAtirar;
        // Start is called before the first frame update
        public void OnPointerDown(PointerEventData eventData)
        {
            tocandoAtirar = true;
        }

        //Do this when the mouse click on this selectable UI object is released.
        public void OnPointerUp(PointerEventData eventData)
        {
            tocandoAtirar = false;
        }
    }
}
