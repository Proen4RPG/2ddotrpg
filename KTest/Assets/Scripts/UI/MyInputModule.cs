using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyInputModule : BaseInputModule
{
    [SerializeField]
    string HorizontalAxis = "Horizontal";
    [SerializeField]
    string VerticallAxis = "Vertical";
    [SerializeField]
    string Submit = "Submit";
    [SerializeField]
    string Cancel = "Cancel";
    [SerializeField]
    float RepeatDelay = 0.5f;

    Vector3 prevInput;

    public static bool IsLoop = true;
    public static bool IsRepeatKey = true;

    float pastTime;

    public override void Process()
    {
        if (EventSystem.current.currentSelectedGameObject == null) {
            return;
        }
        pastTime += Time.deltaTime;

        if (IsRepeatKey) {
            if (pastTime < RepeatDelay) {
                return;
            }
        }
        Vector3 inputVec;

        if (!IsRepeatKey) {
            inputVec = RepeatKeyDisable();
        }
        else {
            inputVec = GetKeyVec();
        }

        prevInput = inputVec;

        var currentSelectObj = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        var nextSelect = currentSelectObj.FindSelectable(inputVec);

        if (IsLoop) {
            LoopEnabel(currentSelectObj, inputVec);
        }

        else if (nextSelect) {
            EventSystem.current.SetSelectedGameObject(nextSelect.gameObject);
            pastTime = 0.0f;
        }
    }

    Vector3 GetKeyVec()
    {

        float x = 0.0f;
        float y = 0.0f;

        x = Input.GetAxis(HorizontalAxis);
        y = Input.GetAxis(VerticallAxis);

        if (x < 0.0f) {
            x = -1.0f;
        }
        if (x > 0.0f) {
            x = 1.0f;
        }
        if (y < 0.0f) {
            y = -1.0f;
        }
        if (y > 0.0f) {
            y = 1.0f;
        }

        Vector3 vec = new Vector3(x, y);
        return vec;
    }

    Vector3 RepeatKeyDisable()
    {
        var currentVec = GetKeyVec();
        Debug.Log(currentVec + " " + prevInput);
        //Debug.Log(prevInput == currentVec);
        return prevInput == currentVec ? Vector3.zero : currentVec;
    }

    void LoopEnabel(Selectable currentSelect, Vector3 inputVec)
    {
        var nextSelect = currentSelect.FindSelectable(Vector3.right * inputVec.x);
        if (nextSelect == null) {
            pastTime = 0.0f;
            nextSelect = currentSelect;
            while (nextSelect.FindSelectable(Vector3.left * inputVec.x)) {
                nextSelect = nextSelect.FindSelectable(Vector3.left * inputVec.x);
            }
        }
        currentSelect = nextSelect;
        nextSelect = currentSelect.FindSelectable(Vector3.up * inputVec.y);

        if (nextSelect == null) {
            pastTime = 0.0f;
            nextSelect = currentSelect;

            while (nextSelect.FindSelectable(Vector3.down * inputVec.y)) {
                nextSelect = nextSelect.FindSelectable(Vector3.down * inputVec.y);
            }
        }
        EventSystem.current.SetSelectedGameObject(nextSelect.gameObject);
    }
// Use this for initialization
protected override void Start()
    {
        prevInput = Vector3.zero;
        pastTime = RepeatDelay;
        //var obj = EventSystem.current.currentSelectedGameObject;
        //obj.GetComponent<Selectable>().Find
    }

    // Update is called once per frame
    void Update()
    {

    }
}
