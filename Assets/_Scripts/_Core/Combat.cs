using UnityEngine;
using System.Collections;

public class Combat {
	
	private IFightable friend, enemy, attacker, defender;
	private Status friendStatus, enemyStatus, attackerStatus, defenderStatus;
	
	public Combat (IFightable goodguy, IFightable badguy) {
		friend = goodguy;
		enemy = badguy;
		friendStatus = friend.GetStatus();
		enemyStatus = enemy.GetStatus();
	}
	
	public bool Phase() {
		friendStatus.turn += friendStatus.speed + friendStatus.speedbonus;
		enemyStatus.turn += enemyStatus.speed + enemyStatus.speedbonus;
		
		GetNextTurn();
		
		attackerStatus = attacker.GetStatus();
		defenderStatus = defender.GetStatus();
		
		attackerStatus.turn -= defenderStatus.turn;
		
		defenderStatus.health -= (int)(10f * (attackerStatus.attack + attackerStatus.attackbonus)
			* GetArmourReduction(defenderStatus.armour + defenderStatus.armorbonus));
		
		if (defenderStatus.health <= 0) {
			Potion potion = defender.GetBest<Potion>();
			if (potion != null) {
				potion.UseItem(defender as WorldObject);
			}
		}
		
		return false;
	}
	
	void GetNextTurn() {
		if (friendStatus.turn > enemyStatus.turn) {
			attacker = friend;
			defender = enemy;
		} else if (enemyStatus.turn > friendStatus.turn) {
			attacker = enemy;
			defender = friend;
		} else if (friendStatus.speed > enemyStatus.speed) {
			attacker = friend;
			defender = enemy;
		} else if (enemyStatus.speed > friendStatus.speed) {
			attacker = enemy;
			defender = friend;
		} else if (Random.Range(0, 1) == 0) {
			attacker = friend;
			defender = enemy;
		} else {
			attacker = enemy;
			defender = friend;
		}
	}
	
	void Attack() {
		if (attacker == null || defender == null) return;
		defenderStatus.health -= (int)(10f * (attackerStatus.attack + attackerStatus.attackbonus)
			* GetArmourReduction(defenderStatus.armour + defenderStatus.armorbonus));
	}
	
	float GetArmourReduction(float armour) {
		return (1f / (armour + 1f));	
	}
}
