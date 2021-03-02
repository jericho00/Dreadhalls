using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour
{

    public string scene;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetAxis("Submit") == 1)
        {
            DontDestroy.removeObject();
            SceneManager.LoadScene(scene);
        }
    }
}
