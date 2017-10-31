using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OpenFrameButton : MonoBehaviour
{
    /// <summary>
    /// ボタンクリック時に開くフレーム
    /// </summary>
    [SerializeField]
    MyMenuFrame openFrame;
    /// <summary>
    /// 新規メニューを開き、戻るを押したときに戻ることができるか
    /// </summary>
    [SerializeField]
    bool isStackHistory = true;
    bool IsStackHistory { set { isStackHistory = value; } get { return isStackHistory; } }
    /// <summary>
    /// 新規メニューを開いたときに、現在のメニューを閉じるか
    /// </summary>
    [SerializeField]
    bool isCloseWindow = false;
    bool IsCloseWindow { set { isCloseWindow = value; } get { return isCloseWindow; } }

    void Start()
    {
        var button = GetComponent<Button>();
        button.OnClickAsObservable()
            .Subscribe(_ => {
                FrameManager.changeWindow(openFrame, isCloseWindow, isStackHistory);
            })
            .AddTo(this);
    }
}
