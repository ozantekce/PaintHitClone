using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{

    public Sprite[] backgroundSprites;

    public Image backgroundImage;


    void Start()
    {
        RandomBackground();
    }

    public void RandomBackground()
    {

        int r = Random.Range(0, backgroundSprites.Length);
        backgroundImage.sprite = backgroundSprites[r];

    }



}
