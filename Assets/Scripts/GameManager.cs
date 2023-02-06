using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    public float maxGameTime = 20f;
    public float gameTime = 0f;

    private void Awake()
    {
        gameTime = 0f;
        instance = this;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
