using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief ダンジョン管理クラス DungeonSceneオブジェクトにスクリプトを関連づけます
/// </summary>
/*===============================================================*/
public class DungeonScene : MonoBehaviour {

	[SerializeField, TooltipAttribute( "ダンジョンシーン背景" )]
	public GameObject imgBack; /* ダンジョンシーン背景 */
	private bool fadeFlg;
	private int encounter = 0;

	// プレイヤー移動クラスを継承します
	PlayerMover mover = new PlayerMover( );
	// ゲームマネージャークラスを継承します
	GameManager manager = new GameManager( );

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによる初期化
	/// </summary>
	void Awake( ) {
		Initialize( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 初期化
	/// </summary>
	void Initialize( ) {
		// 呼び出されたときに, ゲーム状態を更新する
		GameManager.SetGameState( GameManager.GameState.DUNGEON );
		Debug.Log( "DungeonClass!" );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		// 戦闘シーン移行の為のランダムエンカウントテストです
		if ( mover.GetMove == PlayerMover.MOVE_DIR.LEFT
			|| mover.GetMove == PlayerMover.MOVE_DIR.RIGHT ) {
			int test = ( int )Random.Range( 0.0f, 100.0f );
			encounter++;
			if ( test.Equals( 50 ) && encounter > 120 ) {
				encounter = 0;
				fadeFlg = true;
				Debug.Log( "ランダムエンカウントテスト" );

			}

		}
		if( fadeFlg ) {
			Color clr = manager.FadeOut( imgBack.GetComponent<SpriteRenderer>( ) );
			if ( clr.a <= 0.0f ) {
				GameManager.SetGameState( GameManager.GameState.BATTLE );
				fadeFlg = false;

			}

		}


	}
	/*===============================================================*/


}