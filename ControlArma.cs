using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArma : MonoBehaviour
{
    // Start is called before the first frame update
    ControlLuci ctr;
    void Start()
    {
        ctr = GameObject.Find("Luci").GetComponent<ControlLuci>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("cactus"))
        {
            ctr.SetCrlCactus(other.gameObject.GetComponent<ControlCactus>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("cactus"))
        {
            ctr.SetCrlCactus(null);
        }
    }
}
