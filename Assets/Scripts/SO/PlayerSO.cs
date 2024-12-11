using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerSO_", menuName = "Create PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public Sprite playerSprite;
    public Animator animator;
}
