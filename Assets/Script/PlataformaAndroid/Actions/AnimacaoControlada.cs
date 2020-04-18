using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoControlada : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator ani;
    public TurnButton button;


    void Update()
    {
        if (button.estaLigado)
        {
            ani.SetBool("die", true);
        }
        else
        {
            ani.SetBool("die", false);
        }


    }
}
