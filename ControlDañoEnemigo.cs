using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDañoEnemigo : MonoBehaviour
{
    Collider2D colliderEnem = null;
    public int delayBajarPuntosEnem = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Abeja") && colliderEnem == null)
        {
            Debug.Log("Colisión con el enemigo");
            colliderEnem = collision;
            Invoke("BajarPuntosEnemigo", delayBajarPuntosEnem);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == colliderEnem)
        {
            Debug.Log("Salir de Colision con el Enemigo");
            colliderEnem = null;
            CancelInvoke("BajarPuntosEnemigo");
        }
    }

    void BajarPuntosEnemigo()
    {
        Debug.Log("BajarPuntosEnemigo");
        colliderEnem.gameObject.GetComponent<ControlEnemio>().BajarPuntosLuciCerca();
    }
}
