using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

	public Transform flame;

	void Update () {
		flame.Rotate(0, Random.Range(-5, 10) * Mathf.Rad2Deg * Time.deltaTime, 0);
	}
}
