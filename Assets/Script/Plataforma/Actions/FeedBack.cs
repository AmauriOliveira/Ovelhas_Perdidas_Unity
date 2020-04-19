using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBack : MonoBehaviour
{
    private MyGameController _myGameController;
    public string msgFeedback;
    private bool avisado = false;

    void Start()
    {
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !avisado)
        {
            avisado = true;
            _myGameController.StartCoroutine("FeedBack", msgFeedback);
            StartCoroutine("NovoAviso");
        }
    }
    IEnumerator NovoAviso()
    {
        yield return new WaitForSeconds(5);
        avisado = false;
    }
}
