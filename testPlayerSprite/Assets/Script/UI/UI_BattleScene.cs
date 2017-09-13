using UnityEngine;
using UnityEngine.UI;

/*===============================================================*/
/// <summary>
/// @brief バトルシーンのインタラクティブなUIを管理します, BattleSceneオブジェクトにスクリプトを関連づけます
/// </summary>
public class UI_BattleScene : MonoBehaviour {

	
	//[TooltipAttribute( "攻撃, 防御, スキル, 会話, 入れ替え, アイテム, 逃げる, スキル説明\n" +
	//					"directory : BattleScene/CanvasUI/CommandAndSkillExplanation/" )]
	static GameObject[ , ] lblElement01 = new GameObject[ 3, 4 ];

	//[TooltipAttribute( "コマンド選択部分のフレーム\n" +
	//					"directory : BattleScene/CanvasUI/CommandAndSkillExplanation" )]
	public GameObject CommandFrame;

	//[TooltipAttribute( "敵選択 UI のオブジェクト\n" +
	//					"directory : BattleScene/" )]
	public GameObject[ ] ChoiceArrow;

	//[TooltipAttribute( "バトルシーン上のプレイヤーオブジェクト\n" +
	//					"directory : BattleScene/" )]
	public GameObject[ ] Players;

	// 各種カウント用変数
	private int x, y; // 二次元配列操作用カウント変数
	private int choice; // 敵選択 Arrow
	// 判定用フラグ
	private bool isAttack; // 攻撃ボタン
	// バトルシーンでのエネミーの現在位置を保存する変数の準備
	private Vector3 EnemyTranslate = new Vector3( );
	// バトルシーンでのプレイヤーの現在位置を保存する変数の準備
	private Vector3 PlayerTranslate = new Vector3( );

	/*===============================================================*/
	/// <summary>
	/// @brief 初期化
	/// </summary>
	void Start( ) {
		// X, Y element 7
		lblElement01[ 0, 0 ] = GameObject.Find( "Attack" );
		lblElement01[ 0, 1 ] = GameObject.Find( "Skill" );
		lblElement01[ 0, 2 ] = GameObject.Find( "Change" );
		lblElement01[ 0, 3 ] = GameObject.Find( "Escape" );
		lblElement01[ 1, 0 ] = GameObject.Find( "Defence" );
		lblElement01[ 1, 1 ] = GameObject.Find( "Conversation" );
		lblElement01[ 1, 2 ] = GameObject.Find( "Item" );
		lblElement01[ 1, 3 ] = GameObject.Find( "SkillExplanation" );
		// 二次元配列操作用カウント変数の初期化
		x = 0;
		y = -1;
		// 敵選択 Arrow の初期化
		for( int i = 0; i < ChoiceArrow.Length; i++ ) ChoiceArrow[ i ].SetActive( false );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		// 攻撃コマンドを選択している間は, 処理しない
		if( !isAttack ) {
			// 方向キーによるコマンド操作 攻撃から始まるようにする
			if (	Input.GetKeyDown( KeyCode.RightArrow ) ||
					Input.GetKeyDown( KeyCode.LeftArrow ) && y != -1 ||
					Input.GetKeyDown( KeyCode.UpArrow )	  && y != -1 ||
					Input.GetKeyDown( KeyCode.DownArrow ) && y != -1 ||
					Input.GetKeyDown( KeyCode.Return ) && y != -1 ) {

				// 上キーが押された時 X 軸にマイナス
				if( Input.GetKeyDown( KeyCode.UpArrow ) && x >= 1 && x <= 1 && y != -1 ) x--;
				// 下キーが押されたとき X 軸をプラス
				if( Input.GetKeyDown( KeyCode.DownArrow ) && x >= 0 && x < 1 && y != -1 ) {
					x++;
					// x, y = 1, 3 の時は, x, y = 0, 4 に移動できないようにする
					if ( lblElement01[ x, y ] == lblElement01[ 1, 3 ] ) x--;

				}
				// 右キーが押された時 Y 軸にプラス
				if ( Input.GetKeyDown( KeyCode.RightArrow ) && y < lblElement01.GetLength( 0 ) ) {
					// Y インクリメント
					y++;
					// x, y = 1, 3 の時は, x, y = 0, 4 に移動できないようにする
					if( lblElement01 [ x, y ] == lblElement01[ 1, 3 ] ) y--;
				
				}
				// 左キーが押されたとき Y 軸をマイナス
				if ( Input.GetKeyDown( KeyCode.LeftArrow ) && y > 0 && y != -1 ) y--;
				// ログの出力 コマンド選択の時などに位置確認として使います
				Debug.Log( "<color='blue'>" + x + ", " + y + "</color>" );
				// 変数の準備
				Outline outline = lblElement01[ x, y ].GetComponent<Outline>( );
				Color clr = outline.effectColor;
				//	配列中の color R 値が 255.0f 以上の時, 配列の中身全部を 0.0f にする
				for( int i = 0; i < 2; i++ ) {
					for( int j = 0; j < 4; j++ ) { 
						if( lblElement01[ i, j ].GetComponent<Outline>( ).effectColor.r >= 255.0f ) {
							clr.r = 0.0f;
							lblElement01[ i, j ].GetComponent<Outline>( ).effectColor = clr;
							outline.effectColor = clr;

						}

					}

				}
				// 色情報を戻した後に次の色情報を変える
				clr.r = 255.0f;
				lblElement01[ x, y ].GetComponent<Outline>( ).effectColor = clr;
				outline.effectColor = clr;
				// x, y = 0, 3 の時は, x, y = 0, 4 に移動できないようにする
				if ( lblElement01[ x, y ] == lblElement01[ 0, 3 ] && y < 3 ) y--;
				// 決定キー ( Enter ) が押されたとき
				if( Input.GetKeyDown( KeyCode.Return ) ) {
					Debug.Log( lblElement01[ x, y ].GetComponent<Text>( ).text + " で決定キーが押されました。" );
					// 戦闘コマンド処理
					Combat( lblElement01[ x, y ].GetComponent<Text>( ).text );

				}

			}

		}
		// キャンセルコマンド ( BackSpace ) が押されたらリセットする
		if( Input.GetKeyDown( KeyCode.Backspace ) ) {
			IsEnableCommandGroup( true );
			isAttack = false;
			for( int i = 0; i < ChoiceArrow.Length; i++ ) ChoiceArrow[ i ].SetActive( false );

		}
		// 攻撃コマンドを選んだ時, 敵を選択できるようにする
		if( isAttack ) {
			// 上下十字キーに反応
			if( Input.GetKeyDown( KeyCode.DownArrow ) && choice < ChoiceArrow.Length - 1 ) choice++;
			if( Input.GetKeyDown( KeyCode.UpArrow ) && choice >= 1 ) choice--;
			// 全て Inactive にする
			for( int i = 0; i < ChoiceArrow.Length; i++ ) ChoiceArrow[ i ].SetActive( false );
			// オブジェクト操作
			ChoiceArrow[ choice ].SetActive( true );
			// 決定キー ( Enter ) が押されたとき
			if ( Input.GetKeyDown( KeyCode.Return ) ) {
				Debug.Log( ChoiceArrow[ choice ].transform.root.gameObject.transform.name );
				EnemyTranslate = ChoiceArrow[ choice ].transform.root.gameObject.transform.position;
				Debug.Log( EnemyTranslate );

			}

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief Enterを押した際の攻撃動作処理及びUI動作処理
	/// @param string TEXTコンポーネントを指定します,例:lblElement01[x,y].GetComponent<Text>().text
	/// </summary>
	private void Combat( string str ) {
		switch( str ) {
			case "攻撃" : {
				Debug.Log( "Combat 関数で攻撃が呼ばれました。" );
				IsEnableCommandGroup( false );
				isAttack = true;
				break;

			}
			case "防御" : {
				break;

			}
			case "スキル" : {
				break;

			}
			case "会話" : {
				break;

			}
			case "アイテム" : {
				break;

			}
			case "逃げる" : {
				break;

			}

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief コマンド選択部分及びフレームの active を操作します
	/// @param bool active : true, Inactive : false
	/// </summary>
	private void IsEnableCommandGroup( bool v ) {
		// コマンド選択部分のフレームを操作します
		CommandFrame.SetActive( v );
		// CommandGroup の active 非 active 処理
		for( int i = 0; i < 2; i++ ) {
			for( int  j = 0; j < 4; j++ ) {
				lblElement01[ i, j ].SetActive( v );

			}

		}


	}
	/*===============================================================*/


}
/*===============================================================*/