using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public int gravityState = 3;
    public Rigidbody2D alvoRB2D;
    // Start is called before the first frame update
    void Start()
    {
        alvoRB2D = GetComponent<Rigidbody2D>();//carrega sozinho o RG2D
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (gravityState == 0)
        {
            Physics2D.gravity = new Vector2(-1.0f, 0);
        }
        if (gravityState == 1)
        {
            Physics2D.gravity = new Vector2(0, 1.0f);//teto
        }
        if (gravityState == 2)
        {
            Physics2D.gravity = new Vector2(1.0f, 0);
        }
        if (gravityState == 3)
        {
            Physics2D.gravity = new Vector2(0, -1.0F);//normal
        }
        
        //Physics2D.gravity = Quaternion.Euler(0, 0,-180) * Physics2D.gravity;
        Debug.Log(alvoRB2D .velocity.y);//returna a velocidade y
        Debug.Log(alvoRB2D .velocity.x);//returna a velocidade x
    }
}