using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief Player管理クラス Playerオブジェクトにスクリプトを関連づけます
/// </summary>
/*===============================================================*/
public class PlayerManager : MonoBehaviour {

	static private Vector3 move = new Vector3( ); // Player の 座右移動

	private Animator animator; // アニメーター
	private const float playerSpeed = 5.0f; // Player の移動速度を定義

	// CSVLoader loader.GetCSV_Key_Record クラスで使う変数を準備
	// これらの変数には, キー情報とキーに対するデータが入ります
	static string [ ] CSV_CharacterStatusKey = new string [ 1024 ];
	static string [ ] CSV_CharacterStatusKeyData = new string [ 1024 ];

	// 現在のゲームシーン
	private GameManager.GameState state = GameManager.GetGameState( );

	// Use this for initialization
	private void Awake( ) {
		animator = GetComponent< Animator > ( );
		// 初期アニメーションは, 静止
		animator.speed = 0.0f;
		// 初期は, 右方向を向く
		animator.SetBool( "testFlg", true );
		// CSV読み込み機能を使った配列へのデータ読み込み
		CSVLoader loader = new CSVLoader( );
		CSV_CharacterStatusKeyData = loader.GetCSV_Key_Record( "CSV/CSV_CharacterStatus", CSV_CharacterStatusKey );


	}
	
	// Update is called once per frame
	void Update ( ) {
		// player を移動できるようにする
		Move( );
		


	}

	/*===============================================================*/
	/// <summary>
	/// @brief キーボード操作によるプレイヤーの移動
	/// </summary>
	private void Move( ) {
		// BattleScene では, Player は移動しない
		if( state != GameManager.GameState.BATTLE ) {
			/* 右方向移動処理＆アニメーション処理 */
			if ( Input.GetKeyUp( KeyCode.D ) ) {
				/* D ボタンが離されたとき */
				animator.SetBool( "testFlg", false );
				animator.speed = 0.0f;
				move.x = 0.0f;

			} else if ( Input.GetKeyDown( KeyCode.D ) ) {
				/* D ボタンが押されたとき */
				animator.SetBool( "testFlg", true );
				animator.speed = 1.0f;
				animator.Play( "rightWalk@Player01" );
				move.x = playerSpeed * 1 / 60;
				//Debug.Log( move.x );

			}
			/*****************************************/

			/* 左方向移動処理＆アニメーション処理 */
			if ( Input.GetKeyUp( KeyCode.A ) ) {
				/* A ボタンが離されたとき */
				animator.SetBool( "testFlg", true );
				animator.speed = 0.0f;
				move.x = 0.0f;

			} else if ( Input.GetKeyDown( KeyCode.A ) ) {
				/* A ボタンが押されたとき */
				animator.SetBool( "testFlg", false );
				animator.speed = 1.0f;
				animator.Play( "leftWalk@Player01" );
				move.x = -playerSpeed * 1 / 60;
				//Debug.Log( move.x );

			}
			/*****************************************/
			transform.Translate( move.x, 0.0f, 0.0f );

		}


	}
	/*===============================================================*/

	void OnTriggerEnter2D( Collider2D col ) {
		
	}

	/*===============================================================*/
	/// <summary>
	/// @brief Player の X, Y, Z を取得します
	/// @return GetPlayerMove PlayerのX, Y, Zのデータ
	/// </summary>
	static public Vector3 GetPlayerMove( ) { return move; }
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief Player のキーデータを元にキーに対するデータを取得します
	/// @param string 例：コウ_IDなどを指定します
	/// @return GetPlayerStatusData 例:コウ_IDに対するデータ
	/// </summary>
	static public string GetPlayerStatusData( string key ) {
		// data を格納する変数
		string str = "";
		// key を元に該当データを探し出す
		for( int i = 0; i < CSV_CharacterStatusKey.Length; i++ ) {
			// 引数 key と CSV_CharacterStatusKey の値が同じの場合
			if( CSV_CharacterStatusKey[ i ] == key ) {
				// data を str に格納する
				str = CSV_CharacterStatusKeyData[ i ];

			}

		}
		// 格納したデータを返す
		return str;


	}
	/*===============================================================*/


}