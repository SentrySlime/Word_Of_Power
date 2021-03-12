using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Level_Instancing : MonoBehaviour
{
    public List<GameObject> itemsToPickFrom;
    public GameObject teleporter;


    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;

    public float points = 0;
    public int randomNumber;
    public int randomDirection;

    public int randomIndex;

    int[] intArray = {0, 1, 2, 3, 5, 6, 7, 8};
    int[] randomDirections = { 0, 90, 180, 270, 360 };

    public Vector3 gridOrigin = Vector3.zero;
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public GameObject forestSuroudning;


    private void Awake()
    {
        pickRanomdNuyer();

        SpawnGrid();

        surfaces[0].BuildNavMesh();
    }


    private void Update()
    {  

        if(Input.GetKeyDown(KeyCode.T))
        {

            PickRandomDirection();
        }

    }

    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                PickRandomDirection();
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickandSpawn(spawnPosition, Quaternion.Euler(new Vector3(0, randomDirection, 0)));
                //PickandSpawn(spawnPosition, Quaternion.identity);

            }
        }
    }


    void PickandSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {


        if (points == randomNumber)
        {
            surfaces.Add(Instantiate(teleporter, positionToSpawn, rotationToSpawn, gameObject.transform).GetComponent<NavMeshSurface>());
        }
        else
        {
            randomIndex = Random.Range(0, itemsToPickFrom.Count);
            surfaces.Add(Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn, gameObject.transform).GetComponent<NavMeshSurface>());

        }

        points++;


        //itemsToPickFrom.RemoveAt(randomIndex);
    }


    public void PickRandomDirection()
    {
        int val = Random.Range(0, 5);

        randomDirection = randomDirections[val];

    }

    public void pickRanomdNuyer()
    {
        
        int val = Random.Range(0, 8);

        randomNumber = intArray[val];
    }
}