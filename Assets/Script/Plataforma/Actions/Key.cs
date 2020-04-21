using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private MyGameController _myGameController;
    public ParticleSystem particulasEfeitos;
    private int pontos = 400;

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
            if (!_myGameController.temChave)
            {
                _myGameController.PlaySfx(_myGameController.SxfKey, 1);
                _myGameController.temChave = true;
                particulasEfeitos.Play(true);
                _myGameController.fasePontos += pontos;
                _myGameController.StartCoroutine("FeedBack", "Você Encontrou uma chave");

                Destroy(gameObject, 0.2f);
            }
            else
            {
                _myGameController.PlaySfx(_myGameController.SxfKey, 1);
                particulasEfeitos.Play(true);
                _myGameController.fasePontos += (pontos + 200);//200 bonus por ter achado outras chaves
                _myGameController.StartCoroutine("FeedBack", "Você Encontrou outra chave");
                
                Destroy(gameObject, 0.2f);
            }

        }
    }
}
