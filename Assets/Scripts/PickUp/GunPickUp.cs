using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GivePlayersGun();
            GameManager.Instance.uiManager.DisplayBulletCount();
            GameManager.Instance.OpenTrapChamberDoor();
            Destroy(gameObject);
        }
    }
}
