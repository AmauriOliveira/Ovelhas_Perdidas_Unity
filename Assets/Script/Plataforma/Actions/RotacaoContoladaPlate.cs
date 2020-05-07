using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoContoladaPlate : MonoBehaviour
{
    public float velocidade;
    public TurnPlate plate;
    private AudioSource sfxSource;
    private bool estaVisivel = false;
    public bool isInverso = false;
    public bool emiteSom = false;
    public float limite;

    void Start()
    {
        if (emiteSom)
        {
            sfxSource = gameObject.GetComponent<AudioSource>();
            sfxSource.Stop();
        }
    }
    private void OnBecameVisible()
    {
        estaVisivel = true;
    }
    private void OnBecameInvisible()
    {
        estaVisivel = false;
    }
    void Update()
    {
        bool temp = plate.estaLigado;
        if (isInverso)
        {
            temp = !temp;
        }
        if (temp)
        {
            if (velocidade > 0 && limite != 0 && transform.localEulerAngles.z <= limite)
            {
                transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
            }
            else if (velocidade < 0 && limite != 0 && transform.localEulerAngles.z >= limite)
            {
                transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
            }
            else if (velocidade != 0 && limite == 0)
            {
                transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
            }

            if (estaVisivel && emiteSom)
            {
                sfxSource.Play();
            }
        }
    }
}