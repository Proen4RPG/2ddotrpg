using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindow : MonoBehaviour {

    private CharacterWindow characterWindow;

    private PlayerManagerCSV.PlayerParameters param; // PlayerManagerCSV に変更を行いました
	public PlayerManagerCSV.PlayerParameters Param /* PlayerManagerCSV に変更を行いました */
	{
        get
        {
            return param;
        }
        set
        {
            param = value;
        }
    }

    //表示部分
    private Slider HP;
    private Slider MP;
    private Slider EXP;
    private Text TotalEXP;
    private Text ATK;
    private Text MATK;
    private Text DEF;
    private Text MDEF;
    private Text AGI;
    private Text LUK;

    // Use this for initialization
    void Start ()
    {
        HP       = GameObject.Find("HPBar").GetComponent<Slider>();
        MP       = GameObject.Find("MPBar").GetComponent<Slider>();
        EXP      = GameObject.Find("EXPBar").GetComponent<Slider>();
        TotalEXP = GameObject.Find("Value").GetComponent<Text>();
        ATK      = GameObject.Find("ATKValue").GetComponent<Text>();
        MATK     = GameObject.Find("MATKValue").GetComponent<Text>();
        DEF      = GameObject.Find("DEFValue").GetComponent<Text>();
        MDEF     = GameObject.Find("MDEFValue").GetComponent<Text>();
        AGI      = GameObject.Find("AGIValue").GetComponent<Text>();
        LUK      = GameObject.Find("LUKValue").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        DisplayText(ATK, param.ATK);
        DisplayText(MATK, param.MATK);
        DisplayText(DEF, param.DEF);
        DisplayText(MDEF, param.MDEF);
        DisplayText(AGI, param.SPD);
        DisplayText(LUK, param.LUCKY);
    }

    void DisplayText(Text item, int value)
    {
        item.text =value.ToString();
    }
    
    void DisplaySlider(Slider item, int value)
    {
        
    }
}
