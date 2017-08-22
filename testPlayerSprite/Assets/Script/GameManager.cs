using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/****************************************************************
GameManager.cs : ゲーム進行管理クラス
****************************************************************/
public class GameManager : MonoBehaviour {

	//private int encounter = 0;

	/* ゲームの状態定義 */
	public enum GameState {
		DUNGEON,	/* ダンジョン */
		BATTLE,		/* 戦闘シーン */

	}

	/* ゲーム状態 */
	static private GameState state;

	// Use this for initialization
	void Start () {
		//state = GameState.DUNGEON;
		Debug.Log( "現在のシーン : " + state.ToString( ) );


	}
	
	// Update is called once per frame
	void Update () {
		//if( state.Equals( GameState.DUNGEON ) ) {
		//	/* 戦闘シーン移行の為のランダムエンカウントテストです */
		//	if( PlayerManager.GetPlayerMove( ).x > 0.0f
		//		|| PlayerManager.GetPlayerMove( ).x < 0.0f ) {
		//		int test = ( int )Random.Range( 0.0f, 100.0f );
		//		encounter++;
		//		if ( test.Equals( 50 ) && encounter > 120 ) {
		//			encounter = 0;
		//			SceneManager.LoadScene( "BattleScene" );
		//			Debug.Log( "ランダムエンカウントテスト" );

		//		}
		//		DebugDisplayLog.SetDebugString( encounter.ToString( ) );

		//	}
			//Debug.Log( PlayerManager.GetPlayerMove( ).x );
			/*****************************************************/

		//}


	}

	/* ゲーム状態更新 */
	static public void SetGameState( GameState setState ) { state = setState; }


}