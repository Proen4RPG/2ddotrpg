using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief Player のアニメーションを管理します プレイヤーオブジェクトに割り当てます
/// </summary>
/*===============================================================*/
public class PlayerAnimator : PlayerMover {

	// プレイヤーアニメーション制御用 Animator
	private Animator animatorObj;

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによる初期化
	/// </summary>
	void Awake( ) {
		// 初期化関数を呼び出す
		Initialize( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 初期化
	/// </summary>
	void Initialize( ) {
		// プレイヤーの Animator コンポーネントをセットしておきます
		animatorObj = GetComponent<Animator>( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		// プレイヤーの左右移動アニメーション制御
		switch ( moveDirection ) {
			// 停止
			case MOVE_DIR.STOP : {
				animatorObj.speed = 0.0f;
				break;

			}
			// 左アニメーション
			case MOVE_DIR.LEFT : {
				animatorObj.SetBool( "IsWalk", false );
				animatorObj.speed = 1.0f;
				animatorObj.Play( "leftWalk@Player01" );
				break;

			}
			// 右アニメーション
			case MOVE_DIR.RIGHT : {
				animatorObj.SetBool( "IsWalk", true );
				animatorObj.speed = 1.0f;
				animatorObj.Play( "rightWalk@Player01" );
				break;

			}

		}

		//Debug.Log( moveDirection );

		
	}
	/*===============================================================*/


}