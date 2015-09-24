using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MapGenerator : MonoBehaviour {

	[System.Serializable]
	public struct Stuff {
		public string name;
		public Gem prefab;
		public int amount;
	}

	public Camera camera;
	public Transform leftBorder;
	public Transform rightBorder;
	public Transform bottomBorder;

	public Map map;

	public Stuff[] stuffs;

	Vector3 from;
	Vector3 to;

	

	void OnEnable() {

	}

	void Update () {
		if(Input.GetMouseButtonDown(0)) {

		}
	}

	public void NewLevel(int level) {
		CalcBorders();
		Clear();

		stuffs[0].amount = (level > 10) ? 4 : 3;
		Generate();
	}

	void Generate() {
		foreach (Stuff stuff in stuffs) {
			for (int i = 0; i < stuff.amount; i++) {
				Vector3 pos = new Vector3(Random.Range(from.x, to.x), Random.Range(from.y, to.y), 0);
				Gem gem = Instantiate(stuff.prefab, pos, Random.rotation) as Gem;
				gem.SetSize(Random.Range(0.4f, 1f));
				map.AddGem(gem);
			}
		}
	}

	void Clear() {
		map.Clear();
	}

	void CalcBorders() {
		from = ScreenPointToWorldPosition(new Vector3(0, 0, 0));
		to = ScreenPointToWorldPosition(new Vector3(camera.pixelWidth, camera.pixelHeight * 0.65f, 0));

		from.z = 0;
		to.z = 0;

		leftBorder.position = new Vector3(Mathf.Min(from.x, to.x) - 0.5f, 0, 0);
		rightBorder.position = new Vector3(Mathf.Max(from.x, to.x) + 0.5f, 0, 0);
		bottomBorder.position = new Vector3(0, Mathf.Min(from.y, to.y) - 0.5f, 0);
	}

	Vector3 ScreenPointToWorldPosition(Vector3 screnPoint) {
		Ray r = camera.ScreenPointToRay(screnPoint);
		RaycastHit hit;
		if(Physics.Raycast(r, out hit)) {
			return hit.point;
		}

		Debug.LogError("ScreenPoint " + screnPoint + " cannot hit anything");
		return Vector3.zero;
	}
}
