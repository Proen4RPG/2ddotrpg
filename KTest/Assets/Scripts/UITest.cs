using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class UITest : MonoBehaviour
{
    [SerializeField]
    GameObject MenuPrefab;
    [SerializeField]
    GameObject ButtonPrefab;
    List<MyMenuFrame> frames = new List<MyMenuFrame>();

    MyMenuFrame menuFrame;
    Button[] buttons;

    // Use this for initialization
    void Start()
    {
        menuFrame = GetComponent<MyMenuFrame>();
        buttons = GetComponentsInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
        for (int i = 0; i < buttons.Length; ++i) {
            var frame = Instantiate(MenuPrefab, transform.parent);
            for (int j = 0; j < i + 1; ++j) {
                Instantiate(ButtonPrefab, frame.transform, false);

            }
            frames.Add(frame.GetComponent<MyMenuFrame>());
            frame.SetActive(false);
        }

        for (int i = 0; i < buttons.Length; ++i) {
            buttonSetting(i);
        }
        gameObject.SetActive(false);
    }

    void buttonSetting(int i)
    {
        buttons[i].OnClickAsObservable()
            .Subscribe(_ => {
                menuFrame.oldSelectNumber = i;
                ButtonManager.openWindow(frames[i]);
            })
            .AddTo(this);
    }
}
