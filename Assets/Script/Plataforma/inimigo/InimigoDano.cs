using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InimigoDano : MonoBehaviour
{
    public int dano;

    //void OnCollisionEnter2D(Collision2D other)
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<PlayerBasicoVida>().imunidade)
        {
            other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("removeHP",dano);

            other.gameObject.GetComponent<PlayerBasicoVida>().StartCoroutine("CronometroImunidade");
            if (other.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, 0.5f, 0) * 2, ForceMode2D.Impulse);
            }
            else
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1, -0.5f, 0) * 2, ForceMode2D.Impulse);
            }
        }
    }
}