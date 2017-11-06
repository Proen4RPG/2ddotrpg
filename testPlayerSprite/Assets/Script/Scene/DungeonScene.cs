using UnityEngine;

/*===============================================================*/
/// <summary>ダンジョン管理クラス</summary>
/// <remarks>DungeonSceneオブジェクトにスクリプトを関連づけます</remarks>
public class DungeonScene : MonoBehaviour {

	[SerializeField, TooltipAttribute( "ダンジョンシーン背景" )]
	public GameObject imgBack; /* ダンジョンシーン背景 */
	private bool fadeFlg;
	private int encounter = 0;

	/*===============================================================*/
	/// <summary>UnityEngineライフサイクルによる初期化 </summary>
	void Awake( ) {
		Initialize( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary> 初期化</summary>
	void Initialize( ) {
		// 呼び出されたときに, ゲーム状態を更新する
		GameManager.SetGetGameState = GameManager.GameState.DUNGEON;


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>UnityEngineライフサイクルによって毎フレーム呼ばれます </summary>
	void Update( ) {
		// 戦闘シーン移行の為のランダムエンカウント
		if ( PlayerMover.GetMove == PlayerMover.MOVE_DIR.LEFT
			|| PlayerMover.GetMove == PlayerMover.MOVE_DIR.RIGHT ) {
			int rnd = ( int )Random.Range( 0.0f, 100.0f );
			encounter++;
			// player 一定移動距離 + 乱数が 50 の時
			//Debug.Log( "<color='red'>乱数 : " + rnd + ", player 移動 : " + encounter + "</color>" );
			if ( rnd.Equals( 50 ) && encounter > 120 ) {
				encounter = 0;
				fadeFlg = true;

			}

		}
		// player 移動中にエンカウント値に達したとき
		if ( fadeFlg ) {
			Color clr = GameManager.FadeOut( imgBack.GetComponent<SpriteRenderer>( ) );
			// 画像が完全に透明になったら GameManager でシーン移行する
			if ( clr.a <= 0.0f ) {
				GameManager.SetGetGameState = GameManager.GameState.BATTLE;
				fadeFlg = false;

			}

		}


	}
	/*===============================================================*/


}