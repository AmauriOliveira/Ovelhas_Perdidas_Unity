using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSobre : MonoBehaviour
{
    public AudioSource sfxSource;
    public void btnMenuPrincipal()
    {
        sfxSource.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Principal");
    }
}