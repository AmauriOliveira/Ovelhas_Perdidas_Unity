using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InimigoSliderHorizontal : MonoBehaviour
{
    private Animator enemyAnimator;
    private SliderJoint2D sliderJoint2;
    private SpriteRenderer enemySpriteRenderer;
    private JointMotor2D tempMotor;
    private bool enemyEstaVivo = true;
    private bool dir = true;
    private bool contador = false;
    private InimigoBasicoVida inimigoBasicoVida;
    private MyGameController _myGameController;
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        sliderJoint2 = GetComponent<SliderJoint2D>();
        tempMotor = sliderJoint2.motor;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        inimigoBasicoVida = GetComponent<InimigoBasicoVida>();
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;

        if (tempMotor.motorSpeed < 0)
        {
            dir = false;
        }
        else if (tempMotor.motorSpeed >= 0)
        {
            dir = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inimigoBasicoVida.hp <= 0)
        {
            enemyEstaVivo = false;
        }
        if (enemyEstaVivo)
        {

            if (sliderJoint2.limitState == JointLimitState2D.LowerLimit && dir == false)
            {
                dir = true;
                tempMotor.motorSpeed *= -1;
                sliderJoint2.motor = tempMotor;
            }
            if (sliderJoint2.limitState == JointLimitState2D.UpperLimit && dir == true)
            {
                dir = false;
                tempMotor.motorSpeed *= -1;
                sliderJoint2.motor = tempMotor;
            }
            enemySpriteRenderer.flipX = dir;
        }
        else
        {
            if (!contador)
            {
                inimigoBasicoVida.Drop();
                _myGameController.PlaySfx(_myGameController.SxfBatDie, 1);
                contador = true;
                enemyAnimator.SetBool("die", true);
                Destroy(gameObject.GetComponent<SliderJoint2D>());
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<Collider2D>());
                _myGameController.fasePontos += inimigoBasicoVida.pontos;
                Destroy(gameObject, 1);
            }
        }
    }
}
