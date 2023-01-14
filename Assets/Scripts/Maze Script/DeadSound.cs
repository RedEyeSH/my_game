using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    public AudioSource dead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dead.Play();
        }
    }
}
