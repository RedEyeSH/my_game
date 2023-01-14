using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using Unity.VisualScripting;

public class PlayerControllerX : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    public float speed;
    public float jumpForce;
    public float isPlayerOutGameRangeY = 10;

    public int health = 3;

    public bool isGameActive;
    public bool isGameOver;
    public bool isOnGround;
    public bool isGroundJumped;

    private Rigidbody playerRb;

    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;

    public GameObject menuCanvas;
    public GameObject gameOverCanvas;

    public Button resume;
    public Button restart;
    public Button quitESC;
    public Button quit;

    public TextMeshProUGUI healthText;

    private CameraControllerX cameraControllerXScript;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the component
        playerRb = GetComponent<Rigidbody>();

        // had to be GameObject.Find("Object") in order to get true or false.
        cameraControllerXScript = GameObject.Find("Focal Point").GetComponent<CameraControllerX>();
    }

    // Update is called once per frames
    void Update()
    {
        // Player's input, gets the wsad keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        bool leftClick = Input.GetMouseButtonDown(0);
        //bool rightClick = Input.GetMouseButtonDown(1);

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

        // If the player jumps, isOnGround becomes to false
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


        if (player.transform.position.y < -isPlayerOutGameRangeY)
        {
            transform.position = new Vector3(0, 0, 0);
            health -= 1;
            if (health <= 0)
            {
                isGameOver = true;
            }
        }
        /*
        if (health <= 0)
        {
            isGameOver = true;
        }
        */

        if (isGameOver)
        {
            gameOverCanvas.SetActive(true);
            menuCanvas.SetActive(false);
        }
        healthText.text = "Health: " + health;

        resume.onClick.AddListener(Resume);
        restart.onClick.AddListener(Restart);
        quit.onClick.AddListener(Quit);
        quitESC.onClick.AddListener(Quit);
    }

    private void Resume()
    {
        menuCanvas.SetActive(false);
        isGameActive = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene("Parkour");
    }

    private void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    private void FirstPerson()
    {
        cameraControllerXScript.firstPerson = true;
        cameraControllerXScript.thirdPerson = false;

        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
    private void ThirdPerson()
    {
        cameraControllerXScript.firstPerson = false;
        cameraControllerXScript.thirdPerson = true;

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
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
        }


        if (other.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("OnObject"))
        {
            isOnGround = false;
        }
    }
}
