using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private GameObject player;

    private Rigidbody rb;

    private bool onCar = false;

    public Stage_set3 stage;


    // public GameObject cubeplayer;
    // public GameObject richman;

    bool is_setup = false;



    public void Setup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        Debug.Log(player.name);
        is_setup = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (is_setup)
        {
            Vector3 distance = player.transform.position - transform.position;
            if (distance.magnitude > 2 && onCar)
            {
                Debug.Log("掉出去了");
                player.GetComponent<BoxCollider>().enabled = true;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                onCar = false;
                gameObject.transform.parent = null;
            }
        }
    }


    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            onCar = true;
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
            Vector3 player_pos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            player.transform.position = player_pos;

            player.GetComponent<BoxCollider>().enabled = false;
            rb.isKinematic = true;
            rb.detectCollisions = false;


            gameObject.transform.parent = player.transform;

        }

        else if (collide.gameObject.CompareTag("key") && onCar)
        {
            stage.find_key_number += 1;
            Destroy(collide.gameObject);
            Debug.Log("找到鑰匙了 " + stage.find_key_number);
            stage.count_text.text = "Current Key: " + stage.find_key_number.ToString() + "/5";
            if (stage.find_key_number == 5)
            {
                stage.win_lose_text.text = "恭喜你贏了！";
                stage.gameover_img.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }



}
