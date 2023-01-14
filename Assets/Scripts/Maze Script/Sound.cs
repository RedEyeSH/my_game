using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource pickup;
 
    private void OnTriggerEnter(Collider other)
    {
        pickup.Play();
    }
}
