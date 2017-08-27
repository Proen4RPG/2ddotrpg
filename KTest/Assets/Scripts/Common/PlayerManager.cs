using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayerManager
{
    // とりあえずの仮実装
    public class Player
    {
        public Player(int playerID)
        {
            // 一致するIDの 情報を取得
            playerParam = GV.GData.Players.First(param => {
                return param.ID == playerID;
            });
            // ここで同じセーブデータに保存されないデータをを読み込める
            // 読み込む際はIDを使用
            updateParam();

            // 武器の剣と斧、体のローブなどの情報が入る
            // まだデータ形式を決めていないので変わる可能性あり
            // とりあえずIDを入れとく
            // 現在は0, 1のどちらかしか入らない
            // それぞれ全部装備可能、剣と斧以外装備可能にする
            enableEquipmentType = playerID;
        }

        GV.PlayerParam playerParam;
        public GV.PlayerParam BaseParam { get { return playerParam; } }

        // 装備補正時の値
        public int MP { private set; get; }
        public int HP { private set; get; }
        public int Atk { private set; get; }
        public int Def { private set; get; }
        public int Int { private set; get; }
        public int Mgr { private set; get; }
        public int Agl { private set; get; }
        public int Luc { private set; get; }

        /// <summary>
        /// パラメーターの再計算
        /// </summary>
        void updateParam()
        {
            HP = playerParam.HP;
            MP = playerParam.MP;
            Atk = playerParam.Atk;
            Def = playerParam.Def;
            Int = playerParam.Int;
            Mgr = playerParam.Mgr;
            Agl = playerParam.Agl;
            Luc = playerParam.Luc;

            foreach (var equipID in playerParam.currentEquipment) {
                var equipment = EquipmentManager.getEquipment(equipID);
                HP  += equipment.HP;
                MP  += equipment.MP;
                Atk += equipment.Atk;
                Def += equipment.Def;
                Int += equipment.Int;
                Mgr += equipment.Mgr;
                Agl += equipment.Agl;
                Luc += equipment.Luc;
            }

        }

        int enableEquipmentType;
        List<EquipmentManager.Equipment> equipments;

        public int EnableEquipmentType {
            get
            {
                return enableEquipmentType;
            }
        }

        public ReadOnlyCollection<EquipmentManager.Equipment> getEqupmets()
        {
            return Array.AsReadOnly(equipments.ToArray());
        }

        // equipPart を enum で実装する予定
        /// <summary>
        /// 装備する
        /// </summary>
        /// <param name="equipPart">装備させる部位</param>
        /// <param name="equipment">装備させるID</param>
        public void Equip(int equipPart, int equipmentID)
        {
            BaseParam.currentEquipment[equipPart] = equipmentID;
            updateParam();
        }
    }

    static public List<Player> players;
    static public void initialize()
    {
        players = new List<Player>();
        players.Add(new Player(0));
        players.Add(new Player(1));
    }

    /// <summary>
    /// 情報取得用に作成
    /// 選択されているプレイヤー
    /// </summary>
    static public Player selectPlayer;
}
