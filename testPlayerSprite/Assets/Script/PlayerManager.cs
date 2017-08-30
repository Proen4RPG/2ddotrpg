using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerManager.cs : Player管理クラス
/// </summary>
public class PlayerManager : MonoBehaviour {

	/// <summary>
	/// キャラクターのステータスキーです
	/// GameData.setString( KEY, DATA ) などで使います
	/// </summary>
	public enum KEY {
		/* start */
		Character01_NAME,
		Character01_HP,
		Character01_MP,
		/*end */

		/* start */
		Character02_NAME,
		Character02_HP,
		Character02_MP,
		/* end */

		/* start */
		Character03_NAME,
		Character03_HP,
		Character03_MP,
		/* end */

		/* start */
		Character04_NAME,
		Character04_HP,
		Character04_MP
		/* end */


	}

	static private Vector3 move = new Vector3( ); /* Player の 座右移動 */

	private Animator animator; /* アニメーター */
	private const float playerSpeed = 5.0f;

	// Use this for initialization
	void Start ( ) {
		animator = GetComponent< Animator > ( );
		animator.speed = 0.0f; /* 初期アニメーションは, 静止 */
		animator.SetBool( "testFlg", true ); /* 初期は, 右方向を向く */


	}
	
	// Update is called once per frame
	void Update ( ) {
		/* 右方向移動処理＆アニメーション処理 */
		if ( Input.GetKeyUp( KeyCode.D ) ) {
			/* D ボタンが離されたとき */
			animator.SetBool( "testFlg", false );
			animator.speed = 0.0f;
			move.x = 0.0f;

		}

		else if ( Input.GetKeyDown( KeyCode.D ) ) {
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

		}

		else if ( Input.GetKeyDown( KeyCode.A ) ) {
			/* A ボタンが押されたとき */
			animator.SetBool( "testFlg", false );
			animator.speed = 1.0f;
			animator.Play( "leftWalk@Player01" );
			move.x = -playerSpeed * 1 / 60;
			//Debug.Log( move.x );

		}
		/*****************************************/
		transform.Translate( move.x, 0.0f, 0.0f );
		//DebugDisplayLog.SetDebugString( move.x.ToString( ) );


	}

	void OnTriggerEnter2D( Collider2D col ) {
		
	}

	/* Player の X, Y, Z を取得します。 */
	static public Vector3 GetPlayerMove( ) { return move; }


}