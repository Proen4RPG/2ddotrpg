using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EquipmentNode : MonoBehaviour
{
    /// <summary>
    /// 装備のどの初期化
    /// </summary>
    /// <param name="equipment">作成する装備</param>
    public void initialize(EquipmentManager.Equipment equipment, int part)
    {
        var button = GetComponent<Button>();
        var text = GetComponentInChildren<Text>();
        text.text = equipment.Name;

        button.OnClickAsObservable()
            .Subscribe(_ => {
                var selectPlayer = PlayerManager.selectPlayer;
                selectPlayer.Equip(part, equipment.ID);
               // FrameManager.cancel();
            })
            .AddTo(this);
    }
}
