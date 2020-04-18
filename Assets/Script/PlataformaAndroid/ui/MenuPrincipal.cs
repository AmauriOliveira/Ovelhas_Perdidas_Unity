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

    void Start()
    {
        cam = Camera.main;
    }
    public void Jogar()
    {
        //imprementar
        Avancar();
    }
    public void Controle()
    {
        canvasPrincipal.enabled = false;
        canvasKeys.enabled = true;
        cam.transform.position = new Vector3(keyPosX, cam.transform.position.y, cam.transform.position.z);

    }
    public void Sobre()
    {
        canvasPrincipal.enabled = false;
        canvasSobre.enabled = true;
        cam.transform.position = new Vector3(sobrePosX, cam.transform.position.y, cam.transform.position.z);

    }
    public void Sair()
    {
        Application.Quit();
    }
    private void Avancar()
    {//implementare
        //SceneManager.LoadScene("Seletor", LoadSceneMode.Single);
        SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
    }
    public void Voltar()
    {
        canvasKeys.enabled = false;
        canvasSobre.enabled = false;
        canvasPrincipal.enabled = true;
        cam.transform.position = new Vector3(0, cam.transform.position.y, cam.transform.position.z);
    }
}
