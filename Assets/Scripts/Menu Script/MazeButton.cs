using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MazeButton : MonoBehaviour
{
    public Button maze;

    public AudioSource button;

    // Update is called once per frame
    void Update()
    {
        maze.onClick.AddListener(Maze);
        maze.onClick.AddListener(Button);
    }

    private void Maze()
    {
        SceneManager.LoadScene("Maze");
    }

    private void Button()
    {
        button.Play();
    }
}
