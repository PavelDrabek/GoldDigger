using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

	public Transform myTransform;
	public MeshParticleEmitter particles;
	public Gun gun;

	public float speed;

	Vector3 origin;
	float actualSpeed;
	GameObject child;
	LineRenderer line;

	void Awake () {
		origin = myTransform.localPosition;
		line = GetComponent<LineRenderer>();
		particles.emit = false;
	}
	
	void Update () {

		myTransform.Translate( -Vector3.up * actualSpeed * GameController.Instance.TimeFlow);
		if(actualSpeed < 0) {
			if(Vector3.Distance(myTransform.position, gun.transform.position) < 0.5f) {
				OnHookReturned();
			}
		}

		line.SetPosition(0, gun.transform.position + Vector3.back * 0.05f);
		line.SetPosition(1, myTransform.position + Vector3.back * 0.05f);
	}

	void OnTriggerEnter(Collider collider) {
		if(child == null) {
			IHitable hittable = collider.GetComponent<IHitable>();
			if(hittable != null) {
				hittable.Hit(this);
			}
		}
		Return();
	}

	void OnHookReturned() {
		if(child != null) {
			Gem gem = child.GetComponent<Gem>();
			if(gem != null) {
				if(gem.gemType == Gem.GemType.Dynamite) {
					gun.dynamites++;
					GameController.Instance.dynamiteText.text = gun.dynamites.ToString();
					ScoreManager.Instance.ShowMessage("Dynamite");
				} else {
					ScoreManager.Instance.AddScore(gem.Score);
				}
			}
			GameController.Instance.map.Gems.Remove(gem);
			DestroyImmediate(child);
		}

		GameController.Instance.SpeedUp(false);
		Reset();
	}

	void Return() {
		actualSpeed = -2 * speed;
		particles.emit = false;
	}

	public void SetCatch(GameObject go) {
		child = go;
		child.transform.SetParent(myTransform);
	}

	public void Go() {
		if(actualSpeed == 0) {
			actualSpeed = speed;
			particles.emit = true;
		}
	}

	public void Reset() {
		actualSpeed = 0;
		gun.Continue();
		myTransform.localPosition = origin;
		particles.emit = false;
		if(child) {
			DestroyImmediate(child);
		}
	}
}
