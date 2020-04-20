using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoArea : MonoBehaviour
{
    public int dano = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<InimigoBasicoVida>().imunidade)
        {
            other.gameObject.GetComponent<InimigoBasicoVida>().hp -= dano;
            other.gameObject.GetComponent<InimigoBasicoVida>().StartCoroutine("CronometroImunidade");
        }
    }
}
