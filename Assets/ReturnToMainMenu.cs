using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public AnimationClip fading;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void StartFade()
    {
        anim.SetTrigger("Fade");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
