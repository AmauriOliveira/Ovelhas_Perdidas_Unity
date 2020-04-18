using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efeito : MonoBehaviour
{
    public SpriteRenderer sofreEfeito;
    public bool ativo = false;
    private float eulerAngZ;
    public int anguloLimite = 320;//devine até onde vai rotacinar
    public float velocidade = -0.2f;//define a velocidade de rotaciona
      void Update()
    {
        eulerAngZ = sofreEfeito.transform.localEulerAngles.z;//retorna a rotação me angulos no eixo Z

        if (ativo)
        {
            sofreEfeito.transform.Rotate(new Vector3(0, 0, velocidade));
        }
        if (ativo && eulerAngZ < anguloLimite)
        {
            ativo = !ativo;
        }
    }
    private void OnTriggerEnter2D()//quando encontar
    {
        if (!ativo)
        {
            sofreEfeito.color = Color.blue;
            ativo = true;
        }
    }
}
