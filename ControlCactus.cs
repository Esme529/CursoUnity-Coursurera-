using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCactus : MonoBehaviour
{
    public int numHitsToFall = 3;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GolpeLuci()
    {
        bool resp = false;
        numHitsToFall--;
        if(numHitsToFall<=0)
        {
            anim.SetTrigger("caer");
            resp = true;
        }
        return resp;
    }
}
