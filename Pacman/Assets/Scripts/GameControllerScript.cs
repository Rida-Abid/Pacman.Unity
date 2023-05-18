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
    public bool gameIsRunning;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;

    public GameObject blinkyGhost;
    public GameObject inkyGhost;
    public GameObject pinkyGhost;
    public GameObject clydeGhost;

    public AudioSource siren;
    public AudioSource death;

    public int[] ghostModeTimers = new int[] {7, 20, 7, 20, 5, 20, 5};
    public int ghostModeTimerIndex;
    public float ghostModeTimer = 0;
    public bool runningTimer;
    public bool completedTimer;


    public enum GhostMode
    {
        chase, scatter
    }
    
    public GhostMode currentGhostMode;

    // Start is called before the first frame update
    void Awake()
    {
        pinkyGhost.GetComponent<GhostScript>().readyToLeaveHome = true;
        inkyGhost.GetComponent<GhostScript>().readyToLeaveHome = true;
        clydeGhost.GetComponent<GhostScript>().readyToLeaveHome = true;
        siren.Play();
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStartingNode = true;
    }
    void Update()
    {
       if(!gameIsRunning)
       {
            return;
       }

       if(!completedTimer && runningTimer)
        {
            ghostModeTimer += Time.deltaTime;
            if(ghostModeTimer >= ghostModeTimers[ghostModeTimerIndex])
            {
                ghostModeTimer = 0;
                ghostModeTimerIndex++;
                if(currentGhostMode == GhostMode.chase)
                {
                    currentGhostMode = GhostMode.scatter;
                }
                else
                {
                    currentGhostMode = GhostMode.chase;
                }

                if(ghostModeTimerIndex  == ghostModeTimers.Length)
                {
                    completedTimer = true;
                    runningTimer = false;
                    currentGhostMode = GhostMode.chase;

                }
            }
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 0.5f;
    }
    
    public IEnumerable Setup()
    {
        ghostModeTimerIndex = 0;
        ghostModeTimer = 0;
        completedTimer = false;
        runningTimer = true;
        yield return new WaitForSeconds(1);

    }






}
