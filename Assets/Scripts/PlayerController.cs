// handles collision with enemies and firing magic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth hp, mana;

    [SerializeField]
    private Rigidbody magicBullet;

    [SerializeField]
    private Transform hand;

    [SerializeField]
    public UIController ui;

    private PlayerSaveAndLoad save;

    void Start()
    {
        if(hp == null)
        {
            hp = this.GetComponent<PlayerHealth>();
        }

        save = GetComponent<PlayerSaveAndLoad>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
            hp.ChangeMana(-10);

        }
    }

    void Fire()
    {
        Rigidbody copy = Instantiate(magicBullet, hand.position, hand.rotation);
        copy.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse); // shoot bullet forward
        copy.GetComponent<BulletController>().owner = hp;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I have hit " + other.name);
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("I've hit enemy");
            hp.ChangeHealth(-10);
        }

        if(other.gameObject.CompareTag("AddHealth"))
        {
            hp.ChangeHealth(+10);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("CheckPoint"))
        {
            save.Save();
        }
    }
}
