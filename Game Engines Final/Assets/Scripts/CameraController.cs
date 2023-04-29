using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10; //How fast the object moves.
    public float lookSensitivity = 2; //How fast the object looks.
    public string groundTag = "Ground";

    private Vector2 cameraShift; //This basically stores where the mouse is on screen.

    private Quaternion cameraLook; //This is the actual angle needed for rotating the camera.

    [Header("Keybinds")]
    public KeyCode upKey = KeyCode.Space; //What key the player presses to fly up
    public KeyCode downKey = KeyCode.LeftShift; //...and down.
    public KeyCode freezeKey = KeyCode.LeftControl; //The button to freeze the camera look
    public KeyCode spawnKey = KeyCode.P; //The button to spawn stuff.
    public KeyCode deleteKey = KeyCode.L; //The button to delete stuff.
    private int selection = 1;

    [Header("Spawning")] 
    public GameObject foodObject;
    public GameObject preyObject;
    public GameObject predatorObject;
    public GameObject tree;
    public GameObject rock; //These are what will be instantiated by the create key.
    
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
            flydir = 1; //Move up
        }
        else if (Input.GetKey(downKey))
        {
            flydir = -1; //Move down
        }

        if (!Input.GetKey(freezeKey))
        {
            cameraShift.x += Input.GetAxis("Mouse X");
            cameraShift.y += Input.GetAxis("Mouse Y"); //Apply mouse movements to variables. Could probably compress this into one line but... meh.
        }

        
        cameraLook = Quaternion.Euler(cameraShift.y * -lookSensitivity, cameraShift.x * lookSensitivity, 0); //Apply said variables to the camera rotation variable.
        
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, flydir * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime); //Move the camera based on keyboard inputs.
        transform.rotation = cameraLook; //Rotate the camera in the proper direction.


        if (Input.GetKeyDown(spawnKey))
        {
            RaycastHit hit; //Store what got hit by the raycast.
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity); //Cast a ray.
            
            switch (selection)
            {
                case 1: Instantiate(foodObject, new Vector3(hit.point.x, hit.point.y +1, hit.point.z), Quaternion.identity); break;
                case 2: Instantiate(preyObject, new Vector3(hit.point.x, hit.point.y +1, hit.point.z), Quaternion.identity); break;
                case 3: Instantiate(predatorObject, new Vector3(hit.point.x, hit.point.y +1, hit.point.z), Quaternion.identity); break;
                case 4: Instantiate(tree, new Vector3(hit.point.x, hit.point.y +1, hit.point.z), Quaternion.identity); break;
                case 5: Instantiate(rock, new Vector3(hit.point.x, hit.point.y +1, hit.point.z), Quaternion.identity); break;
                default: break; //Instantiate a prefab based on the selection variable.
            }
        }

        if (Input.GetKeyDown(deleteKey)) //If you're pressing delete...
        {
            RaycastHit hit; //Variable storing what got hit by the raycast.
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity); //Cast a ray.

            if (!hit.collider.CompareTag(groundTag)) //If what it hit was not tagged as a ground object...
            {
                Destroy(hit.collider.gameObject); //Destroy it.
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selection = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selection = 2;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selection = 3;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selection = 4;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selection = 5;
        } //All of these get input and change the selection variable based on the number pushed. Could probably have found a better way to do this, but it works this way so changing it is a low priority.
    }   
}
