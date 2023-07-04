using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    public GameObject fadeInPanel;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

}
