using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    private Button startButton;

    public void SceneChange()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
}
