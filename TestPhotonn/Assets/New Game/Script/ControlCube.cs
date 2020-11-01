using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCube : MonoBehaviour
{
    float dirX;
    float dirY;
    float speed = 10;
    CharacterManager chaMan;
    Objectspool objectspool;

    private void Start()
    {
        chaMan = GetComponent<CharacterManager>();
    }

    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        transform.Translate(0, 0, dirY * speed * Time.deltaTime);
        transform.Rotate(0, dirX * speed*20 * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            chaMan.ShootB();
    }
}
