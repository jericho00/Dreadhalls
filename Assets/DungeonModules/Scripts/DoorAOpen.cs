using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetTrigger("DoorATrigger");
    }
}