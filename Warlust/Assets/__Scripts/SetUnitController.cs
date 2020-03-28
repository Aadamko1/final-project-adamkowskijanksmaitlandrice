﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetUnitController : MonoBehaviour {
	[Header("Set in Inspector")]
	public float attackerMinX = -2.5f;
	public float attackerMaxX = -0.5f;
	public float defenderMinX = 6.5f;
	public float defenderMaxX = 8.5f;

	[Header("Set Dynamically")]
    public Tilemap land;
	public Army currentArmy;

	private List<string> unitDescriptions;
	private int index;
	private bool attackersTurn;

    // Start is called before the first frame update
    void Start() {
		land = TMapController.M.land;
        currentArmy = TMapController.M.attacker;
		unitDescriptions = currentArmy.unitDescriptions;
		index = 0;
		print("Place your " + unitDescriptions[index]);
		attackersTurn = true;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector3Int cel = land.WorldToCell(pos);
            pos = land.CellToWorld(cel);
			print(pos.x);
			if (attackersTurn && (pos.x < attackerMinX || pos.x > attackerMaxX)) return;
			else if (!attackersTurn && (pos.x < defenderMinX || pos.x > defenderMaxX)) return;

			currentArmy.SetUnit(index, cel);
			index++;
			if (index == unitDescriptions.Count) {
				if (attackersTurn) {
					currentArmy = TMapController.M.defender;
					unitDescriptions = currentArmy.unitDescriptions;
					index = 0;
					attackersTurn = false;
					print("Place your " + unitDescriptions[index]);
				} else {
					TMapController.M.StartBattle();
				}
			} else print("Place your " + unitDescriptions[index]);
        }
    }
}