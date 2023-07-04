using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject Box;
    [SerializeField]
    private GameObject Box1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Box == null) {
            Box1.SetActive(false);
        }
    }
}
