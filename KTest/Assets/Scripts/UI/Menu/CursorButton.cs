using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorButton : MonoBehaviour
{
    /// <summary>
    /// メニューの文字
    /// </summary>
    [SerializeField, Header("Caption")]
    Text captionText;

    /// <summary>
    /// 文字が表示されるとこの背景
    /// </summary>
    [SerializeField, Header("Caption")]
    Image captionBackground;
    /// <summary>
    /// 非選択時に文字が表示されるとこの背景色
    /// </summary>
    [SerializeField, Header("Caption")]
    Color disableCaptionBackgroundColor;
    /// <summary>
    /// ボタンが押されたときの文字が表示されるとこの背景
    /// </summary>
    [SerializeField, Header("Caption")]
    Color PressedCaptionBackgroundColor;

    [SerializeField]
    Image enabledBackgroundImage;

    [SerializeField]

    // Use this for initialization
    void Start()
    {

    }
}
