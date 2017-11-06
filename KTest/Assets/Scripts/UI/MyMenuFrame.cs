using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class MyMenuFrame : MonoBehaviour
{
    /// <summary>
    /// 開いたときに前回選択していたオブジェクトを選択するか
    /// </summary>
    public bool isSaveObject;
    // フレームを開いたときonOpenWindow 関数を呼び出すか
    public bool isCallOnOpenWindow;

    int oldSelect = 0;
    public int oldSelectNumber { set { oldSelect = value; } get { return oldSelect; } }

    public void OpenWindow()
    {
        if (isCallOnOpenWindow) {
            onOpenWindow();
            return;
        }

        Observable.NextFrame()
            .Subscribe(_ => 
            {
                EventSystem.current.SetSelectedGameObject(GetComponentsInChildren<Button>()[isSaveObject ? oldSelectNumber : 0].gameObject);
            });
    }

    /// <summary>
    /// 開いたときに呼ばれる関数
    /// </summary>
    protected virtual void onOpenWindow()
    {
    }
}
