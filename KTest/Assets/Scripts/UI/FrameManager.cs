using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class FrameManager : MonoBehaviour
{
    public enum UIType
    {
        Menu,
        Buttle,
        Other
    }

    static public UIType currentUIType;

    /// <summary>
    /// 現在の Window
    /// </summary>
    static MyMenuFrame currentWindow;
    /// <summary>
    /// 開いた Window の履歴
    /// </summary>
    static Dictionary<UIType, Stack<MyMenuFrame>> history;

    void Awake()
    {
        history = new Dictionary<UIType, Stack<MyMenuFrame>>();
        history[UIType.Menu] = new Stack<MyMenuFrame>();
        history[UIType.Buttle] = new Stack<MyMenuFrame>();
        history[UIType.Other] = new Stack<MyMenuFrame>();
        currentUIType = UIType.Menu;
    }

    /// <summary>
    /// Window の切り替え
    /// </summary>
    /// <param name="window">切り替える Window</param>
    /// <param name="isAddHistory">履歴に追加するか</param>
    /// <param name="closeOldWindow">現在開いている Window を閉じるか</param>
    static public void changeWindow(MyMenuFrame window, bool closeOldWindow = false, bool isAddHistory = false)
    {
        if (window == currentWindow) {
            return;
        }

        settingCurrentWindow(closeOldWindow);
        settingFrame(window, true);
        if (isAddHistory && currentWindow != null) {
            history[currentUIType].Push(currentWindow);
        }
        currentWindow = window;
    }

    /// <summary>
    /// Window を開く
    /// history に追加
    /// </summary>
    /// <param name="window">切り替える Window</param>
    /// <param name="closeOldWindow">現在開いている Window を閉じるか</param>
    static public void openWindow(MyMenuFrame window, bool closeOldWindow = false)
    {
        if (window == currentWindow) {
            return;
        }
        
        settingCurrentWindow(closeOldWindow);
        settingFrame(window, true);
        if (currentWindow != null) {
            history[currentUIType].Push(currentWindow);
        }
        currentWindow = window;
    }

    /// <summary>
    /// 現在の UIType の履歴を削除します
    /// </summary>
    static public void clearHistory()
    {
        history[currentUIType].Clear();
    }

    /// <summary>
    /// 履歴をすべて削除します
    /// </summary>
    static public void allClearHistory()
    {
        foreach (var node in history.Values) {
            node.Clear();
        }
    }

    /// <summary>
    /// 現在の Window を閉じ、履歴の一番新しいものを表示します
    /// </summary>
    static public void cancel()
    {
        settingCurrentWindow(true);
        if (history[currentUIType].Count == 0) {
            return;
        }

        var window = history[currentUIType].Pop();
        Debug.Log(history[currentUIType].Count);
        settingFrame(window, true);
        currentWindow = window;
    }

    /// <summary>
    /// 履歴に積まれている Window を全部閉じ、履歴を削除します
    /// </summary>
    static public void allCloseWindow()
    {
        settingCurrentWindow(true);
        while (history[currentUIType].Count != 0) {
            var window = history[currentUIType].Pop();
            window.gameObject.SetActive(false);
        }

        currentWindow = null;
    }

    static public bool isAllClosed()
    {
        return history[currentUIType].Count == 0;
    }

    /// <summary>
    /// 現在のウィンドウ内のボタンを非アクティブにします
    /// </summary>
    /// <param name="closeWindow">window を閉じるか</param>
    static void settingCurrentWindow(bool closeWindow)
    {
        if (currentWindow == null) {
            return;
        }

        settingFrame(currentWindow, false);
        currentWindow.gameObject.SetActive(!closeWindow);
    }

    /// <summary>
    /// フレームの設定をします
    /// </summary>
    /// <param name="window">操作するWindow</param>
    /// <param name="activate">ボタンをアクティブ化するか</param>
    static void settingFrame(MyMenuFrame window, bool activate)
    {
        window.gameObject.SetActive(true);
        var buttons = window.GetComponentsInChildren<Button>();
        foreach (var button in buttons) {
            button.interactable = activate;
        }
        window.OpenWindow();
    }
}
