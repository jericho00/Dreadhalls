using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDespawn : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        if (transform.position.y < 0)
        {

            LevelGenerator.ResetLevel();
            SceneManager.LoadScene("GameOver");
        }
    }
}
