﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro2d : MonoBehaviour
{
    public Rigidbody2D CarroRB2D;
    public Rigidbody2D RodaFrenteRB2D;
    public Rigidbody2D RodaTrasRB2D;//
    public Rigidbody2D RodaMeioFrenteRB2D;
    public Rigidbody2D RodaMeioTrasRB2D;//
    public float movimento;//não mecher em nada
    public float velocidade = 20;
    // Start is called before the first frame update
    public float TorqueCarro = 10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movimento = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        RodaFrenteRB2D.AddTorque(-movimento * velocidade * Time.fixedDeltaTime);
        RodaTrasRB2D.AddTorque(-movimento * velocidade * Time.fixedDeltaTime);
        RodaMeioFrenteRB2D.AddTorque(-movimento * velocidade * Time.fixedDeltaTime);
        RodaMeioTrasRB2D.AddTorque(-movimento * velocidade * Time.fixedDeltaTime);
        CarroRB2D.AddTorque(-movimento * TorqueCarro * Time.fixedDeltaTime);
    }
}
