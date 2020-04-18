using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBasicoVida : MonoBehaviour
{
    public int hp;
    public bool imunidade = false;
    public float tempoDeImunidade = 0.5f;
    public ParticleSystem particulasEfeitos;
    private RipplePostProcessor camRippleEffect;
    private SpriteRenderer spriteRenderer;
  
    public int pontos = 400;

    void Start()
    {
        particulasEfeitos.Stop(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        camRippleEffect = Camera.main.GetComponent<RipplePostProcessor>();

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
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
