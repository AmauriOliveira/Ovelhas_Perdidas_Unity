using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [Space]
    [Header("Canvas")]
    public Canvas canvasPrincipal;
    public Canvas canvasKeys;
    public Canvas canvasSobre;
    public float keyPosX = 17.92f;
    public float sobrePosX = 35.84f;
    private Camera cam;
    [Space]

    [Header("Som")]

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip SfxMenu;
    public AudioClip SfxAlert;
    public AudioClip SfxClick;
    void Start()
    {
        cam = Camera.main;
    }
    public void Jogar()
    {
        sfxSource.PlayOneShot(SfxAlert, 1);
        Avancar();
    }
    public void Controle()
    {
        sfxSource.PlayOneShot(SfxMenu, 1);
        canvasPrincipal.enabled = false;
        canvasKeys.enabled = true;
        cam.transform.position = new Vector3(keyPosX, cam.transform.position.y, cam.transform.position.z);
    }
    public void Sobre()
    {
        sfxSource.PlayOneShot(SfxMenu, 1);
        canvasPrincipal.enabled = false;
        canvasSobre.enabled = true;
        cam.transform.position = new Vector3(sobrePosX, cam.transform.position.y, cam.transform.position.z);
    }
    public void Sair()
    {
        sfxSource.PlayOneShot(SfxAlert, 1);
        Application.Quit();
    }
    private void Avancar()
    {
        //Implementar
        SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
    }
    public void Voltar()
    {
        sfxSource.PlayOneShot(SfxClick, 1);
        canvasKeys.enabled = false;
        canvasSobre.enabled = false;
        canvasPrincipal.enabled = true;
        cam.transform.position = new Vector3(0, cam.transform.position.y, cam.transform.position.z);
    }
    public void JogarTour()
    {
        sfxSource.PlayOneShot(SfxClick, 1);
        SceneManager.LoadScene("Tutorial");
    }
}