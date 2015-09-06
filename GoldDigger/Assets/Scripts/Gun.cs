using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Animator animator;
	public Hook hook;
	public float gunSpeed;

	public int dynamites;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			StartHook();
		}
	}

	public void StartHook() {
		animator.speed = 0;
		hook.Go();
	}

	public void Continue() {
		animator.speed = 1;
	}

	public void Reset() {
		hook.particles.ClearParticles();
		hook.Reset();
	}

	public void UseDynamite() {
		if(dynamites > 0) {
			GameController.Instance.BlowsAll(Gem.GemType.Rock);
			dynamites--;
		}
		GameController.Instance.dynamiteText.text = dynamites.ToString();
	}

	public void ResetDynamites() {
		dynamites = 0;
		GameController.Instance.dynamiteText.text = dynamites.ToString();
	}
}
