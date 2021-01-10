using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IndexController : MonoBehaviour
{
    public GameObject choose;
    public Button player1_button;
    public Button player2_button;
    public Button player3_button;



    public GameObject index;
    public GameObject game_info;
    public Button game1_button;
    public Button game2_button;
    public Button game3_button;
    public Button game4_button;
    public Button game5_button;

    public Button select_chac_button;
    public Button game_info_button;
    public Button end_game_info_button;
    public Button exit_button;

    public static int chooserole;

    public static bool firsttime = true;




    // Start is called before the first frame update
    void Start()
    {
        if(firsttime){
            choose.SetActive(true);
            index.SetActive(false);
        }
        else{
            choose.SetActive(false);
            index.SetActive(true);
        }

        game_info.SetActive(false);

        Button btn_player1 = player1_button.GetComponent<Button>();
        btn_player1.onClick.AddListener(player1Click);

        Button btn_player2 = player2_button.GetComponent<Button>();
        btn_player2.onClick.AddListener(player2Click);

        Button btn_player3 = player3_button.GetComponent<Button>();
        btn_player3.onClick.AddListener(player3Click);

        Button btn_game1 = game1_button.GetComponent<Button>();
        btn_game1.onClick.AddListener(game1Click);

        Button btn_game2 = game2_button.GetComponent<Button>();
        btn_game2.onClick.AddListener(game2Click);

        Button btn_game3 = game3_button.GetComponent<Button>();
        btn_game3.onClick.AddListener(game3Click);

        Button btn_game4 = game4_button.GetComponent<Button>();
        btn_game4.onClick.AddListener(game4Click);

        Button btn_game5 = game5_button.GetComponent<Button>();
        btn_game5.onClick.AddListener(game5Click);


        Button btn_select_chac = select_chac_button.GetComponent<Button>();
        btn_select_chac.onClick.AddListener(select_chac_Click);

        Button btn_game_info = game_info_button.GetComponent<Button>();
        btn_game_info.onClick.AddListener(game_info_Click);

        Button btn_end_game_info = end_game_info_button.GetComponent<Button>();
        btn_end_game_info.onClick.AddListener(end_game_info_Click);

        Button btn_exit = exit_button.GetComponent<Button>();
        btn_exit.onClick.AddListener(exitClick);
    }

    void player1Click()
    {
        chooserole = 1;
        choose.SetActive(false);
        index.SetActive(true);
        firsttime = false;
    }

    void player2Click()
    {
        chooserole = 2;
        choose.SetActive(false);
        index.SetActive(true);
        firsttime = false;
    }

    void player3Click()
    {
        chooserole = 3;
        choose.SetActive(false);
        index.SetActive(true);
        firsttime = false;
    }

    void game1Click()
    {
        SceneManager.LoadScene("game1");
    }

    void game2Click()
    {
        SceneManager.LoadScene("game2");
    }

    void game3Click()
    {
        SceneManager.LoadScene("game3");
    }

    void game4Click()
    {
        SceneManager.LoadScene("game4");
    }

    void game5Click()
    {
        SceneManager.LoadScene("game5");
    }

    void select_chac_Click()
    {
        index.SetActive(false);
        choose.SetActive(true);
    }

    void game_info_Click(){
        game_info.SetActive(true);
    }

    void end_game_info_Click(){
        game_info.SetActive(false);
    }

    void exitClick()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
