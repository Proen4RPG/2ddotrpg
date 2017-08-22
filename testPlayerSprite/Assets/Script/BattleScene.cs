using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
BattleScene.cs : 戦闘シーン管理クラス
****************************************************************/
public class BattleScene : MonoBehaviour {

	public GameObject imgBack; /* 戦闘シーン背景 */

	// Use this for initialization
	void Start () {
		GameManager.SetGameState( GameManager.GameState.BATTLE );
		Debug.Log( "BattleSceneClass!" );

		SpriteRenderer renderer = imgBack.GetComponent<SpriteRenderer>( );
		Color color = renderer.color;
		color.a = 0.0f; /* 透明にしておく */
		renderer.color = color; // 変更した色情報に変更


	}
	
	// Update is called once per frame
	void Update ( ) {
		/* http://rikoubou.hatenablog.com/entry/2016/01/30/222448 */
		SpriteRenderer renderer = imgBack.GetComponent<SpriteRenderer>( );
		Color color = renderer.color;
		color.r = 1.0f;	// RGBのR(赤)値
		color.g = 1.0f;	// RGBのG(緑)値
		color.b = 1.0f;	// RGBのB(青)値
		if( color.a <= 1.0f ) color.a += 0.01f;	// RGBのアルファ値(透明度の値)
		renderer.color = color; // 変更した色情報に変更
		/****************************************************************/
		//DebugDisplayLog.SetDebugString( color.a.ToString( ) );


	}


}