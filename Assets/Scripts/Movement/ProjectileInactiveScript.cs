//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ProjectileInactiveScript : MonoBehaviour
//{

//    private Rigidbody rb;
//    public float damage;

//    public GeneralAbilities generalAbilites;

//    public HomingProjectile homingProjectile;


//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {

//    }

//    public void SetDamageScript()
//    {
//        damage = generalAbilites.damage;
//    }

//    void OnTriggerEnter(Collider col)
//    {
//        if (col.tag == "Enemy")
//        {
//            col.GetComponent<IEnemy>().TakeDamage(damage);
//            gameObject.SetActive(false);
//            rb.velocity = Vector3.zero;
//            rb.angularVelocity = Vector3.zero;
//        }
//    }
//}