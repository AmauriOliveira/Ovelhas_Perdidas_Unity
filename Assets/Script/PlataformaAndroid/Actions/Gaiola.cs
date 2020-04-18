using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaiola : MonoBehaviour
{
    private MyGameController _myGameController;
    public ParticleSystem particulasEfeitos;
    private int pontos = 500;

    void Start()
    {
        particulasEfeitos.Stop(true);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
        Debug.Log("Gaiola.cs incompleta");

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_myGameController.temChave && _myGameController.temGaiola && other.gameObject.GetComponent<JogadorControleA>().EstaNoChao())
            {
                _myGameController.temGaiola = false;
                particulasEfeitos.Play(true);
                Destroy(gameObject, 0.5f);
                _myGameController.fasePontos += pontos;
            }
            else
            {
                Debug.Log("Implementar");
            }
        }
    }
}
