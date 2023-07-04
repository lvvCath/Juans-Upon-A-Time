using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayText : MonoBehaviour
{

    public GameObject delayText;
    
   
    void Delay()
    {

        Instantiate(delayText);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Delay();
        Invoke("Delay", 10.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
