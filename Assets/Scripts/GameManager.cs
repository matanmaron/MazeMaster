using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text TxtHealth;
    public Text TxtKeys;
    public Text TxtScore;
    public PlayerControll Player;

    // Start is called before the first frame update
    void Start()
    {
        TxtHealth.text = Player.Health.ToString();
        TxtScore.text = Player.Score.ToString();
    }



    // Update is called once per frame
    void Update()
    {
    }
}