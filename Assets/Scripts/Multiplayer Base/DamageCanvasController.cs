using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageCanvasController : MonoBehaviour
{
    public TMP_Text myText;
    Rigidbody2D myRB;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1f) * speed, ForceMode2D.Impulse);
    }



    public void SetText(int theText)
    {
        myText.text = theText.ToString();
    }
}
