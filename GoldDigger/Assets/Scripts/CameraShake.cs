using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	Transform myTransform;
	Vector3 originalPosition;

	void Start () {
		myTransform = transform;
		originalPosition = myTransform.position;
	}

	public void StartShaking() {
		StartCoroutine(Shaking(0.4f));
	}

	private IEnumerator Shaking(float time) {
		while(time > 0) {
			time -= GameController.Instance.TimeFlow;
			myTransform.position = originalPosition + Random.insideUnitSphere * time;
			yield return 0;
			yield return 0;
		}
		myTransform.position = originalPosition;
	}
}
