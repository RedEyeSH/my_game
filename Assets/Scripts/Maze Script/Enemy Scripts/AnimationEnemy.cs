using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnemy : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerControllerScript = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.transform.position.x > -26)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isWalkingg", false);
        }

        if (transform.position.x > 112)
        {
            animator.SetBool("isWalkingg", false);

        }
        
    }
}
