using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    public Text txt;
    public Touch toque;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            toque = Input.GetTouch(0);
            switch (toque.phase)
            {
                case TouchPhase.Began://tocou
                    txt.text = "tocando";
                    break;
                case TouchPhase.Canceled://girou a tela em quanto tocava
                    break;
                case TouchPhase.Ended://soltou
                    txt.text = "soltou";
                    break;
                case TouchPhase.Moved://movendo
                    break;
                    // case TouchPhase.Stationary://parado
                    //    break;
            }
        }
    }
}
