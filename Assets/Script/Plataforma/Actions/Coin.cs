using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MyGameController _myGameController;
    public ParticleSystem particulasEfeitos;
    public int valor = 1;
    public int pontos = 100;

    // Start is called before the first frame update
    void Start()
    {
        particulasEfeitos.Stop(true);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _myGameController.coin += valor;
            particulasEfeitos.Play(true);
            _myGameController.fasePontos += pontos;
            _myGameController.PlaySfxArray(_myGameController.SxfCoin, 0.8f);
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject, 0.2f);
        }

    }
}
