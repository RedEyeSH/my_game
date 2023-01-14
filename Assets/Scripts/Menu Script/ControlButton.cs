using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    public Button control;

    public GameObject menu;
    public GameObject controlCanvas;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        control.onClick.AddListener(Control);
        control.onClick.AddListener(Button);
    }

    private void Control()
    {
        menu.SetActive(false);
        controlCanvas.SetActive(true);
    }

    private void Button()
    {
        button.Play();
    }
}
