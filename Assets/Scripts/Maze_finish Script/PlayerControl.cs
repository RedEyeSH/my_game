using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    public float speed;
    public float jumpForce;
    public float isPlayerInGameRange = 50;
    public float isPlayerOutGameRangeY = 10;

    public bool isGameActive;
    public bool isGameOver;
    public bool isOnGround;

    private Rigidbody playerRb;

    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject clipboard;
    public GameObject menuCanvas;
    public GameObject gameOverCanvas;

    public Button resume;
    public Button restart;
    public Button quitESC;
    public Button quit;

    private CameraControl cameraControlScript;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the component of the Rigidbody
        playerRb = GetComponent<Rigidbody>();

        // had to be GameObject.Find("Object") in order to get true or false.
        cameraControlScript = GameObject.Find("Focal Point").GetComponent<CameraControl>();
    }

    // Update is called once per frames
    void Update()
    {
        // Player's input, gets the wsad keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);
        bool rightClickUp = Input.GetMouseButtonUp(1);

        bool firstPerson = Input.GetKeyDown(KeyCode.U);
        bool thirdPerson = Input.GetKeyDown(KeyCode.I);

        bool gameMenu = Input.GetKeyDown(KeyCode.Escape);

        // If game is active and not game over, player have ability to move
        if (isGameActive && !isGameOver)
        {
            // Player's movements
            transform.Translate(Vector3.forward * speed * verticalInput);
            transform.Translate(Vector3.right * speed * horizontalInput);
        }

        // If the player jumps, "isOnGround" becomes to false
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && isGameActive && !isGameOver)
        {
            Jump();
        }

        // Player changes the position of camera
        if (firstPerson && isGameActive && !isGameOver)
        {
            FirstPerson();
        }

        if (thirdPerson && isGameActive && !isGameOver)
        {
            ThirdPerson();
        }

        // If the player presses "ESC", the game pauses
        if (gameMenu && !isGameOver)
        {
            Menu();
        }

        // If the player holds the right click "clipboard" is set to true
        if (rightClick && isGameActive && !isGameOver)
        {
            clipboard.SetActive(true);
        }

        // If the player let go right click "clipboard" is set to false
        if (rightClickUp)
        {
            clipboard.SetActive(false);
        }

        // If the player is out of the game range X, "isGameOver" is set to true
        if (player.transform.position.x > isPlayerInGameRange || player.transform.position.x < -isPlayerInGameRange)
        {
            isGameOver = true;

        }

        // If the player is out of the game range Y, "isGameOver" is set to true
        if (player.transform.position.y > isPlayerOutGameRangeY || player.transform.position.y < -isPlayerOutGameRangeY)
        {
            isGameOver = true;
        }

        
            // If the player is out of the game range Z, "isGameOver" is set to true
        if (player.transform.position.z > isPlayerInGameRange || player.transform.position.z < -isPlayerInGameRange)
        {
            isGameOver = true;
        }
        

        // If the game is over then the "Game Over Canvas" will appear in the screen
        if (isGameOver)
        {
            gameOverCanvas.SetActive(true);
            menuCanvas.SetActive(false);
            clipboard.SetActive(false);
        }

        // Buttons on click
        resume.onClick.AddListener(Resume); // If the player presses resume button, "isGameActive" becomes to true
        restart.onClick.AddListener(Restart); // Reload the current scene
        quit.onClick.AddListener(Quit); // Move the player to the menu
        quitESC.onClick.AddListener(Quit); // Move the player to the menu
    }

    private void Resume()
    {
        menuCanvas.SetActive(false);
        isGameActive = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene("Maze"); // Load the scene of "Maze"
    }

    private void Quit()
    {
        SceneManager.LoadScene("Menu"); // Load the scene of "Menu"
    }

    private void FirstPerson()
    {
        // If the player is on "First Person Camera" then it set to true while "Third Person Camera" is set to false 
        cameraControlScript.firstPerson = true;
        cameraControlScript.thirdPerson = false;

        // "First Person Camera" object is set to true while "Third Person Camera" object is set to false
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
    private void ThirdPerson()
    {
        // If the player is on "Third Person Camera" then it set to true while "First Person Camera" is set to false 
        cameraControlScript.firstPerson = false;
        cameraControlScript.thirdPerson = true;

        // "Third Person Camera" is set to true while "First Person Camera" is set to false
        thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
    }
    private void Menu()
    {
        menuCanvas.SetActive(true);
        isGameActive = false;
    }

    private void Jump()
    {
        // If the player jumps then "isOnGround" becomes to false
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entrance"))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is on "Ground", then it becomes to true while "OnObject" is false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
