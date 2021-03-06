using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string destination = "Level 2";


    public void ChangeScene()
    {
       SceneManager.LoadScene(destination);
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            ChangeScene();
        }
    }

}
