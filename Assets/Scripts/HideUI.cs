using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UI;

    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
        Hide();
        Show();
    }
    public void Hide()
    {
        UI[0].SetActive(false);
    }
    public void Show()
    {
        UI[0].SetActive(true);
    }
}
