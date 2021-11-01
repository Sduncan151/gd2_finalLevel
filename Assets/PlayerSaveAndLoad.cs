using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveAndLoad : MonoBehaviour
{
    PlayerHealth hp;

    void Start()
    {
        hp = GetComponent<PlayerHealth>();
        Debug.Log("Health = " + hp.health);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Health", hp.health);

        PlayerPrefs.SetFloat("posX", transform.position.x);
        PlayerPrefs.SetFloat("posY", transform.position.y);
        PlayerPrefs.SetFloat("posZ", transform.position.z);
    }

    public void Load()
    {
        hp.health = PlayerPrefs.GetInt("Health", 100);

        Vector3 newPosition;

        newPosition.x = PlayerPrefs.GetFloat("posX", 100);
        newPosition.y = PlayerPrefs.GetFloat("posY", 100);
        newPosition.z = PlayerPrefs.GetFloat("posz", 100);

        this.transform.position = newPosition;
    }
}
