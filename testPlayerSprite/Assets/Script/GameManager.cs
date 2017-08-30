using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// GameManager.cs : ゲーム進行管理クラス
/// </summary>
public class GameManager : MonoBehaviour {

	//private int encounter = 0;

	/* ゲームの状態定義 */
	public enum GameState {
		DUNGEON,	/* ダンジョン */
		BATTLE,		/* 戦闘シーン */

	}

	/* ゲーム状態 */
	static private GameState state;

	/* CSV データの読み込み用変数とカウント変数 */
	private string[ ] CSV_CharacterStatus = new string[ 1024 ];
	private int CSV_CharacterStatusCnt = 0;
	/*********************************************************/

	// Use this for initialization
	void Start () {
		//state = GameState.DUNGEON;
		Debug.Log( "現在のシーン : " + state.ToString( ) );

		/*****************************************************/
		// ローダーの生成
		CSVLoader loader = new CSVLoader( );
		// CSVを読み込み,CSVのデータテーブルを生成.
		CSVTable csvTable = loader.LoadCSV( "CSV/CSV_CharacterStatus" ); /* キャラクターステータス情報を読み込む */
		foreach ( CSVRecord record in csvTable.Records ) {
			foreach ( string header in csvTable.Headers ) {
				CSV_CharacterStatus[ CSV_CharacterStatusCnt ] = record.GetField( header );
				CSV_CharacterStatusCnt++;
				Debug.Log( header + " : " + record.GetField( header ) ); /* CSV の内容をデバッグ確認できます */

			}

		}

		/* CSV のデータを配列に格納する */
		for( int i = 0; i < CSV_CharacterStatus.Length; i++ ) {
			if( CSV_CharacterStatus[ i ] != null ) {
				/* セーブ するデータを格納 */
				//GV.GameData.ID = "1";
				//GV.GameData.Name = CSV_CharacterStatus[ 0 ];
				//int.TryParse( CSV_CharacterStatus[ 1 ], out GV.GameData.HP );
				//int.TryParse( CSV_CharacterStatus[ 2 ], out GV.GameData.MP );
				//Character01.Name = CSV_CharacterStatus[ 13 ];

				/* CSV_CharacterStatus の内容をセットする */
				SaveData.setString( PlayerManager.KEY.Character01_NAME.ToString( ), CSV_CharacterStatus[ 0 ] );
				SaveData.setInt( PlayerManager.KEY.Character01_HP.ToString( ), int.Parse( CSV_CharacterStatus [ 1 ] ) );
				SaveData.setInt( PlayerManager.KEY.Character01_MP.ToString( ), int.Parse( CSV_CharacterStatus [ 2 ] ) );

				SaveData.setString( PlayerManager.KEY.Character02_NAME.ToString( ), CSV_CharacterStatus [ 13 ] );
				SaveData.setInt( PlayerManager.KEY.Character02_HP.ToString( ), int.Parse( CSV_CharacterStatus [ 14 ] ) );
				SaveData.setInt( PlayerManager.KEY.Character02_MP.ToString( ), int.Parse( CSV_CharacterStatus [ 15 ] ) );

				SaveData.setString( PlayerManager.KEY.Character03_NAME.ToString( ), CSV_CharacterStatus [ 26 ] );
				SaveData.setInt( PlayerManager.KEY.Character03_HP.ToString( ), int.Parse( CSV_CharacterStatus [ 27 ] ) );
				SaveData.setInt( PlayerManager.KEY.Character03_MP.ToString( ), int.Parse( CSV_CharacterStatus [ 28 ] ) );

				SaveData.setString( PlayerManager.KEY.Character04_NAME.ToString( ), CSV_CharacterStatus [ 39 ] );
				SaveData.setInt( PlayerManager.KEY.Character04_HP.ToString( ), int.Parse( CSV_CharacterStatus [ 40 ] ) );
				SaveData.setInt( PlayerManager.KEY.Character04_MP.ToString( ), int.Parse( CSV_CharacterStatus [ 41 ] ) );

				/* 該当データが何番目に格納されているかの確認 Debug 用 */
				Debug.Log( i + "番目 : " + CSV_CharacterStatus[ i ] );

			}

		}
		/*****************************************************/

		/* save / load 機能の確認 */
		GV.save( );
		//GV.load( );
		/*********/


	}

	// Update is called once per frame
	void Update () {


	}

	/// <summary>
	/// 現在のシーン状態を書き換える関数です
	/// <param name="setState">GameStateで定義されたシーンをいれます</param>
	/// </summary>
	static public void SetGameState( GameState setState ) { state = setState; }


}