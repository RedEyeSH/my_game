using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float speed;
    private bool isOnViewArea = true;

    private float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");

        if (isOnViewArea)
        {
            transform.Rotate(Vector3.up * speed * mouseX);
        }
        
        if (transform.localRotation.y < -0.1f || transform.localRotation.y > 0.1f)
        {
            isOnViewArea = false;
        }
        
        
        
    }
}
