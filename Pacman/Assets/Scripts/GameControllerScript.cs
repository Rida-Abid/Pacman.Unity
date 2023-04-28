using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int Score;
    public Text Scoretext;
    public GameObject gameOverScreen;
    public GameObject pacman;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;

    public enum GhostMode
    {
        chase, scatter
    }
    
    public GhostMode currentGhostMode;

    // Start is called before the first frame update
    void Awake()
    {
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
    }

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
