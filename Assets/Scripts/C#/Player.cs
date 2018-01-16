using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {

	[SerializeField]
	private Animator anim = null;

	[SerializeField]
	float move = 0.05f;

	private bool jumpFlg = false;

	[SerializeField]
	private float jumpHeight = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Move ();		//移動処理

		Action ();		//アクション処理

		if(!jumpFlg)
		{
			Jump ();		//ジャンプ処理
		}

		if (transform.position.y < 0.0f) {
			transform.position = new Vector3 (transform.position.x, 0.0f, 4);
			jumpFlg = false;
		}
		//transform.rotation = new Quaternion (transform.rotation.x,1f, transform.rotation.z,1.0f);
	}

	/// <summary>
	/// 移動処理
	/// </summary>
	private void Move()
	{
		/*if (Input.GetAxis ("Vertical")) {

		}

		if (Input.GetAxis ("Horizontal")) {

		}*/

		if (Input.GetKey (KeyCode.A)) {
			transform.position += new Vector3 (-1.0f, 0, 0) * move;
		} else if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (1.0f, 0, 0) * move;
		} else {
			transform.position += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0) * move;
		}
	}

	/// <summary>
	/// アクション処理
	/// </summary>
	private void Action()
	{
		anim.SetBool ("Jub", Input.GetKey (KeyCode.J) || Input.GetButtonDown ("Action1"));
		anim.SetBool ("HiKick", Input.GetKey (KeyCode.I) || Input.GetButtonDown ("Action2"));
	}

	/// <summary>
	/// ジャンプ処理
	/// </summary>
	private void Jump()
	{
		if (Input.GetKey (KeyCode.Space) || Input.GetButtonDown ("Jump")) {
			jumpFlg = true;
		}

		if (jumpFlg) {
			transform.position += new Vector3 (0.0f, jumpHeight, 0.0f);
		}
			
	}
}
