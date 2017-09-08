using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*===============================================================*/
/// <summary>
/// @brief ゲーム進行管理クラス カメラオブジェクトにスクリプトを関連づけます
/// </summary>
/*===============================================================*/
public class GameManager : MonoBehaviour {

	// ゲームの状態定義
	public enum GameState { 
		DUNGEON,	/* ダンジョン */
		BATTLE,		/* 戦闘シーン */

	}

	// ゲーム状態
	static private GameState state;

	// LoadScene に対するフラグ変数
	private bool loadLevelFlg;

	// Use this for initialization
	void Start( ) {
		//state = GameState.DUNGEON;
		Debug.Log( "現在のシーン : " + state.ToString( ) );


	}

	/*===============================================================*/
	/// <summary>
	/// @brief 毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		// GameState によるシーン遷移
		switch ( state ) {
			case GameState.DUNGEON : {
				// BattleScene に遷移出来るようにフラグを変更
				loadLevelFlg = true;
				break;

			}

			case GameState.BATTLE : {
				if( loadLevelFlg ) {
					// LoadLevel
					SceneManager.LoadScene( "BattleScene" );
					// load scene が1回だけ呼ばれるようにする
					loadLevelFlg = false;

				}
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

	/*===============================================================*/
	/// <summary>
	/// @brief 現在のシーン状態取得する関数です
	/// @return GetGameState 現在のゲームシーン
	/// </summary>
	static public GameState GetGameState( ) { return state; }
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 現在の押されている key code を取得します キーを確認したいときに使います
	/// </summary>
	public void DownKeyCheck( ) {
		if ( Input.anyKeyDown ) {
			foreach ( KeyCode code in Enum.GetValues( typeof( KeyCode ) ) ) {
				if ( Input.GetKeyDown( code ) ) {
					Debug.Log( code );
					break;

				}

			}

		}


	}
	/*===============================================================*/


}