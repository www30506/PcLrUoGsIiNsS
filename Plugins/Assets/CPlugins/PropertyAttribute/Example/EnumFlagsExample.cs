using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumFlagsExample : MonoBehaviour {
	[System.Flags] public enum AttackType {
		Melee  = (1 <<0),    // 000001
		Fire   = (1 <<1),    // 000010
		Ice    = (1 <<2),    // 000100
		Poison = (1 <<3)     // 001000
	}
	[SerializeField] [EnumFlags] AttackType m_flags;

	void Start () {
		
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.A)){
			print(m_flags);
			if((m_flags & AttackType.Melee) == AttackType.Melee){
				Debug.Log("Melee");
			}
			if((m_flags & AttackType.Fire) == AttackType.Fire){
				Debug.Log("Fire");
			}
			if((m_flags & AttackType.Ice) == AttackType.Ice){
				Debug.Log("Ice");
			}
			if((m_flags & AttackType.Poison) == AttackType.Poison){
				Debug.Log("Poison");
			}
		}
	}
}
