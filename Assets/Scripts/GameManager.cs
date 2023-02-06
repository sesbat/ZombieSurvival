using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("Info")]
    public int level;
    public int kill;
    public int exp;
    private int[] nextExp = {3,5,10,100,150,210,280,360,450,600};

    [Header("GameControll")]
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
    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
