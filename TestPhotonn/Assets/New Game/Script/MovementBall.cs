using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class MovementBall : MonoBehaviour, IPoolObject
{ 
    [Range(0,100)]
    [SerializeField] internal float speedBulletForward;
    [Range(0,10)]
    [SerializeField] internal float timeRespawn;

    Rigidbody bullterRig;
    CharacterManager charmango;

    // Start is called before the first frame update
    public void OnObjectSpawn() //Ejecuta codigo de la herencia
    {
        bullterRig = GetComponent<Rigidbody>();
        bullterRig.AddForce(transform.forward * speedBulletForward, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            charmango = collision.gameObject.GetComponent<CharacterManager>();

            if (charmango.life > 20)
            {
                charmango.life = charmango.life - 20;
                Debug.Log("Vida actual"+ charmango.life);
            }
            else {
                collision.gameObject.SetActive(false);
            }
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnEnable()
    {
        Invoke("OnDisable", timeRespawn);
    }
    private void OnDisable()
    {
        this.transform.gameObject.SetActive(false);
    }
}
