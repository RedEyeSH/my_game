using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CL_Back_Button : MonoBehaviour
{
    public Button back;

    public GameObject controlCanvas;
    public GameObject menu;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        back.onClick.AddListener(Back);
        back.onClick.AddListener(Button);
    }

    private void Back()
    {
        controlCanvas.SetActive(false);
        menu.SetActive(true);
    }

    private void Button()
    {
        button.Play();
    }
}
