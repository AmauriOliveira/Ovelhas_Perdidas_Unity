using UnityEngine;

public class Arrows : MonoBehaviour
{
    public bool estaNoAr = true;
    public int dano = 2;


    void FixedUpdate()
    {
        ArrowEnd();
        ArrowRot();
        Destroy(gameObject, 4);
    }

    void ArrowRot()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void ArrowEnd()
    {
        if (estaNoAr == false)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject, 0.3f);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Arrows" || other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }
        else if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<InimigoBasicoVida>().imunidade)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }
        else
        {

            estaNoAr = false;
        }
        if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<InimigoBasicoVida>().imunidade)
        {

            Destroy(gameObject);
            other.gameObject.GetComponent<InimigoBasicoVida>().hp -= dano;
            other.gameObject.GetComponent<InimigoBasicoVida>().SfxHit();
            other.gameObject.GetComponent<InimigoBasicoVida>().StartCoroutine("CronometroImunidade");

        }
        else if (other.gameObject.tag == "quebra")
        {
            other.gameObject.GetComponent<Quebrou>().SfxHit();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "buttons")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<TurnButton>().mudarStatus();
        }
    }

}