using UnityEngine;

/*===============================================================*/
/// <summary>
/// @brief BattleSceneObjectにScriptを関連づけます
/// @brief 敵の生成および生成した敵のステータス情報を管理します
/// </summary>
public class BattleEnemyGenerate : EnemyManager {
	// オブジェクト参照
	[SerializeField, TooltipAttribute( "エネミープレハブ" )]
	private GameObject enemyPrefab;
	[SerializeField, TooltipAttribute( "ゲームキャンバス" )]
	private GameObject canvasGame;
	// 敵生成数上限
	private const int MAX_ENEMY = 5;
	// 生成された敵オブジェクトを配列で管理するための変数準備
	static private GameObject[ ] generateEnemy = new GameObject[ MAX_ENEMY ];
	// 敵の生成位置
	private float firstEnemySpawnX = -6.0f;
	private float firstEnemySpawnY = -3.0f;
	// 乱数を格納するための変数の準備
	static private float rndCnt;
	private float rnd;

	// セッターおよびゲッター定義部
	/// <summary>
	/// @brief 生成されたgameobjectをgetします,Enemy[index].nameにid属性が付けられています
	/// </summary>
	static public GameObject[ ] Enemy { get { return generateEnemy; } }
	/// <summary>
	/// @brief 生成された敵の数をgetします
	/// </summary>
	static public float EnemyNumber { get { return rndCnt; } }

	/*===============================================================*/
	/// <summary>
	/// @brief UnityEngine ライフサイクルによる初期化
	/// </summary>
	void Start( ) {
		Initialize( );


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief 初期化
	/// </summary>
	new void Initialize( ) {
		// 敵の数をランダムに生成
		for( rndCnt = 0; rndCnt < Random.Range( 1.0f, MAX_ENEMY ); rndCnt++ ) {
			rnd = Random.Range( 0, CSVLoader.csvId );
			// バトルシーン開始時に敵を生成
			CreateEnemy( ( int )rndCnt );
			Debug.Log( CSVLoader.csvId + ", " + rndCnt );

		}


	}
	/*===============================================================*/

	/*===============================================================*/
	/// <summary>
	/// @brief この関数はプレハブ化されてエネミーオブジェクト限定で使用します
	/// @param int 配列の要素を入れます for文など・・・
	/// </summary>
	void CreateEnemy( int index ) {
		GameObject enemy = ( GameObject )Instantiate( enemyPrefab );
		// EnemyAllow のスプライトレンダラーコンポーネントの取得
		SpriteRenderer renderer = enemy.transform.GetChild( 0 ).GetComponent<SpriteRenderer>( );
		// Enemy 自身のスプライトレンダラーコンポーネントの取得
		SpriteRenderer renderer2 = enemy.GetComponent<SpriteRenderer>( );
		//enemy.transform.SetParent ( canvasGame.transform, false );
		enemy.transform.localPosition = new Vector3 (
			firstEnemySpawnX,
			firstEnemySpawnY,
			0.0f

		);
		// 生成時に, 敵同士が被さらないようにします
		firstEnemySpawnY += renderer.bounds.size.y + renderer2.bounds.size.y + 0.1f;
		// 生成された敵オブジェクトを配列に入れます
		generateEnemy[ index ] = enemy.gameObject;
		// csv を参照し, ランダムに敵を選び gameobject の name 属性に付けます
		generateEnemy[ index ].gameObject.name = GetEnemyStatusData( rnd + "_ID" );


	}
	/*===============================================================*/


}
/*===============================================================*/
