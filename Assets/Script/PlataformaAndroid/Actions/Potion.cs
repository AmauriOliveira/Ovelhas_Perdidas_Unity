using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposPotion
{
    HEAL,
    MAXHEAL,
    IMUNITY,
    POWERUP,
    DANOAREA
}
public class Potion : MonoBehaviour
{
    public int valorEfeito;
    public ParticleSystem particulasEfeitos;
    private MyGameController _myGameController;
    public int pontos = 250;
    public TiposPotion potion;
    public GameObject danoArea;
    void Start()
    {
        particulasEfeitos.Stop(true);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (potion)
            {
                case TiposPotion.HEAL:
                    if (other.gameObject.GetComponent<PlayerBasicoVida>().hp < other.gameObject.GetComponent<PlayerBasicoVida>().maxHP)
                    {
                        particulasEfeitos.Play(true);
                        other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("AddHP", valorEfeito);
                        Remove();
                    }
                    break;

                case TiposPotion.MAXHEAL:
                    if (other.gameObject.GetComponent<PlayerBasicoVida>().hp < other.gameObject.GetComponent<PlayerBasicoVida>().maxHP)
                    {
                        particulasEfeitos.Play(true);
                        other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("MAXHeal");
                        Remove();
                    }
                    break;

                case TiposPotion.IMUNITY:
                    if (!other.gameObject.GetComponent<PlayerBasicoVida>().imunidade)
                    {
                        particulasEfeitos.Play(true);
                        other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("CronometroImunidadePotion", valorEfeito);
                        Remove();
                    }
                    break;
                case TiposPotion.POWERUP:

                    particulasEfeitos.Play(true);
                    other.gameObject.GetComponent<JogadorControleA>().StartCoroutine("AumentarPlayer");
                    Remove();

                    break;
                case TiposPotion.DANOAREA:

                    particulasEfeitos.Play(true);
                    Instantiate(danoArea, gameObject.transform.position, gameObject.transform.rotation);
                    Remove();

                    break;

                default:
                    Debug.Log("tem algo errado.. na potion.cs");

                    break;
            }
        }
    }
    private void Remove()
    {
        Destroy(gameObject, 0.1f);
        _myGameController.fasePontos += pontos;
    }
}