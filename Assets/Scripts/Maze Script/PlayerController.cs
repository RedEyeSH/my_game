using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
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
    public bool isOnObject;
    public bool doorKey;
    public bool treasureChestKey;
    public bool isDoorOpen;

    private Rigidbody playerRb;

    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject clipboard;
    public GameObject flashlight;
    public GameObject dKey;
    public GameObject padLock;
    public GameObject doorOne;
    public GameObject doorTwo;
    public GameObject treasureChest;
    public GameObject menuCanvas;
    public GameObject gameOverCanvas;

    public Button resume;
    public Button restart;
    public Button quitESC;
    public Button quit;

    //public AudioSource walk;

    private CameraController cameraControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the component of the Rigidbody
        playerRb = GetComponent<Rigidbody>();

        cameraControllerScript = GameObject.Find("Focal Point").GetComponent<CameraController>();
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

        // If the player holds the right click "clipboard" object is set to true
        if (rightClick && isGameActive && !isGameOver)
        {
            clipboard.SetActive(true);
        }

        // If the player release the right click "clipboard" object is set to false
        if (rightClickUp)
        {
            clipboard.SetActive(false);
        }

        // If the player stand infront of door, then leftClick is set to true
        if (leftClick && doorKey && transform.position.x > 39 && transform.position.z > 44 && isGameActive && !isGameOver)
        {
            Destroy(padLock);
            Destroy(doorOne);
            Destroy(doorTwo);
            doorKey = false;
            isDoorOpen = true; 
        }

        // If the player stand infront of treasure chest, then leftClick is set to true
        if (leftClick && treasureChestKey && transform.position.x > -11 && transform.position.z > -40 && isGameActive && !isGameOver)
        {
            Destroy(treasureChest);
            treasureChestKey = false;
            dKey.SetActive(true);
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

        // If the dungeon door opens then the game range Z, is set to false
        if (!isDoorOpen)
        {
            // If the player is out of the game range Z, "isGameOver" is set to true
            if (player.transform.position.z > isPlayerInGameRange || player.transform.position.z < -isPlayerInGameRange)
            {
                isGameOver = true;
            }
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
        cameraControllerScript.firstPerson = true;
        cameraControllerScript.thirdPerson = false;

        // "First Person Camera" object is set to true while "Third Person Camera" object is set to false
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
    private void ThirdPerson()
    {
        // If the player is on "Third Person Camera" then it set to true while "First Person Camera" is set to false 
        cameraControllerScript.firstPerson = false;
        cameraControllerScript.thirdPerson = true;

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
        if (other.gameObject.CompareTag("DoorKey"))
        {
            doorKey = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("TreasureChestKey"))
        {
            treasureChestKey = true;
            Destroy(other.gameObject); 
        }

        // If the player touches "Enemy" then "isGameOver" becomes to true
        if (other.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
        }

        if (other.gameObject.CompareTag("Entrance") && isDoorOpen)
        {
            SceneManager.LoadScene("Maze_Finish");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is on "Ground", then it becomes to true while "OnObject" is false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isOnObject = false;
        }

        // If the player is on "OnObject", then it becomes to true while "Ground" is false
        if (collision.gameObject.CompareTag("OnObject"))
        {
            isOnGround = false;
            isOnObject = true;
        }
    }
}
