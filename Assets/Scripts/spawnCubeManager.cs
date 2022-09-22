using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCubeManager : MonoBehaviour
{
    const float SPAWN_FREQUENCY = 1.0f;

    public GameObject [] Objects;

    public GeneralManager genMan;

    private GameObject spawnedOjects;

    // Start is called before the first frame update
    void Start()
    {
        spawnedOjects = new GameObject("spawnedOjects");
        InvokeRepeating("SpawnCube", SPAWN_FREQUENCY, SPAWN_FREQUENCY);
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        int objectIndex = Random.Range(1, 3);
        //Debug.Log("objectIndex: " + objectIndex);

        int available = genMan.getTheAvailablePositions();
        if (available >= 0)
        {
            GameObject obj = Instantiate(Objects[objectIndex], genMan.spawnPoints[available].transform.position, Quaternion.identity);
            obj.name = available.ToString();
            obj.transform.SetParent(null);
            obj.transform.SetParent(spawnedOjects.transform);
        }
    }
    private void SpawnCube()
    {
        int available = genMan.getTheAvailablePositions();
        if (available >= 0)
        {
            GameObject obj = Instantiate(Objects[0], genMan.spawnPoints[available].transform.position, Quaternion.identity);
            obj.name = available.ToString();
            obj.transform.SetParent(null);
            obj.transform.SetParent(spawnedOjects.transform);
        }

    }

}
