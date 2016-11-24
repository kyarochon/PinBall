using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {
	//HingiJointコンポーネントを入れる
	private HingeJoint myHingeJoynt;

	//初期の傾き
	private float defaultAngle = 20;
	//弾いた時の傾き
	private float flickAngle = -20;

	// Use this for initialization
	void Start () {
		//HinjiJointコンポーネント取得
		this.myHingeJoynt = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.defaultAngle);
	}

	// Update is called once per frame
	void Update () {
		//左矢印キーを押した時左フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
			SetAngle (this.flickAngle);
		}
		//右矢印キーを押した時右フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag") {
			SetAngle (this.flickAngle);
		}

		//矢印キー離された時フリッパーを元に戻す
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
			SetAngle (this.defaultAngle);
		}

		//for mobile
		foreach (Touch touch in Input.touches) {
			print ("touch!");
			if (touch.phase == TouchPhase.Began) {
				if (touch.position.x < Screen.width / 2.0f && tag == "LeftFripperTag") {
					SetAngle (this.flickAngle);
				}
				if (touch.position.x >= Screen.width / 2.0f && tag == "RightFripperTag") {
					SetAngle (this.flickAngle);
				}
			} 

			if (touch.phase == TouchPhase.Ended) {
				SetAngle (this.defaultAngle);
			}
		}
	}

	//フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.myHingeJoynt.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoynt.spring = jointSpr;
	}
}
