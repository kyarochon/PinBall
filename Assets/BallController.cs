using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	//ボールが見える可能性のあるz軸の最大値
	private float visiblePosZ = -6.5f;

	//ゲームオーバを表示するテキスト
	private GameObject gameoverText;

	//得点を表示するテキスト
	private GameObject pointText;

	//得点
	private int point = 0;

	//ゲームオーバーになったか
	private bool gameover = false;

	// Use this for initialization
	void Start () {
		//シーン中のGameOverTextオブジェクトを取得
		this.gameoverText = GameObject.Find("GameOverText");

		//シーン中のPointTextオブジェクトを取得
		this.pointText = GameObject.Find("PointText");
	}

	// Update is called once per frame
	void Update () {
		if (!this.gameover) {
			//ボールが画面外に出た場合
			if (this.transform.position.z < this.visiblePosZ) {
				this.gameOver ();
			}
		} else {
			//スペース押したら
			if (Input.GetKeyUp(KeyCode.Space)) {
				this.restart ();
			}

			//タッチされたら(mobile)
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					this.restart ();
					break;
				} 
			}
		}
	}

	//衝突時に呼ばれる関数
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("SmallStarTag"))
		{
			this.point += 10;
		}
		else if (other.gameObject.CompareTag("LargeStarTag"))
		{
			this.point += 5;
		}
		else if (other.gameObject.CompareTag("SmallCloudTag"))
		{
			this.point += 50;
		}
		else if (other.gameObject.CompareTag("LargeCloudTag"))
		{
			this.point += 500;
		}

		this.updatePointText ();
	}

	void updatePointText()
	{
		this.pointText.GetComponent<Text> ().text = "Point:" + this.point.ToString();
	}

	void gameOver()
	{
		this.gameoverText.GetComponent<Text> ().text = "Game Over";
		this.gameover = true;
	}

	void restart()
	{
		this.gameoverText.GetComponent<Text>().text = "";
		this.gameover = false;

		this.point = 0;
		this.updatePointText ();

		this.transform.position = new Vector3 (3.0f, 2.8f, 4.0f);
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
