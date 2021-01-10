using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_set5 : MonoBehaviour
{

    public GameObject cubeplayer;
    public GameObject richman;
    public GameObject ironman;

    // 複製monster1
    // public Rigidbody Monster1;
    private Object[] monster_array;

    private int current_duplicate_monster = 0;


    // key的prefab
    public Rigidbody KeyPrefab;

    // 現在收集到幾個key了
    public int find_key_number = 0;

    // 現在出來幾個key了(總共只會出來五個)
    public int current_key = 0;


    // UI面板
    public GameObject index;
    // 設定button
    public Button StartButton;
    public Text count_text;
    // 遊戲結束的面板
    public GameObject gameover_img;
    public Text win_lose_text;

    public Button StartAgainButton;
    public Button NextGameButton;
    public Button ExitButton;

    // public Stage_set stage;






    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeMonster", 5, 3f);
        InvokeRepeating("MakeKey", 30, 45);
        count_text.text = "Current Key: 0/5";
        gameover_img.SetActive(false);


        Time.timeScale = 0f;

        Button btn_start = StartButton.GetComponent<Button>();
        btn_start.onClick.AddListener(StartClick);

        Button btn_start_again = StartAgainButton.GetComponent<Button>();
        btn_start_again.onClick.AddListener(StartAgainClick);

        Button btn_next_game = NextGameButton.GetComponent<Button>();
        btn_next_game.onClick.AddListener(NextGameClick);

        Button btn_exit = ExitButton.GetComponent<Button>();
        btn_exit.onClick.AddListener(ExitClick);


        if (IndexController.chooserole == 1)
        {
            Destroy(richman);
            Destroy(ironman);
            cubeplayer.SetActive(true);
        }
        else if (IndexController.chooserole == 2)
        {
            Destroy(cubeplayer);
            Destroy(ironman);
            richman.SetActive(true);
        }
        else if(IndexController.chooserole == 3){
            Destroy(cubeplayer);
            Destroy(richman);
            ironman.SetActive(true);
        }


    }




    void StartClick()
    {
        index.SetActive(false);
        Time.timeScale = 1f;
    }

    void StartAgainClick()
    {
        SceneManager.LoadScene("game5");
    }

    void NextGameClick()
    {
        // SceneManager.LoadScene("game6");
    }

    void ExitClick()
    {
        SceneManager.LoadScene("index");
    }

    // Update is called once per frame
    void Update()
    {
        // 更新monster的array
        monster_array = GameObject.FindGameObjectsWithTag("monster");
    }


    // 生產monster
    void MakeMonster()
    {
        current_duplicate_monster += 1;
        GameObject monster_instance;
        monster_instance = Instantiate(monster_array[0], Vector3.zero, Quaternion.identity) as GameObject;
    }


    void MakeKey()
    {
        current_key += 1;
        if (current_key <= 10)
        {
            Vector3 random_pos = new Vector3(Random.Range(-15f, 100f), 10f, Random.Range(-50f, 50f));
            Rigidbody key_instance = Instantiate(KeyPrefab, random_pos, Quaternion.identity) as Rigidbody;

        }
    }
}
