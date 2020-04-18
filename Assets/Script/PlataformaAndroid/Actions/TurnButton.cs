using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    private Animator buttonAnimator;
    public bool estaLigado;
    public bool onParaOff;
    public bool offParaON;
    public float timerEmSegundos;

    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
        buttonAnimator.SetBool("estaLigado", estaLigado);
    }
    public void mudarStatus()
    {
        estaLigado = !estaLigado;
        buttonAnimator.SetBool("estaLigado", estaLigado);
        if ((onParaOff && estaLigado) || (offParaON && !estaLigado))
        {
            StartCoroutine("Mudar");
        }
    }
    IEnumerator Mudar()
    {
        yield return new WaitForSeconds(timerEmSegundos);
        mudarStatus();
    }
}