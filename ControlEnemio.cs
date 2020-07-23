using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlEnemio : MonoBehaviour
{
    public float velEne = -1f;
    Rigidbody2D rgbEne;
    Animator animEnem; //referencia a nuestro animator
    public Slider slider;
    public Text txt;
    public int energy = 100;
    public int LuciCollision = 10;

    void Start()
    {
        rgbEne = GetComponent<Rigidbody2D>();
        animEnem = GetComponent<Animator>();
    }

    void Update()
    {
        if(energy<=0)
        {
            energy = 0;
            animEnem.SetTrigger("Morir");
        }
        slider.value = energy;
        txt.text = energy.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vEne = new Vector2(velEne, 0);
        rgbEne.velocity = vEne;

        if (animEnem.GetCurrentAnimatorStateInfo(0).IsName("volandoEnemigo") && UnityEngine.Random.value < 1f /( 60f*3f)) //cambiar al estado de apuntando una vez acad tres segundos
        {
            animEnem.SetTrigger("Atacar");
        }
        else if (animEnem.GetCurrentAnimatorStateInfo(0).IsName("atacarEnemigo")) //solo atacar una de cada 3 veces
        {
            animEnem.SetTrigger("Volar");
        }


    }

    void OnTriggerEnter2D(Collider2D other) //Para el trigger 
    {
        if(other.gameObject.name.Equals("Luci"))
        {
            ControlLuci controlLuci = other.gameObject.GetComponent<ControlLuci>();
            animEnem.SetTrigger("Atacar");
            if (controlLuci != null)
                controlLuci.RecibirAtaque();
        }
        else
            Flip();
    }

    void Flip() //Metodo que cambia el sentido
    {
        velEne *= -1; //cambia el sentido de la velocidad
        var s = transform.localScale;//por medio de var hacemos que s tenga el mismo tipo de lo que está a la derecha de la asignación
        s.x *= -1; //cambiamos el valor de la escala en x para cambiar el sentido
        transform.localScale = s;
    }

    public void BajarPuntosLuciCerca()
    {
        energy -= LuciCollision;
    }
} 
