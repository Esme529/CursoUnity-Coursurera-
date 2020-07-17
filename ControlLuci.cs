using System.Collections;
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
    bool enFire1 = false;
    ControlCactus CtrCactus = null;
    int energy;
    public GameObject arma = null;
    public Text txtSalud;

    //public Slider slider; //no, hacer desaparecer corazones

    public int costHitAir = 1;
    public int costHitCactus = 3;
    public int prizeTree = 15;
    
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
        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        {
            if (enFire1 == false)
            {
                enFire1 = true;
                arma.GetComponent<CircleCollider2D>().enabled = false;
                aniLuci.SetTrigger("atacarDePie");
                if (CtrCactus != null)
                {
                    if (CtrCactus.GolpeLuci())
                    {
                        energy += prizeTree;
                        if (energy > 100)
                            energy = 100;
                    }
                    else
                        energy -= costHitCactus;
                }

                else
                    energy -= costHitAir;
                
            }
            

        }
        else
            enFire1 = false;
        if (aniLuci.GetCurrentAnimatorStateInfo(0).IsName("Atacar_pie"))
            aniLuci.SetTrigger("dejarAtacar");

        if(energy<=35||energy<=70)
        {
            if (ControlVidaLuci.controlVidaLuci != null)
                ControlVidaLuci.controlVidaLuci.reducirVida();
            if (energy == 0)
                Destroy(gameObject);
        }
        txtSalud.text = energy.ToString();

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
}
