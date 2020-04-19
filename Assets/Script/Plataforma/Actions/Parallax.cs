using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float dir;
    public float limiteLeft;
    public float limiteRigth;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(dir, 0, 0) * Time.deltaTime);

        if (transform.position.x < limiteLeft)
        {
            transform.position = new Vector2(limiteRigth, transform.position.y);
        }
        else if (transform.position.x > limiteRigth)
        {
            transform.position = new Vector2(limiteLeft, transform.position.y);
        }
    }
}
