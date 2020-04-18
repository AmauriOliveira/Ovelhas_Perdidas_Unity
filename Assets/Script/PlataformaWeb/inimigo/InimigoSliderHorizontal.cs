using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InimigoSliderHorizontal : MonoBehaviour
{
    public SliderJoint2D sliderJoint2;
    public SpriteRenderer mySpriteRenderer;
    public JointMotor2D tempMotor;
    public int distanciaDoChao = 3;
    public LayerMask LayersBala = -1;
    private bool dir = true;//caso true indo, caso false voltando..
    // Start is called before the first frame update
    void Start()
    {
        sliderJoint2 = GetComponent<SliderJoint2D>();
        tempMotor = sliderJoint2.motor;
        mySpriteRenderer = GetComponent<SpriteRenderer>();

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
        mySpriteRenderer.flipX = dir;//faz a face virar junto ao movimento
        /* 
                RaycastHit2D hitando = Physics2D.Raycast(transform.position + Vector3.right,transform.position - Vector3.left, 1, LayersBala);
               // RaycastHit2D hitando = Physics2D.Linecast(transform.position + Vector3.up * distanciaDoChao, transform.position - Vector3.up * distanciaDoChao, LayersBala,-2.0f,2.0f);
               // Debug.DrawLine(transform.position + Vector3.up * distanciaDoChao, transform.position - Vector3.up * distanciaDoChao);
                if (hitando.collider != null)
                {
                    Debug.Log("hit");
                    Destroy(gameObject);
                } */
        RaycastHit2D hits = Physics2D.Raycast(transform.position + Vector3.up * distanciaDoChao, transform.position - Vector3.up * distanciaDoChao, distanciaDoChao, LayersBala);

        if (hits.collider != null)
        {
            Debug.Log(hits.collider.ToString());
            Destroy(gameObject);
        }
    }
}
