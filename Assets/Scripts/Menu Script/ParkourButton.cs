using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParkourButton : MonoBehaviour
{
    public Button parkour;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        parkour.onClick.AddListener(Parkour);
        parkour.onClick.AddListener(Button);
    }

    private void Parkour()
    {
        SceneManager.LoadScene("Parkour");
    }

    private void Button()
    {
        button.Play();
    }
}
