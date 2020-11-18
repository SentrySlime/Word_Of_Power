﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public LayerMask walkable;
    public LayerMask interactable;

    Camera cam;
    public PlayerMotor motor;
    public bool isStill = false;
    public float rotationalSpeed;


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkable))
            {
                motor.MoveToPoint(hit.point);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactable))
            {
                motor.MoveToPoint(hit.point);
            }

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isStill = true;
            motor.ResetPath();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isStill = false;
        }
    }

    public void Turning()
    {
        //Create a ray from the mouse cursos on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Create a RayCastHit variable to store infromation about what was hit by the ray
        RaycastHit floorHit;

        //Perform the raycast and if it hits something on the floor layer
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, walkable))
        {


            //Create a vector from the player to the pint on the floor the raycast from the mouse hit
            Vector3 playerToMouse = floorHit.point - transform.position;

            //Ensure the vector is entirely along the floor plane, by setting the Y = 0f;
            playerToMouse.y = 0f;

            //Create a Quaternion (rotation) based on looking down the vector from the player to the mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationalSpeed * Time.deltaTime);

            //Set the player's rotation to this new rotation
            //rb.MoveRotation(newRotation);


        }
    }
}