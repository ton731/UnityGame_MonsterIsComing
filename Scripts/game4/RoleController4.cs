using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController4 : MonoBehaviour
{
    // player
    private Rigidbody rb;

    // 手槍
    public GameObject gun;

    public GameObject ultimate_place;

    // 移動速度
    private float speed = 0.1f;

    // 旋轉速度
    private float rotate_speed = 1.0f;

    // 向上跳的力道、能不能跳
    private float jump_force = 40000f;

    private bool can_jump = true;


    public Rigidbody bulletprefab;

    private float bulletspeed = 1000;


    private Animator player_animator;

    public Stage_set4 stage;

    public Healthbar healthbar;
    int maxHealth = 4;
    int currentHealth;


    public Powerbar powerbar;
    float maxPower = 100;
    float currentPower;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthbar.SetMaxHelth(maxHealth);

        currentPower = maxPower;
        powerbar.SetMaxPower(maxPower);


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


        if (Input.GetMouseButtonDown(1) && currentPower >= 40)
        {
            player_animator.SetBool("is_ultimating", true);
            Invoke("ultimate", 0.5f);
        }
        else
        {
            player_animator.SetBool("is_ultimating", false);
        }


        if (currentPower < maxPower)
        {
            currentPower += Time.deltaTime;
            powerbar.SetPower(currentPower);
        }
    }


    void shoot()
    {
        Rigidbody bullet_instance;
        // Vector3 bullet_pos = transform.position + new Vector3(0.5f,1f,1);
        Vector3 bullet_rot = transform.rotation.eulerAngles + new Vector3(-15, 0, 0);
        bullet_instance = Instantiate(bulletprefab, gun.transform.position, Quaternion.Euler(bullet_rot)) as Rigidbody;
        bullet_instance.AddRelativeForce(new Vector3(0, 0, bulletspeed));
    }

    void ultimate()
    {
       currentPower -= 40;
        powerbar.SetPower(currentPower);

        Vector3 origin = new Vector3(ultimate_place.transform.position.x, ultimate_place.transform.position.y, ultimate_place.transform.position.z);

        // richman的大招
        if (IndexController.chooserole == 2)
        {
            Vector3 origin_local = transform.InverseTransformPoint(origin);
            Vector3 loc_x = transform.InverseTransformDirection(new Vector3(1, 0, 0));
            Vector3 local_inst_pos;

            Rigidbody ultimate_bullet;
            Vector3 inst_pos;
            Vector3 ultimate_bullet_rot;

            for (int x = -7; x <= 7; x++)
            {
                for (int y = -7; y <= 7; y++)
                {
                    local_inst_pos = origin_local + new Vector3(loc_x.x * 4f * x, 0, 0) + new Vector3(0, 4f * y, 0);
                    inst_pos = transform.TransformPoint(local_inst_pos);
                    ultimate_bullet_rot = transform.rotation.eulerAngles + new Vector3(-10, 0, 0);
                    ultimate_bullet = Instantiate(bulletprefab, inst_pos, Quaternion.Euler(ultimate_bullet_rot));
                    ultimate_bullet.AddRelativeForce(new Vector3(0, 0, bulletspeed));
                }
            }
        }


        // ironman的大招
        else if (IndexController.chooserole == 3)
        {
            Rigidbody ultimate_bullet;
            for (int x = -6; x <= 6; x++)
            {
                for (int z = -6; z <= 6; z++)
                {
                    Vector3 inst_pos = new Vector3(origin.x + 0.7f * x, origin.y, origin.z + 0.7f * z);
                    ultimate_bullet = Instantiate(bulletprefab, inst_pos, transform.rotation);
                    float rand_y = Random.Range(1.5f,2.5f);
                    float rand_z = Random.Range(7f,14f);
                    ultimate_bullet.AddRelativeForce(new Vector3(0, bulletspeed / rand_y, bulletspeed / rand_z));
                }
            }
        }
    }



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
