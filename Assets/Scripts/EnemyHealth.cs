using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 30, xpValue = 6;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            health -= 10;

            if(health <= 0)
            {
                other.GetComponent<BulletController>().owner.ChangeXP(xpValue);
                Death();
            }
        }
    }

    void Death()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.angularDrag = 1;
        
        Destroy(this);
        Destroy(this.GetComponent<UnityEngine.AI.NavMeshAgent>());
        Destroy(this.GetComponent<MoveTo>());
        this.gameObject.tag = "Untagged";
    }
}
