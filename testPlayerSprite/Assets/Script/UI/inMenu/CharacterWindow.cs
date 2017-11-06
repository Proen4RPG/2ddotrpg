using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterWindow : MonoBehaviour {
    [SerializeField]
    private UI_Menu menu;

    [SerializeField]
    private PlayerManagerCSV playerManager; // PlayerManagerCSV に変更を行いました

	private GameObject[] objList = new GameObject[4];  //キャラクター名を含むオブジェクト  //TODO キャラクターができ次第要素を増やす
	// PlayerManagerCSV に変更を行いました
    private PlayerManagerCSV.PlayerParameters[] playerList = new PlayerManagerCSV.PlayerParameters[4];    //  キャラクター情報
	// PlayerManagerCSV に変更を行いました
    public PlayerManagerCSV.PlayerParameters[] PlayerList { get { return playerList; } }

    [SerializeField]
    private GameObject StatusWindow;
    [SerializeField]
    private StatusWindow status;

    int choiceElement;
    bool isOpen;                                    //他Windowが開いているか
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }

        set
        {
            isOpen = value;
        }
    }

    void Awake()
    {
        //StatusWindow = GameObject.Find("StatusWindow");
        StatusWindow.SetActive(false);
    }

    // Use this for initialization
    void OnEnable () {
        for(int i = 0; i < objList.Length; i++) {
            objList[i] = GameObject.Find("CharacterName" + (i + 1));
        }

        playerList[0] = playerManager.Player1;
        playerList[1] = playerManager.Player2;
        playerList[2] = playerManager.Player3;
        playerList[3] = playerManager.Player4;

        choiceElement = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (isOpen) return;

        UIController.ChangeChoice(objList, choiceElement);

        if (MyInput.isButtonDown()) {
            choiceElement -= (int)MyInput.direction().y;

            if (choiceElement < 0) {
                choiceElement = 0;
            }
            if (choiceElement >= objList.Length) {
                choiceElement = objList.Length - 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)) {
            StatusWindow.SetActive(true);
            status.Param = playerList[choiceElement];

            isOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            menu.IsOpen = false;

            this.gameObject.SetActive(false);
        }
    }
}
