using System;
using UnityEngine;

/*===============================================================*/
/// <summary>
/// @biref BattleSceneObjectにScriptを関連づけます
/// @biref 戦闘コマンドの基底クラス
/// </summary>
public class Combat : MonoBehaviour {
	protected GameObject[ ] generateEnemies = BattleEnemyGenerate.Enemy;

	// MonoBehaviour は GamgeObject に依存しているので一度ゲームオブジェクトにしなければならない
	// MonoBehaviour で new のような呼び出しを行う場合は, 呼び出そうとしているスクリプトがあるオブジェクトにアタッチする必要がある
	private GameObject thisObj;

	private HUD_BattleScene hud;
	private BattleEnemyGenerate generate;
	public HUD_BattleScene Hud { get { return hud; } }
	public BattleEnemyGenerate Generate { get { return generate; } }

	// 行動順判定の為の変数の準備
	private int[ ] playersSpeed = new int[ 6 ];

	// テスト判定
	bool once;

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
		// 呼び出そうとしているスクリプトは, Combat スクリプトがアタッチされているオブジェクトと同じオブジェクトにアタッチ
		// されているので現在アタッチされているゲームオブジェクトで初期化する
		thisObj = this.gameObject;
		thisObj.GetComponent<BattleEnemyGenerate>( ).Initialize( );

		generate = thisObj.GetComponent<BattleEnemyGenerate>( );
		hud = thisObj.GetComponent<HUD_BattleScene>( );

		playersSpeed[ 0 ] = hud.Player1.SPD;
		playersSpeed[ 1 ] = hud.Player2.SPD;
		playersSpeed[ 2 ] = hud.Player3.SPD;
		playersSpeed[ 3 ] = hud.Player4.SPD;
		playersSpeed[ 4 ] = hud.Player5.SPD;
		playersSpeed[ 5 ] = hud.Player6.SPD;


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによって毎フレーム呼ばれます
	/// </summary>
	void Update( ) {
		//generate = thisObj.GetComponent<BattleEnemyGenerate>( );
		//hud = thisObj.GetComponent<HUD_BattleScene>( );
		Debug.Log( "combat : " + generate.GetEnemyStatusData( generateEnemies[ 0 ].name + "_NAME" ) );
		Debug.Log( "combat : " + hud.Player1._Player1 );
		// テスト判定
		if( !once ) {
			ActionTurn( );
			once = true;

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 敵およびキャラクターの行動順を決定します
	/// </summary>
	void ActionTurn( ) {
		// 昇順ソートをするための変数準備
		int AscendingOrderElement = ( int )BattleEnemyGenerate.EnemyNumber + playersSpeed.Length;
		int[ ] AscendingOrderArray = new int[ AscendingOrderElement ];
		int tmp = 0;
		int tmp2 = 0;
		// 敵の素早さおよびプレイヤーの素早さを格納します
		for ( int i = 0; i < AscendingOrderElement; i++ ) {
			if( i < BattleEnemyGenerate.EnemyNumber )
				AscendingOrderArray[ i ] = int.Parse( generate.GetEnemyStatusData( generateEnemies[ i ].name + "_SPD" ) );
			if( i <= BattleEnemyGenerate.EnemyNumber ) tmp = i;

		}
		for( int i = tmp; i < AscendingOrderElement; i++ ) {
			if( tmp2 < playersSpeed.Length ) {
				AscendingOrderArray[ i ] = playersSpeed[ tmp2 ];
				tmp2++;

			}

		}
		// 昇順ソート
		Array.Sort( AscendingOrderArray );
		Array.Reverse ( AscendingOrderArray );

		for( int i = 0; i < AscendingOrderElement; i++ )
			Debug.Log( "combat AscendingOrder[ " + i + " ] : " + AscendingOrderArray[ i ] );

		// TODO
		// プレイヤー 5 人と生成された敵の素早さをソートできているので
		// このソートされた値とソートされた値がどの敵またはプレイヤーなのか判定する処理を書いていく


	}
	/*===============================================================*/


}
/*===============================================================*/