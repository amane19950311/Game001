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
	GameObject guard = null;

	private bool guardFlg = false;

	private Rigidbody rb;

	[SerializeField]
	private float VELOCITY_RESET = 1;
	private float velocity = 1;

	float GRAVITY = -0.098f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		Move ();		//移動処理

		Action ();		//アクション処理

		//if(!jumpFlg)
		//{
			Jump ();		//ジャンプ処理
		//}

		//transform.position += new Vector3 (0.0f, -0.98f, 0.0f);

		if (transform.position.y < -0.1f) {
			transform.position = new Vector3 (transform.position.x, -0.1f, 1.0f);
		} 

		//rb.AddForce(new Vector3(0f,-9.8f,0f));
		//transform.rotation = new Quaternion (transform.rotation.x,1f, transform.rotation.z,1.0f);
	}

	/// <summary>
	/// 移動処理
	/// </summary>
	private void Move()
	{
		//コントローラ処理
		/*if (Input.GetAxis ("Vertical")) {

		}

		if (Input.GetAxis ("Horizontal")) {

		}*/

		//キーボード処理
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
		//アクション処理
		if (!guardFlg) {
			anim.SetBool ("Jub", Input.GetKey (KeyCode.J) || Input.GetButtonDown ("Action1"));
			anim.SetBool ("HiKick", Input.GetKey (KeyCode.I) || Input.GetButtonDown ("Action2"));
		}

		//ガード処理
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetButtonDown ("Action2")) {
			guardFlg = true;
		} else {
			guardFlg = false;
		}

		guard.SetActive (guardFlg);
	}

	/// <summary>
	/// ジャンプ処理
	/// </summary>
	private void Jump()
	{
		if (!jumpFlg) {
			if (Input.GetKey (KeyCode.Space) || Input.GetButtonDown ("Jump")) {
				jumpFlg = true;
			}
		}
		if (jumpFlg) {
			transform.position += new Vector3(0f,velocity,0f);

			velocity += GRAVITY;

		}			
	}

	/// <summary>
	/// 地面との衝突判定
	/// </summary>
	/// <param name="col">Col.</param>
	private void OnCollisionEnter(Collision col)
	{
		//地面に着地
		if (col.transform.tag == "Field") {
			jumpFlg = false;
			velocity = VELOCITY_RESET;
		}
	}
}
