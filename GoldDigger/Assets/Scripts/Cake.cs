using UnityEngine;
using System.Collections;

public class Cake : MonoBehaviour {

	public Transform myTransform;

	public Transform parent;
	public Transform candle;
	public Transform coco;

	public Vector3 topCenter;
	public float radius;

	void OnEnable() {
		Clear();
		Init(19);
	}

	void Update () {
		
	}

	public void Init(int years) {
		int rows = (years + 9) / 10;
		float angleStep = 36;
		int candles = 0;

		for (int r = 0; r < rows; r++) {
			for (int i = 0; i < 10 && candles < years; i++) {
				float angle = (145 + angleStep * i) * Mathf.Deg2Rad;
				Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * (r + 1) * 0.5f;

				Transform can = Instantiate(candle) as Transform;
				can.SetParent(parent);
				can.localPosition = topCenter + pos;
				can.localRotation = Quaternion.identity;
				candles++;
			}
		}

		for (int i = 0; i < 300; i++) {
			float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
			Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, Random.Range(-topCenter.y, topCenter.y * 0.5f), Mathf.Sin(angle) * radius);

			Transform can = Instantiate(coco) as Transform;
			can.SetParent(parent);
			can.localPosition = pos;
			can.localRotation = Random.rotation;
		}

	}

	void Clear() {
		foreach(Transform child in parent) {
			Destroy(child.gameObject);
		}
	}
}
