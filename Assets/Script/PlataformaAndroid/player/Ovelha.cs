using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Ovelha : MonoBehaviour
{
    public bool EstaEspelhado = true;//referencia ao lado que ele começa olhando//
    private Rigidbody2D ovelhaRB2D;//nem precisa colocar nada
    public float velocidade = 2.5f;//velocidade
    public UnityArmatureComponent armatureComponent;
    private MyGameController _myGameController;
    public bool estaLivre;

    void Start()
    {
        ovelhaRB2D = GetComponent<Rigidbody2D>();
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
        _myGameController.ovelhaLivre = false;
        estaLivre = false;
        armatureComponent.animation.Play("Idle_S");
        armatureComponent.animation.timeScale = 0.24f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_myGameController.ovelhaLivre && !estaLivre)
        {
            estaLivre = true;
            armatureComponent.animation.Play("Idle_H");
            armatureComponent.animation.timeScale = 0.24f;
        }
        if (_myGameController.playerTrasnform.position.x < transform.position.x)
        {
            armatureComponent.armature.flipX = true;
        }
        else
        {
            armatureComponent.armature.flipX = false;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !estaLivre && !_myGameController.temGaiola)
        {
            _myGameController.StartCoroutine("OvelhaLivre");
        }
    }
}
