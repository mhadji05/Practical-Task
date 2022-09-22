using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{

    public GameObject spawnPointPrefab;

    public Transform PlayZone;

    public Text winText;
    public GameObject playAgainButton;

    public GameObject[] spawnPoints;
    public bool[] SpawnPointsFlag;
    public bool[] PlayerPointsFlag;
    public bool[] allPointsFlag;
    public List<int> availableSpawnPoints;

    public PlayerMovement playerMov;
    public spawnObjs_ScoreManager sp_scoMan;

    void Awake()
    {
        float maxPosX = PlayZone.localScale.x / 2 -0.5f;
        float length = PlayZone.localScale.x / 2f;
        print("maxPosX: " + maxPosX);
        print("length: " + length);

        spawnPoints = new GameObject[(int)(length * length * 4)];

        SpawnPointsFlag = new bool[spawnPoints.Length];
        PlayerPointsFlag = new bool[spawnPoints.Length];
        allPointsFlag = new bool[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnPointsFlag[i] = false;  //initialize all positions.
            PlayerPointsFlag[i] = false;  //initialize all positions.
            allPointsFlag[i] = false;  //initialize all positions.
        }

        GameObject spawnPointsParent = new GameObject("spawnPointsParent");

        int x = 0;
        for (int i=0; i< length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject obj1 = Instantiate(spawnPointPrefab, new Vector3(i+0.5f, 0.5f, j + 0.5f), Quaternion.identity);
                GameObject obj2 = Instantiate(spawnPointPrefab, new Vector3(-i-1+0.5f, 0.5f, -j-1 + 0.5f), Quaternion.identity);
                GameObject obj3 = Instantiate(spawnPointPrefab, new Vector3(-i-1 + 0.5f, 0.5f, j + 0.5f), Quaternion.identity);
                GameObject obj4 = Instantiate(spawnPointPrefab, new Vector3(i + 0.5f, 0.5f, -j-1 + 0.5f), Quaternion.identity);
                obj1.transform.SetParent(spawnPointsParent.transform);
                obj2.transform.SetParent(spawnPointsParent.transform);
                obj3.transform.SetParent(spawnPointsParent.transform);
                obj4.transform.SetParent(spawnPointsParent.transform);

                spawnPoints[x*4] = obj1;
                spawnPoints[x*4+1] = obj2;
                spawnPoints[x*4+2] = obj3;
                spawnPoints[x*4+3] = obj4;
                x++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(0); // Load the scene index 0
        Time.timeScale = 1.0f;
    }

    public int available;

    public int getTheAvailablePositions()
    {
        availableSpawnPoints.Clear();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            allPointsFlag[i] = SpawnPointsFlag[i];

            if (PlayerPointsFlag[i] == true)
            {
                allPointsFlag[i] = true;
            }

            if (allPointsFlag[i] == false)
            { 
                availableSpawnPoints.Add(i);
            }
        }

        /*for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (SpawnPointsFlag[i] == false)
            {
                availableSpawnPoints.Add(i);
            }
        }*/
        availableSpawnPoints.Remove(playerIndex);
        int rand = -1;
        available = -1;

        if (availableSpawnPoints.Count > 0)
        {
            rand = Random.Range(0, availableSpawnPoints.Count);
            allPointsFlag[availableSpawnPoints[rand]] = true;
            SpawnPointsFlag[availableSpawnPoints[rand]] = true;
            available = availableSpawnPoints[rand];
        }
        else
        {
            Lose();
        }

        //print("available: " + available);
        return available;
    }

    void Lose()
    {
        winText.text = "You Lose!";
        playAgainButton.SetActive(true);
        saveInJsonFile();
        Time.timeScale = 0.0f;
    }

    public int playerIndex=-1;
    public void flagPlayerPosition(float posX, float posZ)
    {
        Vector3 playerPos = new Vector3(posX, 0.5f, posZ);
        Vector3 lastPos = playerPos;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (playerPos == spawnPoints[i].transform.position)
            {
                PlayerPointsFlag[i] = true;
                playerIndex = i;
            }
            else
            {
                PlayerPointsFlag[i] = false;
            }
        }
    }

    bool isSaved = false;
    public void saveInJsonFile()
    {
        if (!isSaved)
        {
            string path = Application.dataPath + "/../" + "/RESULTS/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            print(path);

            string timeStamp = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy_HH-mm-ss");
            string fileName = "Results_" + timeStamp;

            string output = "";

            RESULTS results = new RESULTS(sp_scoMan.timerText.text, sp_scoMan.score, sp_scoMan.counter);
            output = JsonUtility.ToJson(results);

            StreamWriter writer = new StreamWriter(path + "_" + fileName + ".json");
            writer.Flush();
            writer.WriteLine(output);
            writer.Close();

            isSaved = true;
            Debug.Log("saveInLogFile()");
        }
    }

}
