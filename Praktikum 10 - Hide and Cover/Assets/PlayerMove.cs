﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Variables
    public static Vector3 playerPos;

    //Just for Movement
    public float speed = 400f;
    private Rigidbody rb;

    //For facing the Mouse
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        StartCoroutine(TrackPlayer());
    }

    IEnumerator TrackPlayer()
    {
        while (true)
        {
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement START
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Movement is our direction vector

        rb.AddForce(movement * speed);
        //Movement END

        //Face Mouse START
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        //Face Mouse END
    
    }
}
