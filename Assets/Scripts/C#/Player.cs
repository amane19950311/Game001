using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {

	enum ACTION_STATE{
		Idle = 0,
		Jub,
		Kick,
		MAX
	};

	class Info{
		string ActionName;	//名前
		int Damage;		//ダメージ
		bool JumpUse;	//ジャンプ中に使用できるアクションか
		bool MoveUse;	//移動中に使用できるアクションか

		public void DamageCheck(int life)
		{
			life -= Damage;
		}

		public bool JumpUseCheck(bool jump){return (jump == JumpUse) ? true : false;} 
		public bool MoveUseCheck(bool move){return (move == MoveUse) ? true : false;} 

		public void SetInfo(string s , int d , bool j , bool m)
		{
			ActionName = s;
			Damage = d;
			JumpUse = j;
			MoveUse = m;
		}
	}


	//プレイヤー情報関連
	private Rigidbody rb;		//RigidBodyの取得

	[SerializeField]
	private int playerNum;

	[SerializeField]
	private Animator anim = null;	//アニメーターの取得

	[SerializeField]
	private int life = 100;

	//移動関連
	[SerializeField]
	private float VELOCITY_RESET = 1;	//加速度の定数

	private float velocity = 1;		//加速度

	[SerializeField]
	private float move = 0.05f;		//移動量

	[SerializeField]//テスト
	private bool moveFlg = false;	//移動フラグ

	//ジャンプ関連
	[SerializeField]//テスト
	private bool jumpFlg = false;	//ジャンプフラグ

	private bool onGroundFlg = true;	//地面着地フラグ

	//ガード関連
	[SerializeField]
	GameObject guard = null;		//ガードオブジェクト

	private bool guardFlg = false;	//ガードフラグ



	float GRAVITY = -0.098f;	//重力

	[SerializeField]
	GameObject bulletPrefab;

	[SerializeField]
	Transform bulletCreatePos;

	bool bulletFlg = false;
	int bulletCreateCnt = 30;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {


		Move ();		//移動処理

		Action ();		//アクション処理

		Jump ();		//ジャンプ処理

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
		//左移動
		if (Input.GetKey (KeyCode.A)) {
			
			transform.position += new Vector3 (-1.0f, 0, 0) * ((jumpFlg) ? move * 2.0f : move);

			//移動フラグ
			moveFlg = true;

			//再生
			anim.SetFloat ("Return", 1);

		}

		//右移動
		else if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (1.0f, 0, 0) * ((jumpFlg) ? move * 2.0f : move);

			//移動フラグ
			moveFlg = true;

			//逆再生
			anim.SetFloat ("Return", -1);
		} 

		//停止
		else {
			transform.position += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0) * move;

			//移動フラグ
			moveFlg = false;

		}

		anim.SetBool ("Step",moveFlg);	//移動アニメーション


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
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetButtonDown ("Action2")) {
			guardFlg = true;
		} else {
			guardFlg = false;
		}

		//バレット処理
		if (!bulletFlg) {
			if (Input.GetKeyDown (KeyCode.K)) {

				if (!bulletFlg) {
					
				}

				bulletFlg = true;
			}
		} else {
			if (bulletCreateCnt <= 0) {
				bulletFlg = false;
				bulletCreateCnt = 30;
			} else {
				bulletCreateCnt--;

				if (bulletCreateCnt == 15) {
					GameObject b = Instantiate (bulletPrefab, bulletCreatePos.position, Quaternion.identity);

					b.GetComponent<Bullet> ().SetUsePlayer (playerNum);
				}
			}
		}
		anim.SetBool ("Bullet", bulletFlg);


		guard.SetActive (guardFlg);
	}

	/// <summary>
	/// ジャンプ処理
	/// </summary>
	private void Jump()
	{		
		//ジャンプ中でなければジャンプを有効
		if (!jumpFlg) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("Jump")) {
				jumpFlg = true;
				onGroundFlg = false;
			}
		}

		//ジャンプ中処理
		if (jumpFlg) {
			transform.position += new Vector3(0f,velocity,0f);

			velocity += (velocity < 0.5f && velocity > -0.5f) ? GRAVITY * 0.2f : GRAVITY;

		}	

		anim.SetBool ("Jump", jumpFlg);		//ジャンプアニメーション
	}

	/// <summary>
	/// 地面との衝突判定
	/// </summary>
	/// <param name="col">Col.</param>
	private void OnCollisionEnter(Collision col)
	{
		//地面に着地
		if (col.transform.tag == "Field") {
			onGroundFlg = true;		//着地フラグ
			jumpFlg = false;
			velocity = VELOCITY_RESET;	//加速度の初期化
		}
	}

	public int GetPlayerNum()
	{
		return playerNum;
	}
}
