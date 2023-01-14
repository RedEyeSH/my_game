using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTwo : MonoBehaviour
{
    private float speed = 6f;
    public GameObject player;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 34 && player.transform.position.z > -41)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.x > 48)
            {
                Destroy(gameObject);
            }
        }
    }
}
