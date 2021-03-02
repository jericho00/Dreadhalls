using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject ceilingPrefab;
    public GameObject characterController;
    public GameObject floorParent;
    public GameObject wallsParent;
    public Text text;
    public bool generateRoof = true;
    public int tilesToRemove = 50;
    public int holeCount = 20;
    public int mazeSize;
    public GameObject pickup;
    private bool characterPlaced = false;

    private static int level = 1;

    private bool[,] mapData;

    private int mazeX = 4, mazeY = 1;

    void Start()
    {

        int holesGenerated = 0;
        mapData = GenerateMazeData();

        for (int z = 0; z < mazeSize; z++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if (mapData[z, x])
                {
                    CreateChildPrefab(wallPrefab, wallsParent, x, 1, z);
                    CreateChildPrefab(wallPrefab, wallsParent, x, 2, z);
                    CreateChildPrefab(wallPrefab, wallsParent, x, 3, z);
                }
                else if (!characterPlaced)
                {
                    characterController.transform.SetPositionAndRotation(
                        new Vector3(x, 1, z), Quaternion.identity
                    );
                    characterPlaced = true;

                    CreateChildPrefab(floorPrefab, floorParent, x, 0, z);

                }

                if (Random.value < 0.1 && holesGenerated < holeCount && characterPlaced)
                {
                    holesGenerated++;
                }
                else
                {
                    CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
                }


                if (generateRoof)
                {
                    CreateChildPrefab(ceilingPrefab, wallsParent, x, 4, z);
                }
            }
        }

        var myPickup = Instantiate(pickup, new Vector3(mazeX, 1, mazeY), Quaternion.identity);
        myPickup.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        text.text = "Level " + level;
    }

    bool[,] GenerateMazeData()
    {
        bool[,] data = new bool[mazeSize, mazeSize];

        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                data[y, x] = true;
            }
        }

        int tilesConsumed = 0;
        while (tilesConsumed < tilesToRemove)
        {
            int xDirection = 0, yDirection = 0;

            if (Random.value < 0.5)
            {
                xDirection = Random.value < 0.5 ? 1 : -1;
            }
            else
            {
                yDirection = Random.value < 0.5 ? 1 : -1;
            }

            int numSpacesMove = (int)(Random.Range(1, mazeSize - 1));

            for (int i = 0; i < numSpacesMove; i++)
            {
                mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
                mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);

                if (data[mazeY, mazeX])
                {
                    data[mazeY, mazeX] = false;
                    tilesConsumed++;
                }
            }
        }

        return data;
    }

    void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        myPrefab.transform.parent = parent.transform;
    }

    public static void IncreaseLevel()
    {
        level += 1;
    }

    public static void ResetLevel()
    {
        level = 1;

    }
}
