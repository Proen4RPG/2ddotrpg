using UnityEngine;

public class ExampleTest : PlayerManager {
	// Use this for initialization
	void Start( ) {
		// PlayerManager クラスから参照する方法
		// PlayerManager クラスにあるメンバーから各ステータスにアクセスできます
		Debug.Log( "<color='red'>" + Player1._Player1 + "</color>" );
		Debug.Log( "<color='red'>" + Player2._Player2 + "</color>" );
		Debug.Log( "<color='red'>" + Player3._Player3 + "</color>" );
		Debug.Log( "<color='red'>" + Player4._Player4 + "</color>" );
		Debug.Log( "<color='red'>" + Player5._Player5 + "</color>" );
		Debug.Log( "<color='red'>" + Player6._Player6 + "</color>" );

		Debug.Log( "<color='red'>" + Player5.WEAPON02 + "</color>" );
		Debug.Log( "<color='red'>" + Player6.HP + "</color>" );


	}


}