using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebrou : MonoBehaviour
{
    private MyGameController _myGameController;
    private RipplePostProcessor camRippleEffect;

    // Start is called before the first frame update
    void Start()
    {
        camRippleEffect = Camera.main.GetComponent<RipplePostProcessor>();
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }

    // Update is called once per frame
    public void SfxHit()
    {
        
        camRippleEffect.RippleEfecct();
        _myGameController.PlaySfx(_myGameController.SfxQuebrou, 0.6f);
    }
}