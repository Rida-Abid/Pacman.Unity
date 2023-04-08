using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int Score;
    public Text Scoretext;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    [ContextMenu("Increase Score")]
    public void addScore()
    {
        Score = Score + 1;
        Scoretext.text = Score.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }



}
