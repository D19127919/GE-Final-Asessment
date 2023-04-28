using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10; //How fast the object moves.
    public float lookSensitivity = 2; //How fast the object looks.

    private Vector2 cameraShift; //This basically stores where the mouse is on screen.

    private Quaternion cameraLook; //This is the actual angle needed for rotating the camera.

    public KeyCode upKey = KeyCode.Space; //What key the player presses to fly up

    public KeyCode downKey = KeyCode.LeftShift; //...and down.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int flydir = 0;
        if (Input.GetKey(upKey))
        {
            flydir = 1;
        }
        else if (Input.GetKey(downKey))
        {
            flydir = -1;
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            cameraShift.x += Input.GetAxis("Mouse X");
            cameraShift.y += Input.GetAxis("Mouse Y"); //Apply mouse movements to variables. Could probably compress this into one line but... meh.
        }

        
        cameraLook = Quaternion.Euler(cameraShift.y * -lookSensitivity, cameraShift.x * lookSensitivity, 0); //Apply said variables to the camera rotation variable.
        
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, flydir * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime); //Move the camera based on keyboard inputs.
        transform.rotation = cameraLook; //Rotate the camera in the proper direction.
    }   
}
