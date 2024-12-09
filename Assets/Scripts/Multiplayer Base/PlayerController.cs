using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;  //must add this to have access to the new input system

public class PlayerController : MonoBehaviour
{
    //this script is used to change the colour of the player when the fire button is pressed.
    //it is only designed for PC, and not used with the arcade controls.
    //RMB or right trigger to change colours.

    PlayerInput myPI;

    // Start is called before the first frame update
    void Start()
    {
        myPI = GetComponent<PlayerInput>();
        transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
    }


    public void OnFire(InputValue inputValue)
    {
        //set a random colour
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

    }

    public void OnSwitchActionMap(InputValue inputValue)
    {
        print(myPI.defaultActionMap);
        myPI.defaultActionMap = "UI";
        print(myPI.defaultActionMap);
    }
}
