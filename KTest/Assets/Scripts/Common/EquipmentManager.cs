using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

// 装備管理用クラス
public class EquipmentManager
{
    static EquipmentManagerBase equipmentManager = null;

    /// <summary>
    /// 装備用のクラス
    /// </summary>
    [Serializable]
    public struct Equipment
    {
        public int ID;
        public string Name;
        public EquipmentType Type;
        public int Type2;
        public int HP;
        public int MP;
        public int Atk;
        public int Def;
        public int Int;
        public int Mgr;
        public int Agl;
        public int Luc;
    }

    public enum EquipmentType
    {
        Wepon,
        Shield,
        Head,
        Body,
        Shoes,
        Accessory
    }

    static EquipmentManagerBase manager {
        get
        {
            if (equipmentManager == null) {
                equipmentManager = new EquipmentManagerBase();
            }
            return equipmentManager;
        }
    }

    /// <summary>
    /// クラスの初期化
    /// </summary>
    static public void initialize()
    {
        equipmentManager = new EquipmentManagerBase();
    }

    /// <summary>
    /// 所持している装備の一覧を返します
    /// </summary>
    /// <returns>所持している装備の配列</returns>
    static public ReadOnlyCollection<Equipment> possessionEquipments()
    {
        var equipmentIDs = GV.GData.Equipments;
        return Array.AsReadOnly(
            equipmentIDs
            .Select(id => manager.equipments[id])
            .ToArray());
    }
    /// <summary>
    /// 特定の所持している装備タイプの一覧を返します
    /// </summary>
    /// <param name="type">装備の種類</param>
    /// <returns>所持している装備の配列</returns>
    static public ReadOnlyCollection<Equipment> possessionEquipments(EquipmentType type)
    {
        var equipmentIDs = GV.GData.Equipments;
        return Array.AsReadOnly(
            equipmentIDs
            .Select(id => manager.equipments[id])
            .Where(equipment => equipment.Type == type)
            .ToArray());
    }

    /// <summary>
    /// 全ての装備の一覧を返します
    /// </summary>
    /// <returns>装備の配列</returns>
    static public ReadOnlyCollection<Equipment> getEquipmentList()
    {
        return Array.AsReadOnly(
            manager.equipments.Values
            .ToArray());
    }
    /// <summary>
    /// 特定の装備タイプの一覧を返します
    /// </summary>
    /// <param name="type">装備の種類</param>
    /// <returns>装備の配列</returns>
    static public ReadOnlyCollection<Equipment> getEquipmentList(EquipmentType type)
    {
        return Array.AsReadOnly(
            manager.equipments.Values
            .Where(equipment => equipment.ID != 0 && type == equipment.Type)
            .ToArray());
    }

    /// <summary>
    /// 装備を取得
    /// 戻り値で値を変更することができるが、行わないように
    /// </summary>
    /// <param name="equipmentID">取得するID</param>
    /// <returns>装備</returns>
    static public Equipment getEquipment(int equipmentID)
    {
        return manager.equipments[equipmentID];
    }

    class EquipmentManagerBase
    {
        public Dictionary<int, Equipment> equipments;

        public EquipmentManagerBase()
        {
            equipments = new Dictionary<int, Equipment>();

            string fileName = "EquipmentList";
            var jsonData = Resources.Load<TextAsset>("Data/" + fileName).text;

            var equipmentList = JsonUtility.FromJson<Serialization<Equipment>>(jsonData).Target;

            // 装備を ID で探索できるようにする
            foreach (var equipment in equipmentList) {
                equipments[equipment.ID] = equipment;
            }
        }
    }
}
