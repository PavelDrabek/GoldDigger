using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour, IHitable {

	public enum GemType { Rock, Coal, Gold, Dynamite }

	public GemType gemType;
	public int score = 100;

	Transform myTranform;
	float size = 1;

	public int Score { get { return (int)(size * score); } }
	public float Size { get { return size; } }

	void Awake() {
		myTranform = transform;
	}

	public void SetSize(float size) {
		this.size = size;
		myTranform.localScale = Vector3.one * size;
	}

	public void Hit (Hook hook)
	{
		hook.SetCatch(gameObject);
	}
}
