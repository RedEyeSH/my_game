using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button back;

    public GameObject menu;
    public GameObject category;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        back.onClick.AddListener(Back);
        back.onClick.AddListener(Button);
    }

    private void Back()
    {
        menu.SetActive(true);
        category.SetActive(false);
    }

    private void Button()
    {
        button.Play();
    }
}
