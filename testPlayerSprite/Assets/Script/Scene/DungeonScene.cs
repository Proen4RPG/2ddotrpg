using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// DungeonScene.cs : ダンジョン管理クラス
/// </summary>
public class DungeonScene : MonoBehaviour {

	public GameObject imgBack; /* ダンジョンシーン背景 */
	public bool fadeFlg;
	private int encounter = 0;

	// Use this for initialization
	void Start( ) {
		//GameManager.SetGameState( GameManager.GameState.BATTLE );
		Debug.Log( "DungeonBattleSceneClass!" );


	}

	// Update is called once per frame
	void Update( ) {
		/* 戦闘シーン移行の為のランダムエンカウントテストです */
		if ( PlayerManager.GetPlayerMove( ).x > 0.0f
			|| PlayerManager.GetPlayerMove( ).x < 0.0f ) {
			int test = ( int )Random.Range( 0.0f, 100.0f );
			encounter++;
			if ( test.Equals( 50 ) && encounter > 120 ) {
				encounter = 0;
				FadeOut( );
				fadeFlg = true;
				Debug.Log( "ランダムエンカウントテスト" );

			}
			//DebugDisplayLog.SetDebugString( encounter.ToString( ) );

		}
		//Debug.Log( PlayerManager.GetPlayerMove( ).x );
		/*****************************************************/
		if( fadeFlg ) FadeOut( );


	}

	public void FadeOut( ) {
		/* http://rikoubou.hatenablog.com/entry/2016/01/30/222448 */
		SpriteRenderer renderer = imgBack.GetComponent< SpriteRenderer >( );
		Color color = renderer.color;
		color.r = 1.0f; // RGBのR(赤)値
		color.g = 1.0f; // RGBのG(緑)値
		color.b = 1.0f; // RGBのB(青)値
		if ( 0.0f <= color.a ) color.a -= 0.01f;   // RGBのアルファ値(透明度の値)
		else {
			fadeFlg = false;
			SceneManager.LoadScene( "BattleScene" );

		}
		renderer.color = color; // 変更した色情報に変更
		/******************************************************************/
		//DebugDisplayLog.SetDebugString( color.a.ToString( ) );


	}


}