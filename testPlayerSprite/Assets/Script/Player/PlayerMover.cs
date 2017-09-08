using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief Player の移動処理を管理します プレイヤーオブジェクトに割り当てます
/// </summary>
/*===============================================================*/
public class PlayerMover : MonoBehaviour {

	// プレイヤー制御用 Rigidbody2D
	private Rigidbody2D rbody;
	// プレイヤー移動速度固定値
	[SerializeField, TooltipAttribute( "プレイヤー移動速度" )]
	private float MOVE_SPEED = 3.0f;
	// プレイヤー移動速度
	private float moveSpeed;
	// 移動方向定義
	public enum MOVE_DIR {
		STOP,
		LEFT,
		RIGHT


	}
	// 移動方向
	private MOVE_DIR moveDirection = MOVE_DIR.STOP; // 初期状態は, 停止

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによる初期化
	/// </summary>
	public void Awake( ) {
		// 初期化関数を呼び出す
		Initialize( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 初期化
	/// </summary>
	public void Initialize( ) {
		// プレイヤーの RigidBody2D コンポーネントをセットしておきます
		rbody = GetComponent<Rigidbody2D>( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		// キーを取得します
		if( Input.GetKeyUp( KeyCode.D ) ) moveDirection = MOVE_DIR.STOP;
		if( Input.GetKeyUp( KeyCode.A ) ) moveDirection = MOVE_DIR.STOP;
		if( Input.GetKeyDown( KeyCode.D ) ) moveDirection = MOVE_DIR.RIGHT;
		if( Input.GetKeyDown( KeyCode.A ) ) moveDirection = MOVE_DIR.LEFT;

		
	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって固定フレームレートで呼ばれます
	/// </summary>
	void FixedUpdate( ) {
		// プレイヤーの移動処理
		switch( moveDirection ) {
			// 停止
			case MOVE_DIR.STOP : {
				moveSpeed = 0.0f;
				break;

			}
			// 左に移動
			case MOVE_DIR.LEFT : {
				moveSpeed = MOVE_SPEED * -1;
				break;

			}
			// 右に移動
			case MOVE_DIR.RIGHT : {
				moveSpeed = MOVE_SPEED;
				break;

			}

		}
		// Rigidbody コンポーネントに速度を設定する
		rbody.velocity = new Vector2( moveSpeed, rbody.velocity.y );


		
	}
	/*===============================================================*/


}