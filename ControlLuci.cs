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

    public Slider slider; //no, hacer desaparecer corazones
    
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        aniLuci = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        {
            if (enFire1 == false)
            {
                enFire1 = true;
                aniLuci.SetTrigger("atacarDePie");
                if (CtrCactus != null)
                    CtrCactus.GolpeLuci();
            }
            else
                enFire1 = false;

        }
        else if (aniLuci.GetCurrentAnimatorStateInfo(0).IsName("Atacar_pie"))
            aniLuci.SetTrigger("velocidad");
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("cactus"))
        {
            SetCrlCactus(other.gameObject.GetComponent<ControlCactus>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("cactus"))
        {
            SetCrlCactus(null);
        }
    }

    public void SetCrlCactus(ControlCactus ctr)
    {
        CtrCactus = ctr;
    }
}
