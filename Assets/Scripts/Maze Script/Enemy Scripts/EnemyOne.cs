using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent enemy;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy follows the player
        enemy.SetDestination(playerTransform.position);
        animator.SetBool("", true);
    }
}
