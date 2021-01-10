using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController5 : MonoBehaviour
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
    private float jump_force = 40000f;

    private bool can_jump = true;


    // 手槍的轉角
    private float angle;
    private float gun_turn_speed = 30;
    private float min_angle = -20;
    private float max_angle = 0;


    public Stage_set5 stage;

    public static bool onCar = false;


    public Healthbar healthbar;
    int maxHealth = 4;
    int currentHealth;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        healthbar.SetMaxHelth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

    }



    void FixedUpdate()
    {

        // 讓玩家左右移動及旋轉
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.Translate(0, 0, speed * v);
        transform.Rotate(0, rotate_speed * h, 0);



        // 讓玩家透過滑鼠的移動來控制槍左右的方向
        var x = Input.GetAxis("Mouse X");
        gun.transform.Rotate(0, 3 * x, 0);

        //玩家用滑鼠滾輪控制槍的上下
        angle += Input.GetAxis("Mouse ScrollWheel") * gun_turn_speed;
        angle = Mathf.Clamp(angle, min_angle, max_angle);

        Vector3 gun_angle = gun.transform.localEulerAngles;
        gun_angle.x = angle;
        gun.transform.localEulerAngles = gun_angle;



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
