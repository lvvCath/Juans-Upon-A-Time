using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDelay : MonoBehaviour
{

    public GameObject Option_1;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(HideAndShow(15.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HideAndShow(float delay)
    {
        Option_1.SetActive(false);
        yield return new WaitForSeconds(delay);
        Option_1.SetActive(true);
    }
}

