using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBack : MonoBehaviour
{
    private MyGameController _myGameController;
    public string msgFeedback;

    void Start()
    {
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _myGameController.StartCoroutine("FeedBack", msgFeedback);
        }
    }
}
