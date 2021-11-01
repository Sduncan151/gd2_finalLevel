using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingLoading : MonoBehaviour
{
    int health = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Health", Random.Range(1, 99));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health = " + health);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetHealth();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("my x position is " + transform.position.x);
            Debug.Log("my y position is " + transform.position.y);
            Debug.Log("my z position is " + transform.position.z);

            GetPosition();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetPosition();
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            GetPosition();
        }
    }

    void GetHealth()
    {
        health = PlayerPrefs.GetInt("Health", 100);
    }

    // saving the position of this gameObject as three floats.
    void SetPosition()
    {
        PlayerPrefs.SetFloat("posX", transform.position.x);
        PlayerPrefs.SetFloat("posY", transform.position.y);
        PlayerPrefs.SetFloat("posZ", transform.position.z);
    }

    void GetPosition()
    {
        Vector3 newPosition;

        newPosition.x = PlayerPrefs.GetFloat("posX", 100);
        newPosition.y = PlayerPrefs.GetFloat("posY", 100);
        newPosition.z = PlayerPrefs.GetFloat("posz", 100);

        this.transform.position = newPosition;
    }

    /*
        comment blocks! They're great.

        pseudocode = plain english laid out like code.
        Q: How do we get the xyz of a vector3 player position into floats?
            A: transform.position.x
        Q: What do I do with the floats then, to save them?
            A: 
        Q: How do I load those floats back into the player position?
    */
}
