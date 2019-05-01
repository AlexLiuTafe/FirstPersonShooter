﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public bool isCursorHidden = true;
    public float minPitch = -80f, maxPitch = 80f;
    public Vector2 speed = new Vector2(120f, 120f);//Speed in degrees(per sec)

    private Vector2 euler; // Current euler rotation

    // Start is called before the first frame update
    void Start()
    {
        // Is the cursor supposed to be hidden?
        if(isCursorHidden)
        {
            //Lock and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // ... invisible!
        }
        euler = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the euler with mouse movement
        euler.y += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
        euler.x -= Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
        // Clamp the camera on pitch
        // Apply euler to the player & camera seperately
        transform.parent.localEulerAngles = new Vector3(0, euler.y, 0);
        transform.localEulerAngles = new Vector3(euler.x, 0, 0);
    }
}
