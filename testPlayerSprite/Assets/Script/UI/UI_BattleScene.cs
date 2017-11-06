using UnityEngine;
using UnityEngine.UI;

/*===============================================================*/
/// <summary>バトルシーンのインタラクティブなUIを管理します, BattleSceneオブジェクトにスクリプトを関連づけます</summary>
public class UI_BattleScene : EnemyManager {

	[SerializeField, TooltipAttribute( "コマンドフレームプレハブ" )]
	private GameObject CommandFramePrefabObj ;
	[SerializeField, TooltipAttribute( "ゲームキャンバス" )]
	private GameObject canvasUi;

	/*===============================================================*/
	// コマンドフレームプレハブを操作するための変数の準備
	static private Selectable[ , ] slv;
	private int slvCnt = 0;
	private int commandFrameImgCnt = -1;
	private int pocketElement = 0; // スクロールバーの pocket 何番目を操作できるようにするか
	/*===============================================================*/

	// スキル ( 攻撃名 ) 表示フラグを格納するための変数の準備
	static bool skillMessageFlg = false;

	/*===============================================================*/
	/// <summary>初期化</summary>
	void Start( ) {
		// スキル ( button text )
		string[ ] btnTextSkill = { "いあいぎり", "ぶんなぐる", "あなをほる", "火炎切り", "あてみなげ", "ギガブレイク", "タイキック", "ケツバット"  };
		// 入れ替え ( button text )
		string[ ] btnTextChange = { "キャラ①", "キャラ②", "キャラ③", "キャラ④", "キャラ⑤", "キャラ⑥"  };
		// アイテム ( button text )
		string[ ] btnTextItem = { "傷薬", "毒消し草", "ティアラの薬", "EPチャージII", "ゼラムカプセル", "精霊香", "絶縁テープ", "軟化リキッド", "シールドポーション", "抹茶ぺっきー" };
		// コマンド生成
		slv = CreateCommandFrame( -140.0f, -55.0f, 3, btnTextSkill.Length, btnTextSkill, 4, btnTextChange.Length, btnTextChange, 5, btnTextItem.Length, btnTextItem  );

		ApplyScrollBarBtnTxt( );


	}
	/*===============================================================*/

	/*===============================================================*/
	static public void ApplyScrollBarBtnTxt( ) {
		// TODO
		// combat インスタンスの各ポケット生成処理に関しての案
		// 各ポケットのボタンオブジェクトのインスタンスを複製する
		// ボタンの複製, ホタンの削除をそれぞれ一括で行えるようにする
		//Debug.Log( "<color='red'>ApplyScrollBarBtnTxt : " + slv[ 1, 0 ].transform.GetChild( 0 ) + "</color>" );

	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>毎フレーム呼ばれます</summary>
	void Update( ) {
		// インプットイベント関数を呼びます
		InputEvent( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>コマンドUIおよびButtonを作成します</summary>
	/// <param name="initializePositionX">コマンドフレームの生成位置(X軸)</param>
	/// <param name="initializePositionY">コマンドフレームの生成位置(Y軸)</param>
	/// <param name="pocket">1～6で指定,どの入れ物にボタンを複製するか</param>
	/// <param name="index">ボタンを何個複製するか</param>
	/// <param name="_btnTxt">ボタンにつけるテキストを何にするか※配列0番目の文字列は最後に来ます(バグ)</param>
	/// <returns>このメソッドは各ポケットのbuttonObjを返します</returns>
	private Selectable[ , ] CreateCommandFrame( float initializePositionX, float initializePositionY, int pocket, int index, string[ ] _btnTxt, int pocket2, int index2, string[ ] _btnTxt2,int pocket3, int index3, string[ ] _btnTxt3  ) {
		GameObject prefabObj = ( GameObject )Instantiate( CommandFramePrefabObj );
		GameObject prefabScrollViewObj = prefabObj.transform.GetChild( pocket ).gameObject;
		Selectable[ , ] allBtnObj = new Selectable[ 3, 1024 ]; /* ポケット 3 個分とデータを格納する変数の準備 */
		Selectable[ ] _btnObj = new Selectable[ index ];
		// root game object の位置および指定キャンバスの子とする
		prefabObj.transform.SetParent( canvasUi.transform, false );
		prefabObj.transform.localPosition = new Vector3( initializePositionX, initializePositionY, 0.0f );
		// ポケット 1 番目
		for ( int i = 0; i < index; i++ ) {
			GameObject prefabBtnObj = ( GameObject )Instantiate( prefabObj.transform.GetChild( pocket ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject );
			VerticalLayoutGroup vlg = prefabObj.transform.GetChild( pocket ).GetChild( 0 ).GetChild( 0 ).GetComponent<VerticalLayoutGroup>( );
			Text btnTxt = prefabObj.transform.GetChild( pocket ).GetChild( 0 ).GetChild( 0 ).GetChild( i ).GetChild( 0 ).GetComponent<Text>( );
			prefabBtnObj.transform.SetParent( vlg.transform, false );
			btnTxt.text = _btnTxt[ i ];
			// 先頭のボタンは, 最後のボタンと重複するので無効化する
			if( i == index - 1 ) prefabObj.transform.GetChild( pocket ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject.SetActive( false );
			_btnObj[ i ] = prefabBtnObj.GetComponent<Selectable>( );
			// スクロールバーのボタンに対するクリック処理を付加
			prefabBtnObj.GetComponent<Button>().onClick.AddListener(() => MyOnClick( prefabBtnObj.transform.GetChild( 0 ).gameObject ) );


		}
		// ポケット 2 番目
		Selectable[ ] _btnObj2 = new Selectable[ index2 ];
		for( int i = 0; i < index2; i++ ) {
			GameObject prefabBtnObj = ( GameObject )Instantiate( prefabObj.transform.GetChild( pocket2 ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject );
			VerticalLayoutGroup vlg = prefabObj.transform.GetChild( pocket2 ).GetChild( 0 ).GetChild( 0 ).GetComponent<VerticalLayoutGroup>( );
			Text btnTxt = prefabObj.transform.GetChild( pocket2 ).GetChild( 0 ).GetChild( 0 ).GetChild( i ).GetChild( 0 ).GetComponent<Text>( );
			prefabBtnObj.transform.SetParent( vlg.transform, false );
			btnTxt.text = _btnTxt2[ i ];
			// 先頭のボタンは, 最後のボタンと重複するので無効化する
			if( i == index2 - 1 ) prefabObj.transform.GetChild( pocket2 ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject.SetActive( false );
			_btnObj2[ i ] = prefabBtnObj.GetComponent<Selectable>( );
			// スクロールバーのボタンに対するクリック処理を付加
			prefabBtnObj.GetComponent<Button>().onClick.AddListener(() => MyOnClick( prefabBtnObj.transform.GetChild( 0 ).gameObject ) );


		}
		// ポケット 3 番目
		Selectable[ ] _btnObj3 = new Selectable[ index3 ];
		for( int i = 0; i < index3; i++ ) {
			GameObject prefabBtnObj = ( GameObject )Instantiate( prefabObj.transform.GetChild( pocket3 ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject );
			VerticalLayoutGroup vlg = prefabObj.transform.GetChild( pocket3 ).GetChild( 0 ).GetChild( 0 ).GetComponent<VerticalLayoutGroup>( );
			Text btnTxt = prefabObj.transform.GetChild( pocket3 ).GetChild( 0 ).GetChild( 0 ).GetChild( i ).GetChild( 0 ).GetComponent<Text>( );
			prefabBtnObj.transform.SetParent( vlg.transform, false );
			btnTxt.text = _btnTxt3[ i ];
			// 先頭のボタンは, 最後のボタンと重複するので無効化する
			if( i == index3 - 1 ) prefabObj.transform.GetChild( pocket3 ).GetChild( 0 ).GetChild( 0 ).GetChild( 0 ).gameObject.SetActive( false );
			_btnObj3[ i ] = prefabBtnObj.GetComponent<Selectable>( );
			// スクロールバーのボタンに対するクリック処理を付加
			prefabBtnObj.GetComponent<Button>().onClick.AddListener(() => MyOnClick( prefabBtnObj.transform.GetChild( 0 ).gameObject ) );


		}
		// 攻撃コマンドなどに対するクリック処理を付加
		for( int i = 0; i < prefabObj.transform.GetChild( 0 ).childCount; i++ ) {
			prefabObj.transform.GetChild( 0 ).GetChild( i ).gameObject.GetComponent<Button>( ).onClick.AddListener( ( ) => MyOnClick( slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 0 ).GetChild( commandFrameImgCnt ).GetChild( 0 ).gameObject ) );

		}
		// 各ポケットの button obj を二次元配列に入れる
		for( int i = 0; i < index; i++ ) allBtnObj[ 0, i ] = _btnObj[ i ];
		for( int i = 0; i < index2; i++ ) allBtnObj[ 1, i ] = _btnObj2[ i ];
		for( int i = 0; i < index3; i++ ) allBtnObj[ 2, i ] = _btnObj3[ i ];

		return allBtnObj;


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>攻撃,防御などのcombat部分UIを操作します</summary>
	/// <param name="select">Selectable ゲームオブジェクト,Selectableコンポーネントを入れる</param>
	/// <param name="select2">攻撃や防御などどのボタンを選択するか</param>
	void CommandFrameImg( Selectable select, int select2 ) {
		if( select2 != -1 ) select.transform.parent.parent.parent.parent.GetChild( 0 ).GetChild( select2 ).GetComponent<Selectable>( ).Select( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>キー操作による呼出し命令</summary>
	void InputEvent( ) {
		if( Input.GetKeyDown( KeyCode.DownArrow ) ) {
			/*===============================================================*/
			// スクロールバーの操作処理
			if( slvCnt < slv[ pocketElement, slvCnt ].gameObject.transform.parent.childCount - 1 ) slvCnt++;
			// スクロールバー要素の操作 例 : slv[ 0 /* 操作したいスクロールバー 0 ~ 2 の数値 */, slvCnt ]
			slv[ pocketElement, slvCnt ].Select( );
			slv[ pocketElement, slvCnt ].gameObject.transform.parent.parent.parent.GetChild( 2 ).GetComponent<Scrollbar>( ).value -= 0.2f;
			//Debug.Log( slvCnt > 0 && slvCnt < slv[ pocketElement, slvCnt ].gameObject.transform.parent.childCount );
			/*===============================================================*/

		}
		if( Input.GetKeyDown( KeyCode.UpArrow ) ) {
			/*===============================================================*/
			// スクロールバーの操作処理
			if( slvCnt > 0 && slvCnt < slv[ pocketElement, slvCnt ].gameObject.transform.parent.childCount ) slvCnt--;
			// スクロールバー要素の操作 例 : slv[ 0 /* 操作したいスクロールバー 0 ~ 2 の数値 */, slvCnt ]
			slv[ pocketElement, slvCnt ].Select( );
			slv[ pocketElement, slvCnt ].gameObject.transform.parent.parent.parent.GetChild( 2 ).GetComponent<Scrollbar>( ).value += 0.2f;
			//Debug.Log( slvCnt > 0 && slvCnt < slv[ pocketElement, slvCnt ].gameObject.transform.parent.childCount );
			/*===============================================================*/

		}
		if( Input.GetKeyDown( KeyCode.RightArrow ) ) {
			/*===============================================================*/
			// Combat の操作処理
			if ( commandFrameImgCnt < slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 0 ).childCount - 1 ) commandFrameImgCnt++;
			CommandFrameImg( slv[ 0, 0 ], commandFrameImgCnt );
			/*===============================================================*/

		}
		if( Input.GetKeyDown( KeyCode.LeftArrow ) ) {
			/*===============================================================*/
			// Combat の操作処理
			if ( commandFrameImgCnt != -1 && commandFrameImgCnt > 0 && commandFrameImgCnt < slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 0 ).childCount ) commandFrameImgCnt--;
			CommandFrameImg( slv[ 0, 0 ], commandFrameImgCnt );
			/*===============================================================*/

		}
		if( Input.GetKeyDown( KeyCode.Backspace ) ) {
			/*===============================================================*/
			// back space 時, スクロールバーの出現否か問わず閉じる
			slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 3 ).gameObject.SetActive( false );
			slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 4 ).gameObject.SetActive( false );
			slv[ 0, 0 ].transform.parent.parent.parent.parent.GetChild( 5 ).gameObject.SetActive( false );
			/*===============================================================*/

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>攻撃や防御などの先頭コマンド選択処理</summary>
	/// <param name="combat">MyOnClickから呼び出されたゲームオブジェクトが入ります</param>
	void InputEvent( GameObject combat ) {
		switch( combat.GetComponent<Text>( ).text ) {
			default : {
				// スキル ( 攻撃名 ) 表示
				if( skillMessageFlg ) HUD_BattleScene.ApplyMessageText( combat.GetComponent<Text>( ).text );
				break;

			}
			case "攻撃" : {
				// エフェクト再生関数を呼ぶ
				EnemyEffect.SetEffectPlay( 0, "IsSword", 1.0f, 1.0f, 0.0f, 0.0f );
				HUD_BattleScene.ApplyMessageText( "攻撃" );
				skillMessageFlg = false; // 元に戻す

				/////////////////////////////////////////////////
				/// TODO 攻勢チェンジテスト
				Combat.GetCombatState[ 0 ].isAction = false;
				Combat.GetCombatState[ 1 ].isAction = true;
				/////////////////////////////////////////////////
			
				break;

			}
			case "防御" : {
				// エフェクト再生関数を呼ぶ
				EnemyEffect.SetEffectPlay( 0, "IsSpear", 1.0f, 1.0f, 0.0f, -0.5f );
				skillMessageFlg = false; // 元に戻す
				break;

			}
			case "スキル" : {
				// スクロールバーを閉じる
				combat.transform.parent.parent.parent.GetChild( 4 ).gameObject.SetActive( false );
				combat.transform.parent.parent.parent.GetChild( 5 ).gameObject.SetActive( false );
				// スクロールバーを出現させる
				combat.transform.parent.parent.parent.GetChild( 3 ).gameObject.SetActive( true );
				// スキルスクロールバーを操作できるようにする
				pocketElement = 0;
				// カウント変数の初期化
				slvCnt = 0;
				// スキル ( 攻撃名 ) を表示できるようにする
				skillMessageFlg = true;
				break;

			}
			case "入れ替え" : {
				// スクロールバーを閉じる
				combat.transform.parent.parent.parent.GetChild( 3 ).gameObject.SetActive( false );
				combat.transform.parent.parent.parent.GetChild( 5 ).gameObject.SetActive( false );
				// スクロールバーを出現させる
				combat.transform.parent.parent.parent.GetChild( 4 ).gameObject.SetActive( true );
				// 入れ替えスクロールバーを操作できるようにする
				pocketElement = 1;
				// カウント変数の初期化
				slvCnt = 0;
				skillMessageFlg = false; // 元に戻す
				break;

			}
			case "アイテム" : {
				// スクロールバーを閉じる
				combat.transform.parent.parent.parent.GetChild( 3 ).gameObject.SetActive( false );
				combat.transform.parent.parent.parent.GetChild( 4 ).gameObject.SetActive( false );
				// スクロールバーを出現させる
				combat.transform.parent.parent.parent.GetChild( 5 ).gameObject.SetActive( true );
				// アイテムスクロールバーを操作できるようにする
				pocketElement = 2;
				// カウント変数の初期化
				slvCnt = 0;
				skillMessageFlg = false; // 元に戻す
				break;

			}
			case "逃走" : {
				break;

			}

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>CreateCommandFrameメソッドでAddListenerされたButtonを選択したら呼び出されます</summary>
	/// <param name="n">AddListenerされたゲームオブジェクトが入ります</param>
	void MyOnClick( GameObject n ) {
		Debug.Log( n.GetComponent<Text>( ).text + "番目のボタンが押されました。" );
		// スクロールバーの制御関数を呼び出す
		InputEvent( n );


	}
	/*===============================================================*/


}
/*===============================================================*/