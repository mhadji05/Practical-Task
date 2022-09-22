using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnObjs_ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text winText;
    public Text timerText;

    public GameObject playAgainButton;
    
    public int score = 0;
    public int counter = 0;

    public GeneralManager genMan;
    public spawnCubeManager spawnMan;

    string previousTag;

    public int[] sphereLevels = new int[] { 1, 10, 20 };
    public int[] capsuleLevels = new int[] {2, 12, 22 };

    int playerLevel = 1;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        setPlayerLevel();
        scoreText.text = "Score: " + score;
        playAgainButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int seconds = (int)(timer % 60);
        int minutes = (int)(timer / 60) % 60;

        string timerString = string.Format("{00:00}:{01:00}", minutes, seconds);
        timerText.text = timerString;
    }

    void setPlayerLevel()
    {
        if (score >= 200)
        {
            playerLevel = 3;
            transform.localScale = new Vector3(1, playerLevel, 1);
        }
        else if (score >= 100)
        {
            playerLevel = 2;
            transform.localScale = new Vector3(1, playerLevel, 1);
        }
        else
        {
            playerLevel = 1;
            transform.localScale = new Vector3(1, playerLevel, 1);
        }
    }

    void checkWin()
    {
        scoreText.text = "Score: " + score;
        if (score >= 400)
        {
            score = 400;
            winText.text = "You Win!";
            playAgainButton.SetActive(true);
            scoreText.text = "Score: " + score;
            genMan.saveInJsonFile();
            Time.timeScale = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere")
        {
            if(previousTag == "Sphere")
            {
                score -= sphereLevels[playerLevel-1]*2;
            }
            else
            {
                score += sphereLevels[playerLevel-1];
            }

            previousTag = "Sphere";
            genMan.SpawnPointsFlag[int.Parse(collision.gameObject.name)] = false;
            spawnMan.SpawnObject();
            Destroy(collision.gameObject);
            counter++;
        }
        else if (collision.gameObject.tag == "Capsule")
        {
            if (previousTag == "Capsule")
            {
                score -= capsuleLevels[playerLevel-1] * 2;
            }
            else
            {
                score += capsuleLevels[playerLevel-1];
            }

            previousTag = "Capsule";
            genMan.SpawnPointsFlag[int.Parse(collision.gameObject.name)] = false;
            spawnMan.SpawnObject();
            Destroy(collision.gameObject);
            counter++;
        }

        setPlayerLevel();
        scoreText.text = "Score: " + score;
        checkWin();
           
    }
}
