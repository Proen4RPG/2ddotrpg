using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class EquipmentFrame : MyMenuFrame
{
    // 装備する部位
    public enum EquipmentPart
    {
        Wepon,
        Shield,
        Head,
        Body,
        Shoes,
        FirstAccessory,
        SecondAccessory
    }

    [SerializeField]
    GameObject nodePrefab;
    [SerializeField]
    GameObject viewPanel;
    [SerializeField]
    EquipmentPart part;

    [SerializeField]
    Text[] paramTexts;

    bool firstSelect = true;
    void  Awake()
    {
        isCallOnOpenWindow = true;
    }

    protected override void onOpenWindow()
    {
        var equipmentIDs = GV.GData.Equipments;

        foreach (var equipmentID in equipmentIDs) {
            var newObj = Instantiate(nodePrefab, viewPanel.transform);

            var equipmentNode = newObj.GetComponent<EquipmentNode>();
            var equipment = EquipmentManager.getEquipment(equipmentID);

            equipmentNode.initialize(equipment, (int)part);
            settingSelectButton(newObj.GetComponent<Button>(), equipment);
            EventSystem.current.SetSelectedGameObject(newObj);
        }
    }

    /// <summary>
    /// ボタンがハイライトされているときの処理を設定
    /// </summary>
    void settingSelectButton(Button button, EquipmentManager.Equipment selectEquipment)
    {
        string[] paramStr = new string[8];
        paramStr[0] = "HP";
        paramStr[1] = "MP";
        paramStr[2] = "Atk";
        paramStr[3] = "Def";
        paramStr[4] = "Int";
        paramStr[5] = "Mgr";
        paramStr[6] = "Agl";
        paramStr[7] = "Luc";

        int[] currentParams = new int[8];
        currentParams[0] = PlayerManager.selectPlayer.HP;
        currentParams[1] = PlayerManager.selectPlayer.MP;
        currentParams[2] = PlayerManager.selectPlayer.Atk;
        currentParams[3] = PlayerManager.selectPlayer.Def;
        currentParams[4] = PlayerManager.selectPlayer.Int;
        currentParams[5] = PlayerManager.selectPlayer.Mgr;
        currentParams[6] = PlayerManager.selectPlayer.Agl;
        currentParams[7] = PlayerManager.selectPlayer.Luc;

        button.OnDisableAsObservable()
            .Where(_ => !firstSelect)
            .Subscribe(_ => {
                firstSelect = true;
            })
            .AddTo(this);

        button.OnUpdateSelectedAsObservable()
            //.Where(_ => firstSelect)
            .Subscribe(_ => {
                var selectPlayer = PlayerManager.selectPlayer;
                var currentEquipmentID = selectPlayer.BaseParam.currentEquipment[(int)part];
                var currentEquipment = EquipmentManager.getEquipment(currentEquipmentID);

                int[] deltaParams = new int[8];
                deltaParams[0] = selectEquipment.HP  - currentEquipment.HP;
                deltaParams[1] = selectEquipment.MP  - currentEquipment.MP;
                deltaParams[2] = selectEquipment.Atk - currentEquipment.Atk;
                deltaParams[3] = selectEquipment.Def - currentEquipment.Def;
                deltaParams[4] = selectEquipment.Int - currentEquipment.Int;
                deltaParams[5] = selectEquipment.Mgr - currentEquipment.Mgr;
                deltaParams[6] = selectEquipment.Agl - currentEquipment.Agl;
                deltaParams[7] = selectEquipment.Luc - currentEquipment.Luc;

                for (int i = 0; i < deltaParams.Length; ++i) {
                    if (deltaParams[i] == 0) {
                        paramTexts[i].text = currentParams[i].ToString();
                        paramTexts[i].color = Color.black;
                        continue;
                    }

                    paramTexts[i].text = currentParams[i].ToString() + "  ->  " + (currentParams[i] + deltaParams[i]).ToString();

                    if (deltaParams[i] < 0) {
                        paramTexts[i].color = Color.red;
                        continue;
                    }
                    paramTexts[i].color = Color.blue;
                }
                firstSelect = false;
            })
            .AddTo(this);
    }
}
