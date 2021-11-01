// handles collision with enemies and firing magic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Text potionAmountText;
    private int potionAmount = 0;

    [SerializeField]
    private Text potionManaAmountText;
    private int potionManaAmount = 0;

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
        }
        // if(Input.GetKeyDown(KeyCode.Q))
        // {
        //     Potion();
        // }
    }

    void Fire()
    {
        if(hp.GetMana() > 5)
        {
        hp.ChangeMana(-5);
        Rigidbody copy = Instantiate(magicBullet, hand.position, hand.rotation);
        copy.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse); // shoot bullet forward
        copy.GetComponent<BulletController>().owner = hp;
        Destroy(copy.gameObject, 2);
        }
    }

    // void Potion()
    // {
    //     if(hp.GetPotion() > 1)
    //     {
    //         hp.ChangePotionAmount(-1);
    //         hp.ChangeHealth(+10);
    //     }
    // }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("I have hit " + other.name);
        if(other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("I've hit enemy");
            hp.ChangeHealth(-10);
        }

        else if(other.gameObject.CompareTag("CheckPoint"))
        {
            save.Save();
        }
        if(other.gameObject.CompareTag("DoubleAddHealth"))
        {
            hp.ChangeHealth(+20);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("AddHealth"))
        {
            potionAmount += 1;
            potionAmountText.text = potionAmount.ToString();
            Destroy(other.gameObject);
        }
         if(other.gameObject.CompareTag("AddMana"))
        {
            potionManaAmount += 1;
            potionManaAmountText.text = potionManaAmount.ToString();
            Destroy(other.gameObject);
        }
    }
}
