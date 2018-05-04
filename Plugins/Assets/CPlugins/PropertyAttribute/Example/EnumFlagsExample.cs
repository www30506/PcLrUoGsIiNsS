using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumFlagsExample : MonoBehaviour {
	[System.Flags] public enum AttackType {
		// Decimal     // Binary
		None   = 0,    // 000000
		Melee  = 1,    // 000001
		Fire   = 2,    // 000010
		Ice    = 4,    // 000100
		Poison = 8     // 001000
	}
	[SerializeField] [EnumFlags] AttackType m_flags;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
