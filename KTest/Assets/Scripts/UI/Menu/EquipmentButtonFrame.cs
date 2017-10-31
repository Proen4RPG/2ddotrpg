using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButtonFrame : MyMenuFrame
{
    [SerializeField]
    Text[] paramTexts;
    void Start()
    {
        GV.newGame();
    }

    protected override void onOpenWindow()
    {
        Debug.Log("AudioReverbZone");
        paramTexts[0].text = PlayerManager.selectPlayer.HP.ToString();
        paramTexts[1].text = PlayerManager.selectPlayer.MP.ToString();
        paramTexts[2].text = PlayerManager.selectPlayer.Atk.ToString();
        paramTexts[3].text = PlayerManager.selectPlayer.Def.ToString();
        paramTexts[4].text = PlayerManager.selectPlayer.Int.ToString();
        paramTexts[5].text = PlayerManager.selectPlayer.Mgr.ToString();
        paramTexts[6].text = PlayerManager.selectPlayer.Agl.ToString();
        paramTexts[7].text = PlayerManager.selectPlayer.Luc.ToString();
    }
}
