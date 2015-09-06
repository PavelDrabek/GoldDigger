using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

	public MeshRenderer mRenderer;
	public Rigidbody mRidigbody;
	public GameObject mGameObject;

	public float lifeTime;

	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0) {
			Destroy(mGameObject);
		}
	}

	public void Set(Material m, Vector3 direction) {
		mRenderer.material = m;
		lifeTime = 3;
		transform.position += direction;
		transform.localScale = Vector3.one * Random.Range(0.1f, 0.3f);
		mRidigbody.AddForce(direction * 3, ForceMode.Impulse);
	}
}
