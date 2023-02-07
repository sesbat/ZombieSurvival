using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public Slider expBar;

    [Header("Info")]
    public int level;
    public int kill;
    public int exp;
    private int[] nextExp = {3,5,10,100,150,210,280,360,450,600};

    [Header("GameControll")]
    public float maxGameTime = 20f;
    public float gameTime = 0f;

    [Header("UI")]
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        expBar.value = 0;
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
        killScoreText.text = $"{kill}";
        levelText.text = "Level : "+ $"{level}";
        
        if(!player.enabled)
        {
            Time.timeScale = 0f;
        }
    }
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
        expBar.value = (float)exp / (float)nextExp[level];
    }
}
