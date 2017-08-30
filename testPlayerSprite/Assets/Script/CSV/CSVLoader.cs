using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
/*===============================================================*/
/**
* CSVを読み込むクラス
* 2014年12月23日 Buravo
*/
public class CSVLoader 
{
    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    public CSVLoader ()
    {
        this.Initialize();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 初期化
    */
    public void Initialize ()
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 実行処理
    */
    public void Execution ()
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief CSVを読み込んで、レコードを所持するデータテーブルを渡す関数
    * @param string 読み込むCSVのファイルパス
    * @return CSVTable CSVのデータテーブルクラス
    */
    public CSVTable LoadCSV (string t_csv_path)
    {
        // データテーブルクラスの生成.
        CSVTable csvTable = new CSVTable();
        // テキストアセットとしてCSVをロード.
        TextAsset csvTextAsset = Resources.Load(t_csv_path) as TextAsset;
        // OS環境ごとに適切な改行コードをCR(=キャリッジリターン)に置換.
        string csvText = csvTextAsset.text.Replace(Environment.NewLine, "\r");
        // テキストデータの前後からCRを取り除く.
        csvText = csvText.Trim('\r');
        // CRを区切り文字として分割して配列に変換.
        string[] csv = csvText.Split('\r');
        // 複数の行を元にリストの生成.
        List<string> rows = new List<string>(csv);
        // 項目名の取得.
        string[] headers = rows[0].Split(',');
        // 項目の格納.
        foreach (string header in headers)
        {
            csvTable.AddHeaders(header);
        }
        // 項目名の削除.
        rows.RemoveAt(0);
        // 1件分のデータであるレコードを生成して追加.
        foreach (string row in rows)
        {
            // 各項目の値へと分割.
            string[] fields = row.Split(',');
            // レコードを追加.
            csvTable.AddRecord(CreateRecord(headers, fields));
        }
        return csvTable;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 項目名をキーに入力項目を格納するレコードを生成する関数
    * @param string[] 項目名
    * @param string[] 入力項目
    * @return CSVRecord 項目名をキーに入力項目を格納するレコード
    */
    private CSVRecord CreateRecord (string[] t_headers, string[] t_fields)
    {
        // レコードを生成.
        CSVRecord record = new CSVRecord();
        // 項目名をキーに入力項目をレコードへ格納.
        for (int i = 0; i < t_headers.Length; ++i)
        {
            record.AddField(t_headers[i], t_fields[i]);
        }
        return record;
    }
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @author Hironari Ushiyama
	/// @brief CSVを読み込み,CSVのデータテーブルを生成し, 配列に格納する関数
	/// @param string CSVファイル名 拡張子は抜かす
	/// @param string[] CSV内容格納配列
	/// </summary>
	public void ArrayInput( string file, string[ ] array ) {
		// ローダーの生成
		CSVLoader loader = new CSVLoader( );
		// 配列カウント用 index
		int index = 0;
		// CSVを読み込み,CSVのデータテーブルを生成
		CSVTable csvTable = loader.LoadCSV( file );
		foreach ( CSVRecord record in csvTable.Records ) {
			foreach ( string header in csvTable.Headers ) {
				array[ index ] = record.GetField( header );
				index++;
				// CSV の内容をデバッグ確認できます
				//Debug.Log( header + " : " + record.GetField( header ) );

			}

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @author Hironari Ushiyama
	/// @brief CSVを読み込み,CSVのID属性に対応したKeyの自動生成とセーブデータへの保存をするクラス
	/// @param string CSVファイル名 拡張子は抜かす
	/// @param string[] CSVキー内容格納配列
	/// </summary>
	public void KeyGeneration( string file, string[ ] keyArray ) {
		// ローダーの生成
		CSVLoader loader = new CSVLoader( );
		// 配列カウント用 index
		int index = 0;
		// 配列 ID 入れていき更新していく変数
		string name = "";
		// CSV を読み込み, CSV のデータテーブルを生成
		CSVTable csvTable = loader.LoadCSV( file );
		foreach ( CSVRecord record in csvTable.Records ) {
			foreach ( string header in csvTable.Headers ) {
				if ( header == "ID" ) {
					// ID を元に Key 名を変更
					name = record.GetField( header );

				}
				// ID 属性を元にキーネームを変更していく
				keyArray[ index ] = name + "_" + header;
				// キーネーム, キーネームに対応した CSV データ
				SaveData.setString( name + "_" + header, record.GetField( header ) );
				// 配列の入れ口をインクリメント
				index++;
				// Debug 出力
				Debug.Log( "セーブデータキー : " + name + "_" + header );
				DebugDisplayLog.displayLog.Add( name + "_" + header );

			}

		}
		// セーブクラスを用いて CSV の内容をセーブする
		GV.save( );


	}
	/*===============================================================*/


}
/*===============================================================*/
