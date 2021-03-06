﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ControlLuci : MonoBehaviour
{
    Rigidbody2D rgb;
    Animator aniLuci;
    public float maxVel = 5f;
    bool haciaDerecha = true;

    public float jumpForce = 7f, Radio = 0.5f;
    public Transform pie;
    public LayerMask sueloLayer;
    public bool saltar;
    //Vector2 JumpForce;
    //bool jumping = false;

    bool enFire1 = false;
    ControlCactus CtrCactus = null;
    int energy;
    public GameObject arma = null;
    public Text txtSalud;
    public bool daño = true;

    //public Slider slider; //no, hacer desaparecer corazones

    public int costHitAir = 1;
    public int costHitCactus = 3;
    public int prizeTree = 15;
    public int costAttack = 15;
    
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        aniLuci = GetComponent<Animator>();
        arma = GameObject.Find("/Luci/arma");
        energy = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!aniLuci.GetCurrentAnimatorStateInfo(0).IsName("Muriendo"))
        {
            if (energy <= 0)
            {
                aniLuci.SetTrigger("morir");
                energy = 0;
            }
        }
        else
            return;

        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        {
            if (enFire1 == false)
            {
                enFire1 = true;
                aniLuci.SetTrigger("atacarDePie");
                if (CtrCactus != null)
                {
                    if (CtrCactus.GolpeLuci())
                    {
                        energy += prizeTree;
                        if (energy > 100)
                            energy = 100;
                        arma.GetComponent<CircleCollider2D>().enabled = false;
                    }
                    else
                    {
                        energy -= costHitCactus;
                        
                    }
                        
                }

                else
                {
                    energy -= costHitAir;
                }
            }
        }
        else
            enFire1 = false;
        if (aniLuci.GetCurrentAnimatorStateInfo(0).IsName("Atacar_pie"))
            aniLuci.SetTrigger("dejarAtacar");

        
        txtSalud.text = energy.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow) && Physics2D.OverlapCircle(pie.position, Radio, sueloLayer))
        {
            saltar = true;
        }

        if (energy <= 70)
        {
            if (ControlVidaLuci.controlVidaLuci != null)
                ControlVidaLuci.controlVidaLuci.reducirVida(energy);
        }
    }

    public void HabilitarTriggerArma() //para llamar en la salida del estado atacar 
    {
        arma.GetComponent<CircleCollider2D>().enabled = true;
    }

    void FixedUpdate()
    {
        float vLuci = Input.GetAxis("Horizontal"); //para acceder al input manager, retorna un valor flotanto entre 0 y 1
        vLuci *= maxVel;
        Vector2 velLuci = new Vector2(0, rgb.velocity.y); //si esta en el aire, cae con la velocidad del Ridgi
        velLuci.x = vLuci; //cambiamos la velocidad en x con respecto a lo que venga como entrada
        rgb.velocity = velLuci;

        aniLuci.SetFloat ("velocidad",velLuci.x);
        if(haciaDerecha && vLuci<0)
        {
            haciaDerecha = false;
            Flip();
        }
        else if(!haciaDerecha && vLuci>0)
        {
            haciaDerecha = true;
            Flip();
        }

        if(saltar)
        {
            rgb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            saltar = false;
        }

        //otro metodo para saltar
        //if (Input.GetAxis("Jump") > 0.01f)
        //{
        //    if (!jumping)
        //    {
        //        jumping = true;
        //        JumpForce.x = 0f;
        //        JumpForce.y = jumpForce;
        //        rgb.AddForce(JumpForce);
        //    }
        //}
        //else
        //    jumping = false; //por el momento esto debe ser despues de terminar la animación
    }

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }


    public void SetCrlCactus(ControlCactus ctr)
    {
        CtrCactus = ctr;
    }

    public void RecibirAtaque()
    {
        energy -= costAttack;
    }
}
