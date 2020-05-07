using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoContoladaButton : MonoBehaviour
{
    public float velocidade;
    public TurnButton button;
    private AudioSource sfxSource;
    private bool estaVisivel = false;
    public bool emiteSom = false;

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
        if (button.estaLigado)
        {
            transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
            if (emiteSom && estaVisivel)
            {
                sfxSource.Play();
            }
            else if (emiteSom && !estaVisivel)
            {
                sfxSource.Stop();
            }

        }
    }
}