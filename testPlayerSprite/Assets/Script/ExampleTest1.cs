using UnityEngine;

public class ExampleTest1 : EnemyManager {
	// Use this for initialization
	void Start( ) {
		// EnemyManager クラスから参照する方法
		// EnemyManager クラス GetEnemyStatusData から各ステータスにアクセスできます
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster01_ID" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster02_ID" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster03_ID" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster04_ID" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster01_Feeling" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster04_HP" ) + "</color>" );

		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster02_SPD" ) + "</color>" );
		Debug.Log( "<color='red'>" + GetEnemyStatusData( "Monster02_MATK" ) + "</color>" );


	}


}