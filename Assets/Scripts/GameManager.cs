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
    public int maxGameTime = 20;
    public float gameSec = 0f;
    public int gameMin = 0;

    [Header("UI")]
    public TextMeshProUGUI killScoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameTimeText;

    private void Awake()
    {
        expBar.value = 0;
        gameSec = 0f;
        instance = this;
    }
    private void Update()
    {
        gameSec += Time.deltaTime;
        if(gameMin > maxGameTime)
        {
            gameMin = maxGameTime;
        }
        killScoreText.text = $"{kill}";
        levelText.text = "Level : "+ $"{level}";
        gameTimeText.text = string.Format("{0:D2} : {1:D2}", gameMin, (int)gameSec);
        if((int)gameSec>59)
        {
            gameSec = 0f;
            gameMin++;
        }

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
