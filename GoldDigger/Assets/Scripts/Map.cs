using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public Gun gun;
	List<Gem> gems = new List<Gem>();

	public List<Gem> Gems { get { return gems; } }

	int maxScore;
	public int MaxScore { get { return maxScore; } }

	public void Clear() {
		foreach(Transform child in transform) {
			Destroy(child.gameObject);
		}
		gems.Clear();
		gun.Reset();

		maxScore = 0;
	}

	public void AddGem(Gem gem) {
		gems.Add(gem);
		gem.transform.SetParent(transform);

		maxScore += gem.Score;
	}
}
