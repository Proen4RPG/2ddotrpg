using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief 戦闘シーン管理クラス BattleSceneオブジェクトにスクリプトを関連づけます
/// </summary>
/*===============================================================*/
public class BattleScene : MonoBehaviour {

	[SerializeField, TooltipAttribute( "戦闘シーン背景" )]
	public GameObject imgBack; // 戦闘シーン背景

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
		Debug.Log( "BattleSceneClass!" );
		// 場面転換前準備
		SpriteRenderer renderer = imgBack.GetComponent<SpriteRenderer>( );
		Color color = renderer.color;
		color.a = 0.0f; /* 透明にしておく */
		renderer.color = color; // 変更した色情報に変更


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって毎フレーム呼ばれます
	/// </summary>
	void Update ( ) {
		// 場面転換関数を呼ぶ
		manager.FadeIn( imgBack.GetComponent<SpriteRenderer>( ) );


	}
	/*===============================================================*/


}