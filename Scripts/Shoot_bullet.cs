using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_bullet : MonoBehaviour
{


    public Rigidbody bulletprefab;

    public GameObject player;

    

    // 槍聲
    // public GameObject shoot_sound;

    public float speed = 1500;
    // Start is called before the first frame update
    void Start()
    {
        // shoot_sound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody button_instance;
            // Vector3 gun_rot = transform.rotation.eulerAngles;
            // Vector3 player_rot = player.transform.rotation.eulerAngles;
            // Vector3 conbine_rot = gun_rot + player_rot;
            button_instance = Instantiate(bulletprefab, transform.position, transform.rotation) as Rigidbody;
            button_instance.AddRelativeForce(new Vector3(0, 0, speed));

            // shoot_sound.SetActive(true);
        }

        // else{
        //     shoot_sound.SetActive(false);
        // }
    }
}
