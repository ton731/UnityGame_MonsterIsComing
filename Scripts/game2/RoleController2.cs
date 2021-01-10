using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController2 : MonoBehaviour
{
    // player
    private Rigidbody rb;

    // 手槍
    public GameObject gun;

    // 移動速度
    private float speed = 0.1f;

    // 旋轉速度
    private float rotate_speed = 1.0f;

    // 向上跳的力道、能不能跳
    private float jump_force = 35000f;

    private bool can_jump = true;


    public Rigidbody bulletprefab;

    private float bulletspeed = 1000;
   
    
    private Animator player_animator;

    public Stage_set2 stage;


    public Healthbar healthbar;
    int maxHealth = 4;
    int currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthbar.SetMaxHelth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player_animator.SetBool("is_shooting", true);
            Invoke("shoot", 0.5f);

        }
        else
        {
            player_animator.SetBool("is_shooting", false);
        }
    }


     void shoot()
    {
        Rigidbody button_instance;
        // Vector3 bullet_pos = transform.position + new Vector3(0.5f,1f,1);
        Vector3 bullet_rot = transform.rotation.eulerAngles + new Vector3(-15, 0, 0);
        button_instance = Instantiate(bulletprefab, gun.transform.position, Quaternion.Euler(bullet_rot)) as Rigidbody;
        button_instance.AddRelativeForce(new Vector3(0, 0, bulletspeed));
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        // 讓玩家左右移動及旋轉
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.Translate(0, 0, speed * v);
        transform.Rotate(0, rotate_speed * h, 0);

        if (v != 0)
        {
            player_animator.SetBool("is_running", true);
        }
        else
        {
            player_animator.SetBool("is_running", false);
        }





        // 玩家跳躍
        if (Input.GetKeyDown(KeyCode.Space) && can_jump)
        {
            can_jump = false;
            rb.AddForce(new Vector3(0, jump_force, 0));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            can_jump = true;
        }


        // 透過案R可以讓翻倒的玩家重新就定位
        if (Input.GetKeyDown(KeyCode.R))
        {
            float ang = transform.rotation.eulerAngles.y;
            Quaternion newAngle = Quaternion.identity;
            newAngle.eulerAngles = new Vector3(0, ang, 0);
            transform.rotation = newAngle;
        }


    }


  

    // 與monster撞到時就死亡，與key撞到就多一個key
    void OnCollisionEnter(Collision collide)
    {
         if (collide.gameObject.CompareTag("monster"))
        {
            currentHealth -= 1;
            healthbar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("死亡");
                stage.win_lose_text.text = "很可惜，你這次失敗了！";
                stage.gameover_img.SetActive(true);
                Time.timeScale = 0f;
            }

        }
        else if (collide.gameObject.CompareTag("key"))
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
