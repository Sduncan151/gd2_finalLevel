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

    [SerializeField]
    public ItemController item;

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

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(potionAmount > 0)
            {
                potionAmount -= 1;
                potionAmountText.text = "Health Potions " + potionAmount.ToString();
                hp.ChangeHealth(10);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(potionManaAmount > 0)
            {
                potionManaAmount -= 1;
                potionManaAmountText.text = "Mana Potions " + potionManaAmount.ToString();
                hp.ChangeMana(5);
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            if(item != null)
            {
                item.Drop();
                item = null;
            }
        }
    }

    void Fire()
    {
        if(hp.GetMana() > 5 && item == null)
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
            potionAmountText.text = "Health Potions " + potionAmount.ToString();
            Destroy(other.gameObject);
        }
         if(other.gameObject.CompareTag("AddMana"))
        {
            potionManaAmount += 1;
            potionManaAmountText.text = "Mana Potions " + potionManaAmount.ToString();
            Destroy(other.gameObject);
        }

        Debug.Log("I've hit " + other.gameObject.name);
        if(other.gameObject.CompareTag("Pickupable")) 
        {
            Debug.Log("I can pick this up");
            if(item == null) 
            {
                Debug.Log("Let's try to pick this up.");
                // we can pick up the object!
                item = other.gameObject.GetComponent<ItemController>();
                // move the object to our hand.
                other.gameObject.transform.position = hand.position;
                // make the object a child of the hand so it follows.
                other.gameObject.transform.SetParent(hand);
                // make the object face the same direction as the hand.
                other.gameObject.transform.rotation = hand.rotation;
                // keep the gun from falling
                // other.GetComponent<Rigidbody>().isKinematic = true;
            }
            else 
            {
                Debug.Log("Already holding something.");
            }
        }
    }
}
