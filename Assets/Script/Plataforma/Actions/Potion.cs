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
    private RipplePostProcessor camRippleEffect;
    void Start()
    {
        particulasEfeitos.Stop(true);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
        camRippleEffect = Camera.main.GetComponent<RipplePostProcessor>();
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
                        _myGameController.PlaySfx(_myGameController.SxfHeal, .7f);
                    }
                    break;

                case TiposPotion.MAXHEAL:
                    if (other.gameObject.GetComponent<PlayerBasicoVida>().hp < other.gameObject.GetComponent<PlayerBasicoVida>().maxHP)
                    {
                        particulasEfeitos.Play(true);
                        other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("MAXHeal");
                        Remove();
                        _myGameController.PlaySfx(_myGameController.SxfHeal, 1);
                    }
                    break;

                case TiposPotion.IMUNITY:
                    if (!other.gameObject.GetComponent<PlayerBasicoVida>().imunidade)
                    {
                        particulasEfeitos.Play(true);
                        other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("CronometroImunidadePotion", valorEfeito);
                        Remove();
                        _myGameController.PlaySfx(_myGameController.SfxImunite, 1);
                    }
                    break;
                case TiposPotion.POWERUP:

                    particulasEfeitos.Play(true);
                    other.gameObject.GetComponent<JogadorControleA>().StartCoroutine("AumentarPlayer");
                    Remove();
                    _myGameController.PlaySfx(_myGameController.SfxSize, 1);

                    break;
                case TiposPotion.DANOAREA:

                    particulasEfeitos.Play(true);
                    danoArea.gameObject.gameObject.SetActive(true);
                    _myGameController.PlaySfx(_myGameController.SfxExplosion, 1);
                    camRippleEffect.RippleEfecct();

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
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject, 0.5f);
        _myGameController.fasePontos += pontos;
    }
}