using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Test : MonoBehaviour
{
    [SerializeField]
    MyMenuFrame menu;
    [SerializeField]
    Button NewGame;
    [SerializeField]
    Button SaveGame;
    [SerializeField]
    Button LoadGame;
    [SerializeField]
    Button AddValue;

    // Use this for initialization
    void Start()
    {
        NewGame.OnClickAsObservable()
       .Subscribe(_ => {
           GV.newGame();
           PlayerManager.initialize();
           Debug.Log(PlayerManager.players[0].BaseParam.Atk);
       });
        SaveGame.OnClickAsObservable()
            .Subscribe(_ => {
                GV.save();//newGame();
                Debug.Log(PlayerManager.players[0].BaseParam.Atk);
            });
        LoadGame.OnClickAsObservable()
            .Subscribe(_ => {
                GV.load();
                PlayerManager.initialize();
                Debug.Log(PlayerManager.players[0].BaseParam.Atk);
            });
        AddValue.OnClickAsObservable()
            .Subscribe(_ => {
                PlayerManager.players[0].BaseParam.Atk += 1;
            });

        Observable.NextFrame()
            .Subscribe(_ => {
            });
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerManager.players[0].BaseParam.Atk = 2;
        
        //Debug.Log(PlayerManager.players[0].BaseParam.Atk);

        if (Input.GetKeyDown(KeyCode.A) && FrameManager.isAllClosed()) {
            FrameManager.openWindow(menu);
        }
        if (Input.GetButtonDown("Cancel")) {
            FrameManager.cancel();
        }
    }
}
