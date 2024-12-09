using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public void OnFire(InputValue inputValue)
    {
        transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
    }
}
