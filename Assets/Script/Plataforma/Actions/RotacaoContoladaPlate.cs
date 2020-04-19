using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoContoladaPlate : MonoBehaviour
{
    public float velocidade;
    public TurnPlate plate;
    public AudioClip sfx;
    public float volume;
    public AudioSource sfxSource;
    private bool estaVisivel = false;
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
        if (plate.estaLigado && estaVisivel)
        {
            transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
            sfxSource.PlayOneShot(sfx, volume);
        }
    }
}
