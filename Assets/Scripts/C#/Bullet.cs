using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private int usePlayer;		//所有者

	[SerializeField]
	private int lifeCnt = 60;	//寿命

	[SerializeField]
	private float speed = 0.1f;	//速度

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//位置更新
		transform.position += new Vector3 (speed, 0f, 0f);

		//寿命
		lifeCnt--;
		if (lifeCnt <= 0) {
			//オブジェクトの破棄
			Destroy (gameObject);
			return;
		}
	}

	/// <summary>
	/// 所有権を設定
	/// </summary>
	/// <param name="Num">Number.</param>
	public void SetUsePlayer(int Num)
	{
		usePlayer = Num;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.GetComponent<Player> ().GetPlayerNum () != usePlayer) {
			if (col.transform.tag == "Damage") {
				Destroy (gameObject);
				return;
			}
		}
	}
}
