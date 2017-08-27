using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ゲーム共通用のクラス
/// ロード時に値が入る予定
/// </summary>
public class GV
{
    #region Classes
    public class ParamBase
    {
        public string ID;
        public string Name;
    }

    /// <summary>
    /// プレイヤーのステータス
    /// </summary>
    [Serializable]
    public class PlayerParam : ParamBase
    {
        public int HP;
        public int MP;
        public int Atk;
        public int Def;
        public int Dex;
        public int Int;
        public int Luc;
    }

    /// <summary>
    /// Save するゲームデータ
    /// </summary>
    [Serializable]
    public class GameData
    {
        #region Other
        public int playTime;
        #endregion

        #region Player
        public List<PlayerParam> Players;
        #endregion

        #region Items
        // 所持している装備のIDの配列
        public List<int> Equipments;
        #endregion
    }

    static GameData gameData;
    static public GameData GData 
        {
            get {
                if (gameData == null) {
                    initialize();
                }
            return gameData;
        }
    }

    [Serializable]
    public class SystemData
    {
        public int saveCount;
    }
    static SystemData systemData;
    static public SystemData SData
    {
        get {
            if (systemData == null)
            {
                initialize();
            }
            return systemData;
        }
    }

    #endregion

    #region Properties
    static public List<PlayerParam> Players { get; set; }
    #endregion

    #region Methods
    static void initialize()
    {
        // SystemDataの読み込み
        SaveData.setSlot(0);
        SaveData.load();
        systemData = SaveData.getClass<SystemData>("SystemData", null);

        // 仮で作成
        gameData = new GameData();

        // 装備の仮データ読み込み
        {
            gameData.Equipments = new List<int>();
            // 仮で全装備を ID + 1 所持している状態にする
            var equipments = EquipmentManager.getEquipmentList();
            foreach (var equipment in equipments) {
                for (int i = 0; i <= equipment.ID; ++i) {
                    gameData.Equipments.Add(equipment.ID);
                }
            }
        }
    }

    /// <summary>
    /// GameData をセーブする
    /// </summary>
    /// <param name="slot">セーブするスロット(0はシステムデータ保存用)</param>
    static public void save(int slot = 1)
    {
        gameData = new GameData();
        gameData.playTime = 20;

        gameData.Players = new List<PlayerParam>();
        var player = new PlayerParam();
        gameData.Players.Add(player);


        SaveData.setClass("GameData", GData);
        SaveData.save();
    }

    static public void load(int slot = 1)
    {
        SaveData.setSlot(slot);
        SaveData.load();
        gameData = SaveData.getClass<GameData>("GameData", null);

        Debug.Log(gameData.playTime);
    }
    #endregion
}
