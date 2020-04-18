using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class teste : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// required interface when using the OnPointerDown method.
{
    public bool anda;
    public float velocidade = 2.5f;//velocidade
    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        anda = true;
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        anda = false;
    }
    //void OnMouseDrag()
    //{
    //    print("A");
    // }
    
    void OnMouseDown()
    {
        anda = true;
    }

    void OnMouseUp()
    {
        anda = false;
    }


    void FixedUpdate()
    {
        if (anda)
        {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//aplica força no x
        }

    }
}