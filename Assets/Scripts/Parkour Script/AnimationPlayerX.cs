using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationPlayerX : MonoBehaviour
{
    Animator animator;
    private PlayerControllerX playerControllerXScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerControllerXScript = GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forward = Input.GetKey(KeyCode.W);
        bool backward = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        bool running = Input.GetKey(KeyCode.LeftShift);
        bool jumping = Input.GetKey(KeyCode.Space);

        // if the player is pressing W, isWalkingToForward becomes to true
        if (forward && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToForward", true);
        }

        // if the player is not pressing W, isWalkingToForward becomes to false
        if (!forward)
        {
            animator.SetBool("isWalkingToForward", false);
        }

        // if the player is pressing S, isWalkingToBackward becomes to true
        if (backward && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToBackward", true);
        }

        // if the player is not pressing S, isWalkingToBackward becomes to false
        if (!backward)
        {
            animator.SetBool("isWalkingToBackward", false);
        }

        // if the player is pressing A, isWalkingToLeft becomes to true
        if (left && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToLeft", true);
        }

        // if the player is not pressing A, isWalkingToLeft becomes to false
        if (!left)
        {
            animator.SetBool("isWalkingToLeft", false);
        }

        // if the player is pressing D, isWalkingToRight becomes to true
        if (right && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToRight", true);
        }

        // if the player is not pressing D, isWalkingToRight becomes to false
        if (!right)
        {
            animator.SetBool("isWalkingToRight", false);
        }

        // while the player is walking, then running becomes to true if the player presses the left shift. 
        if (forward && running && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isRunning", true);
            playerControllerXScript.speed = 0.151f;
        }

        // if player stop walking or running, then it becomes to false
        if (!forward || !running)
        {
            animator.SetBool("isRunning", false);
            playerControllerXScript.speed = 0.15f;
        }

        // if the player is pressing space, then the player jumps
        if (jumping && playerControllerXScript.isGameActive && playerControllerXScript.isOnGround && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isJumping", true);
        }

        // if the player is not pressing space then the player stop jumping
        if (!jumping)
        {
            animator.SetBool("isJumping", false);
        }

        // if the players press W and A, then it becomes to true
        if (forward && left && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToForwardLeft", true);
        }

        // if player stop going forward or left, then it becomes to false
        if (!forward || !left)
        {
            animator.SetBool("isWalkingToForwardLeft", false);
        }

        // if the player press W and D, then it becomes to true
        if (forward && right && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToForwardRight", true);
        }

        // if the player stop going forward or right, then it becomes to false
        if (!forward || !right)
        {
            animator.SetBool("isWalkingToForwardRight", false);
        }

        // if the player press S and A, then it becomes to true
        if (backward && left && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToBackwardLeft", true);
        }

        // if the player stop going backward or left, then it becomes to false
        if (!backward || !left)
        {
            animator.SetBool("isWalkingToBackwardLeft", false);
        }

        // if the player press S and D, then it becomes to true
        if (backward && right && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isWalkingToBackwardRight", true);
        }

        // if the player stop going backward or right, then it becomes to false
        if (!backward || !right)
        {
            animator.SetBool("isWalkingToBackwardRight", false);
        }

        // if the player is in air or not in the ground, then it is false
        if (!playerControllerXScript.isOnGround && left && playerControllerXScript.isGameActive && !playerControllerXScript.isGameOver)
        {
            animator.SetBool("isFalling", true);
        }

        // if the player is in the ground, then it is true
        if (playerControllerXScript.isOnGround)
        {
            animator.SetBool("isFalling", false);
        }

        if (playerControllerXScript.isGameOver)
        {
            animator.SetBool("isDead", true);
        }
    }
}
