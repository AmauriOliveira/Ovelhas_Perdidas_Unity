using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicoVida : MonoBehaviour
{
    public int hp;
    public int maxHP;//pubica para facilitar acesso em outro script
    public bool imunidade = false;
    public float tempoDeImunidade = 1.5f;
    public Material playerMaterial;
    private RipplePostProcessor camRippleEffect;
    private MeshRenderer meshRenderer;
    private IEnumerator coroutine;
    private MyGameController _myGameController;
    
    void Start()
    {
        camRippleEffect = Camera.main.GetComponent<RipplePostProcessor>();
        meshRenderer = GetComponent<MeshRenderer>();
        maxHP = hp;
        playerMaterial.color = new Color(1f, 1f, 1f, 1f);
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    IEnumerator CronometroImunidade()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Imunidade");
        camRippleEffect.RippleEfecct();
        imunidade = true;
        float temp = tempoDeImunidade / 10;
        playerMaterial.color = new Color(1f, 1f, 1f, 0.5f);
        for (int i = 0; i < 5; i++)
        {
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(temp);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(temp);
        }
        playerMaterial.color = new Color(1f, 1f, 1f, 1f);
        imunidade = false;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator CronometroImunidadePotion(int time)
    {
        this.gameObject.layer = LayerMask.NameToLayer("Imunidade");
        imunidade = true;

        playerMaterial.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(time);
        playerMaterial.color = new Color(1f, 1f, 1f, 1f);
        imunidade = false;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator removeHP(int dano)
    {
        _myGameController.PlaySfx(_myGameController.SxfHit, 0.8f);
        for (int i = dano; i > 0; i--)
        {
            yield return new WaitForSeconds(0.05f);
            if (hp > 0)
            {
                hp--;
            }
        }
    }
    IEnumerator AddHP(int cura)
    {
        for (int i = 0; i < cura; i++)
        {
            yield return new WaitForSeconds(0.05f);
            if (hp < maxHP)
            {
                hp++;
            }
        }
    }
    IEnumerator MAXHeal()
    {
        while (hp < maxHP)
        {
            yield return new WaitForSeconds(0.05f);
            hp++;
        }
    }
}