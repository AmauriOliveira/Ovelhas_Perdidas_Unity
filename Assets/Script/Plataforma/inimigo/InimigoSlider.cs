using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoSlider : MonoBehaviour
{
    
    public SliderJoint2D sliderJoint2;
    public JointMotor2D tempMotor;
    private bool dir = true;//caso true indo, caso false voltando..
    // Start is called before the first frame update
    void Start()
    {
        sliderJoint2 = GetComponent<SliderJoint2D>();
        tempMotor = sliderJoint2.motor;
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
              
    }
}
