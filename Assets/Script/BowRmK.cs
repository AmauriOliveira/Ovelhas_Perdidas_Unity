using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowRmK : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public Transform SpawnArrow;
    public float Force;
    public float RateOfFire = 0.5F;

    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    public Touch toque;

    void FixedUpdate()
    {

        myTime = myTime + Time.deltaTime;
        if (Input.touchCount > 0 && myTime > nextFire)
        {
            toque = Input.GetTouch(0);//pega o primeiro toque
            if (toque.phase == TouchPhase.Ended)
            {//começou a tocar
                nextFire = myTime + RateOfFire;
                GameObject NewArrow = Instantiate(Arrow, SpawnArrow.transform.position, transform.rotation);
                NewArrow.transform.localScale = new Vector2(0.4f, 0.4f);
                NewArrow.GetComponent<Rigidbody2D>().AddForce(NewArrow.transform.right * Force);
                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }
        }

        /*    if (Input.GetButtonUp("Fire1") && myTime > nextFire)
           {
               nextFire = myTime + RateOfFire;
               GameObject NewArrow = Instantiate(Arrow, SpawnArrow.transform.position, transform.rotation);
               NewArrow.transform.localScale = new Vector2(0.4f, 0.4f);
               NewArrow.GetComponent<Rigidbody2D>().AddForce(NewArrow.transform.right * Force);
               nextFire = nextFire - myTime;
               myTime = 0.0F;
           } */
    }
}
