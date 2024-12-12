using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GameManager.Instance.player1.hasGun = true;
            GameManager.Instance.player2.hasGun = true;

            GameManager.Instance.uiManager.DisplayBulletCount();
            GameManager.Instance.OpenTrapChamberDoor();
            Destroy(gameObject);

           

        }
    }
}
