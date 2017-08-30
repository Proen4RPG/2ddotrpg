using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*===============================================================*/
/// <summary>
/// @brief GameManager.cs : ゲーム進行管理クラス
/// </summary>
/*===============================================================*/
public class GameManager : MonoBehaviour {

	/* ゲームの状態定義 */
	public enum GameState {
		/// <summary>update @brief 関数で切り替わったときに何回も呼ばれるのを防ぐ</summary>
		NONE,       
		DUNGEON,	/* ダンジョン */
		BATTLE,		/* 戦闘シーン */

	}

	/* ゲーム状態 */
	static private GameState state;

	// CSVLoader KeyGeneration クラスで使う変数を準備
	// これらの変数には, キー情報が入ります
	private string [ ] CSV_CharacterStatusKey = new string [ 1024 ];
	private string[ ] CSV_EnemyStatusKey = new string[ 1024 ];

	// Use this for initialization
	void Start( ) {
		//state = GameState.DUNGEON;
		Debug.Log( "現在のシーン : " + state.ToString( ) );

		CSVLoader loader = new CSVLoader( );
		// CSV キー生成と CSV データをセーブデータへ保存
		loader.KeyGeneration( "CSV/CSV_CharacterStatus"/* , "Enemy" */, CSV_CharacterStatusKey );
		loader.KeyGeneration( "CSV/CSV_EnemyStatus", CSV_EnemyStatusKey );


	}

	/*===============================================================*/
	/// <summary>
	/// @brief 毎フレーム呼ばれます, カメラオブジェクトにスクリプトを関連づけます
	/// @param GameState GameStateで定義されたシーンをいれます
	/// </summary>
	void Update( ) {
		// GameState によるシーン遷移
		switch ( state ) {
			case GameState.BATTLE : {
				// LoadLevel
				SceneManager.LoadScene( "BattleScene" );
				Debug.Log( "現在のシーン : " + state.ToString( ) );
				break;

			}

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 現在のシーン状態を書き換える関数です
	/// @param GameState GameStateで定義されたシーンをいれます
	/// </summary>
	static public void SetGameState( GameState setState ) { state = setState; }
	/*===============================================================*/


}