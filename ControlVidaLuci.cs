using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVidaLuci : MonoBehaviour
{
    public GameObject[] Vidas;
    public Queue<GameObject> VidasCola = new Queue<GameObject>(); //variable tipo fila de supermecado, nos permite poner los objetos en orden
    public static ControlVidaLuci controlVidaLuci;
    int contador = 0;
    // Start is called before the first frame update
    void Start()
    {
        controlVidaLuci = this; //asigname esta variable a todo el script del objeto
        foreach(GameObject g in Vidas)
        {
            VidasCola.Enqueue(g); //aca se colocan en orden
            g.gameObject.SetActive(true); //cada uno de los elementos que tenga la lista Vidas se activa
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reducirVida(int vida)
    {
        if((contador==0 && vida<=70) || (contador==1 && vida <= 35) || (contador==2 && vida <=0))
        {
            GameObject g = VidasCola.Dequeue(); //Aca se desencola (se asigna por orden la vida que toca)
            g.gameObject.SetActive(false); //se desactiva
            VidasCola.Enqueue(g); //se vuelve a montar en la fila
            contador++;
        }
        
    }
}
