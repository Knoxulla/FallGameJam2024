using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeCharacter : MonoBehaviour
{
    public SpriteRenderer[] clothingBits;
    public SpriteRenderer[] hairBits;
    public SpriteRenderer featherBits;
    public SpriteRenderer pantsBits;

    public Image UIhat;
    public Image UIfeather;

    GameObject gameCanvas;
    public GameObject playerPanel;

    Color clothingColour;
    Color featherColour;
    Color pantsColour;
    Color redHair = new Color(0.91f, 0.45f, 0.19f, 1f);
    Color blondHair = new Color(1f, 0.92f, 0.016f, 1f);
    Color blackHair = new Color(0.15f, 0.15f, 0.15f, 1f);
    Color brownHair = new Color(0.48f, 0.31f, 0.14f, 1f);
    Color grayHair = new Color(0.69f, 0.67f, 0.65f, 1f);
    Color whiteHair = new Color(0.94f, 0.93f, 0.93f, 1f);
    Color hairColour;

    // Start is called before the first frame update
    void Start()
    {
        gameCanvas = GameObject.FindGameObjectWithTag("GameController");

        Color[] hairColours = { redHair, blondHair, blackHair, brownHair, whiteHair, grayHair};

        clothingColour = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);

        UIhat.color = clothingColour;

        foreach(SpriteRenderer SR in clothingBits)
        {
            SR.color = clothingColour;
        }

        hairColour = hairColours[Random.Range(0, hairColours.Length-1)];
        foreach (SpriteRenderer SR in hairBits)
        {
            SR.color = hairColour;
        }

        featherColour = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
        featherBits.color = featherColour;
        UIfeather.color = featherColour;

        pantsColour = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
        pantsBits.color = pantsColour;

        playerPanel.transform.SetParent(gameCanvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
