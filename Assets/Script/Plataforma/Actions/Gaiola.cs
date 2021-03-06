﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaiola : MonoBehaviour
{
    private MyGameController _myGameController;
    public ParticleSystem particulasEfeitos;
    private int pontos = 500;
    private bool avisoDado = false;

    void Start()
    {
        particulasEfeitos.Stop(true);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;

    }
    private void OnBecameVisible()
    {
        if (!avisoDado)
        {
            _myGameController.PlaySfx(_myGameController.SfxAlert, 0.8f);
            _myGameController.StartCoroutine("FeedBack", "Olhe a ovelha ali na jaula.");
            avisoDado = true;

        }
        StartCoroutine("Chamar");
    }
    private void OnBecameInvisible()
    {
        StopCoroutine("Chamar");
    }
    IEnumerator Chamar()
    {
        yield return new WaitForSeconds(Random.Range(2, 8));
        _myGameController.PlaySfxArray(_myGameController.SxfOvelhaSad, 0.8f);
        StartCoroutine("Chamar");
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_myGameController.temChave)
            {
                if (_myGameController.temGaiola)
                {
                    if (other.gameObject.GetComponent<JogadorControle>().EstaNoChao())
                    {
                        _myGameController.temGaiola = false;
                        particulasEfeitos.Play(true);
                        _myGameController.fasePontos += pontos;
                        _myGameController.StartCoroutine("FeedBack", "Vá até a ovelha.");
                        _myGameController.PlaySfx(_myGameController.SfxQuebrou, 0.8f);

                        Destroy(gameObject, 0.5f);
                    }
                    else
                    {
                        _myGameController.PlaySfx(_myGameController.SfxAlert, 0.8f);
                        _myGameController.StartCoroutine("FeedBack", "Não pode estar no ar.");
                    }//vazio
                }
            }
            else
            {
                _myGameController.PlaySfx(_myGameController.SfxAlert, 0.8f);
                _myGameController.StartCoroutine("FeedBack", "Precisa encontrar a chave.");
            }
        }
    }
}