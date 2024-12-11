using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "playerSO_", menuName = "Create PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public Sprite playerSprite;
    public AnimatorController animatorController;
}
