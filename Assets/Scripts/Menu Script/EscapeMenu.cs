using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check whether it's active / inactive
            bool isActive = optionsMenu.activeSelf;

            optionsMenu.SetActive(!isActive);
        }

    }
}
