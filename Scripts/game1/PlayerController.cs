using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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


    // 手槍的轉角
    private float angle;
    private float gun_turn_speed = 30;
    private float min_angle = -20;
    private float max_angle = 0;


    // // 複製monster1
    // // public Rigidbody Monster1;
    // private Object[] monster_array;


    // // key的prefab
    // public Rigidbody KeyPrefab;

    // 現在收集到幾個key了
    // private int find_key_number = 0;

    // // 現在出來幾個key了(總共只會出來五個)
    // private int current_key = 0;


    // // UI面板

    // public GameObject choose;

    // public GameObject richman;

    // public Button Player1Button;
    // public Button Player2Button;
    // public GameObject index;
    // // 設定button
    // public Button StartButton;
    // public Text count_text;
    // // 遊戲結束的面板
    // public GameObject gameover_img;
    // public Text win_lose_text;

    // public Button StartAgainButton;
    // public Button NextGameButton;
    // public Button ExitButton;




    public Stage_set stage;


    public Healthbar healthbar;
    int maxHealth = 4;
    int currentHealth;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        currentHealth = maxHealth;
        healthbar.SetMaxHelth(maxHealth);

        // stage = gameObject.GetComponent<Stage_set>();


        // InvokeRepeating("MakeMonster", 5, 3);
        // InvokeRepeating("MakeKey", 20, 20);
        // count_text.text = "Current Key: 0/5";

        // index.SetActive(false);
        // gameover_img.SetActive(false);

        // Time.timeScale = 0f;

        // Button btn_player1 = Player1Button.GetComponent<Button>();
        // btn_player1.onClick.AddListener(Player1Click);

        // Button btn_player2 = Player2Button.GetComponent<Button>();
        // btn_player2.onClick.AddListener(Player2Click);

        // Button btn_start = StartButton.GetComponent<Button>();
        // btn_start.onClick.AddListener(StartClick);

        // Button btn_start_again = StartAgainButton.GetComponent<Button>();
        // btn_start_again.onClick.AddListener(StartAgainClick);

        // Button btn_next_game = NextGameButton.GetComponent<Button>();
        // btn_next_game.onClick.AddListener(Next_Game_Click);

        // Button btn_exit = ExitButton.GetComponent<Button>();
        // btn_exit.onClick.AddListener(ExitClick);
    }

    // void Player1Click()
    // {
    //     Destroy(richman);
    //     richman.SetActive(false);
    //     choose.SetActive(false);
    //     index.SetActive(true);
    // }

    // void Player2Click()
    // {
    //     Destroy(gameObject);
    //     richman.SetActive(true);
    //     choose.SetActive(false);
    //     index.SetActive(true);
    // }

    // void StartClick()
    // {
    //     index.SetActive(false);
    //     Time.timeScale = 1f;
    // }

    // void StartAgainClick()
    // {
    //     SceneManager.LoadScene(0);
    // }

    // void Next_Game_Click()
    // {
    //     SceneManager.LoadScene(1);
    // }

    // void ExitClick()
    // {
    //     Application.Quit();
    // }



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



        // // 更新monster的array
        // monster_array = GameObject.FindGameObjectsWithTag("monster");



    }


    // // 生產monster
    // void MakeMonster()
    // {
    //     Rigidbody monster_instance;
    //     monster_instance = Instantiate(monster_array[0], Vector3.zero, Quaternion.identity) as Rigidbody;

    // }


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


    // void MakeKey()
    // {
    //     current_key += 1;
    //     if (current_key <= 5)
    //     {
    //         Vector3 random_pos = new Vector3(Random.Range(-19f, 20f), 10f, Random.Range(-18f, 13f));
    //         Rigidbody key_instance = Instantiate(KeyPrefab, random_pos, Quaternion.identity) as Rigidbody;

    //     }
    // }




}
