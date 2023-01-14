using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button play;

    public GameObject menu;
    public GameObject category;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        play.onClick.AddListener(Play);
        play.onClick.AddListener(Button);
    }

    private void Play()
    {
        category.SetActive(true);
        menu.SetActive(false);
    }

    private void Button()
    {
        button.Play();
    }
}
