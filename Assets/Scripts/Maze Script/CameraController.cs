using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public GameObject player;

    private float speed = 20.0f;
    private float rotatePlayerSpeed = 20.0f;

    public bool firstPerson;
    public bool thirdPerson;

    public float xMouse;

    // Start is called before the first frame update
    void Start()
    {
        // Connects with PlayerController script
        playerControllerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the Mouse X input
        xMouse = Input.GetAxis("Mouse X");

        if (firstPerson)
        {
            // Locks on player's rotation
            CameraLockOnPlayer();

            // First person camera
            FirstPersonCamera();

            // While on "First person camera", thirdPerson becomes to false
            thirdPerson = false;


        }

        if (thirdPerson)
        {
            CameraLockOnPlayer()
                ;
            // Third person camera
            ThirdPersonCamera();

            // While on "Third person camera", firstPerson becomes to false
            firstPerson = false;
        }
    }
    void CameraLockOnPlayer()
    {
        transform.rotation = player.transform.rotation;
    }
    void FirstPersonCamera()
    {
        transform.Rotate(Vector3.up * xMouse * speed);
        player.transform.Rotate(Vector3.up * xMouse * rotatePlayerSpeed);
        transform.position = player.transform.position;
    }
 
    void ThirdPersonCamera()
    {
        transform.Rotate(Vector3.up * xMouse * speed);
        player.transform.Rotate(Vector3.up * xMouse * rotatePlayerSpeed);
        transform.position = player.transform.position;
    }
}
