using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHouses : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] houses;

    public Transform player;
    public GameObject aiPrefab;


    // Start is called before the first frame update
    void Start()
    { foreach(Transform point in spawnPoints)
        {
            float chanceToSpawnHouse = Random.value;
            Debug.Log("Chance to spawn house: " + chanceToSpawnHouse);
            if(Random.value < .5f)
            {
                GameObject newHouse = Instantiate(houses[Random.Range(0,houses.Length)]);
                newHouse.transform.position = point.position;
                newHouse.transform.rotation = point.rotation;
                // spawn in 2-5 AI
                int numberOfAI = Random.Range(2, 5);
                for(int i = 0; i < numberOfAI; i++)
                {
                    GameObject ai = Instantiate(aiPrefab, point.position, point.rotation);
                    ai.GetComponent<MoveTo>().target = player;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
