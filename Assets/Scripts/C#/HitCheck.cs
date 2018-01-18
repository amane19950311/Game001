using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当たり判定（ダメージ）
/// </summary>
public class HitCheck : MonoBehaviour {

	[SerializeField]
	private Animator anim;

	[SerializeField]
	Player player;

	private bool downFlg = false;

	[SerializeField]
	private int DOWN_TIME = 60;

	private int downCnt = 0;
	// Use this for initialization
	void Start () {
		downCnt = DOWN_TIME;
	}
	
	// Update is called once per frame
	void Update () {

		//ダウン中
		if(downFlg)
		{
			if (downCnt <= 0) {
				downCnt = DOWN_TIME;
				downFlg = false;
			} else {
				downCnt--;
			}

			anim.SetBool ("Down", downFlg);
		}
	}

	//衝突判定
	void OnCollisionEnter(Collision col)
	{
		if (null != col.gameObject.GetComponent<Player> ()) {
			if (col.gameObject.GetComponent<Player> ().GetPlayerNum () != player.GetPlayerNum ()) {
				if (col.transform.tag == "Attack") {
					downFlg = true;
				}
			}
		}
	}
}
