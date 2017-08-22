using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    MyMenuFrame menu;

    // Use this for initialization
    void Start()
    {
        GV.save();
        GV.load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && ButtonManager.isAllClosed()) {
            ButtonManager.openWindow(menu);
        }
        if (Input.GetButtonDown("Cancel")) {
            ButtonManager.cancel();
        }
    }
}
