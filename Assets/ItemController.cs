using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum itemType {Mushroom};
    public itemType thisItem = itemType.Mushroom;

    [Header("Audio")]
    
    public AudioSource aud;
    public AudioClip mushroomClip;
    [Range(0f,1f)]
    public float mushroomVolume = .5f;

    public Light Mushroom;

    // Rigidbody rb;

    void Start() 
    {
        // rb = this.GetComponent<Rigidbody>();
    }

    public void Fire()
    {
        if(thisItem == itemType.Mushroom) 
        {
            Mushroom.GetComponent<Light>().enabled = !Mushroom.GetComponent<Light>().enabled;
            aud.PlayOneShot(mushroomClip, mushroomVolume);
        }
    }

    public void Drop() 
    {
        this.transform.SetParent(null);
        // set rigidbody to isKinematic = false;
        // rb.isKinematic = false;
        // throw item forward.
        this.transform.Translate(Vector3.forward * 1);
        // rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
    }
}
