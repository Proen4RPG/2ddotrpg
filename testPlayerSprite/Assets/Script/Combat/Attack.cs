using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/*===============================================================*/
/// <summary>
/// @biref BattleSceneObjectにScriptを関連づけます
/// @biref 攻撃コマンドの処理を管理します
/// </summary>
public class Attack /*: Combat !注意 : extends すると二回呼び出されてしまう！*/ {

	// 取りあえずの実装
	static private List<Combat.CombatState> myCombatState = Combat.GetCombatState;

	/*===============================================================*/
	/// <summary>取りあえずの実装:現状,敵攻撃動作のみに対応,ダメージなどはまだあたえられない</summary>
	public void AAttack( BattleEnemyGenerate generate ) {
		// とりあえずの呼び出し
		Debug.Log( "AAtack Function Call." );

		foreach ( Combat.CombatState item in myCombatState) {
			if( item.isAction ) { // 行動可能フラグが有効な時
				// 敵選択 Arrow を 表示する
				HUD_BattleScene.ApplyMessageText( generate.GetEnemyStatusData
					( item.id.gameObject.name + "_NAME" ) + "の攻撃" );
				// 敵側からの攻撃 → Player 側への攻撃エフェクト表示をする
				// エフェクト再生関数を呼ぶ
				// 2 秒後に出す
				Observable.Timer( TimeSpan.FromMilliseconds( 2000 ) )
					.Subscribe(_ => new 
						PlayerEffect( ).SetEffectPlay( 
							UnityEngine.Random.Range( 0, 3 )/*ランダムに攻撃*/, "IsTest", 36.0f, 36.0f, 0.0f, -70.0f ) );

				// 行動可能フラグを無効にし行動終了とする
				item.isAction = false;

			}

		}
	

	}
	/*===============================================================*/


}
/*===============================================================*/
