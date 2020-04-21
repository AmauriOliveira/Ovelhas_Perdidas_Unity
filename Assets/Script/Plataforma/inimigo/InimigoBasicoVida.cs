using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBasicoVida : MonoBehaviour
{
    [Header("BAsico")]
    public int hp;
    public bool imunidade = false;
    public float tempoDeImunidade = 0.5f;
    [Space]
    [Header("Efeito")]
    public ParticleSystem particulasEfeitos;
    private RipplePostProcessor camRippleEffect;
    private SpriteRenderer spriteRenderer;
    [Space]
    public int pontos = 400;
    private MyGameController _myGameController;
    public AudioClip SxfEnemyHit;
    [Space]
    [Header("Drops")]
    public GameObject itemDrop1;
    public int chanceDrop1;
    public GameObject itemDrop2;
    public int chanceDrop2;
    [Space]
    public bool isBoss = false;
    private Color mobColor;


    void Start()
    {
        particulasEfeitos.Stop(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        camRippleEffect = Camera.main.GetComponent<RipplePostProcessor>();
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;

        mobColor = isBoss ? spriteRenderer.color : Color.white;
    }
    public void Drop()
    {
        int temp;

        if (itemDrop1 != null)
        {
            temp = Random.Range(0, 100);
            if (temp <= chanceDrop1)
            {
                GameObject Drop1 = Instantiate(itemDrop1, gameObject.transform.position, gameObject.transform.rotation);
            }
        }

        if (itemDrop2 != null)
        {
            temp = Random.Range(0, 100);
            if (temp <= chanceDrop2)
            {
                GameObject Drop2 = Instantiate(itemDrop2, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
    public void SfxHit()
    {
        _myGameController.PlaySfx(SxfEnemyHit, 0.5f);
    }
    IEnumerator CronometroImunidade()
    {
        float temp = tempoDeImunidade / 10;
        particulasEfeitos.Play(true);
        camRippleEffect.RippleEfecct();
        imunidade = true;
        this.gameObject.layer = LayerMask.NameToLayer("Imunidade");
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(temp);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(temp);
        }
        imunidade = false;
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        spriteRenderer.color = mobColor;
    }
}