using System;
using UnityEngine ;
using System.Collections;

public class BezierTemp : MonoBehaviour {
	public float StartPointX = 0;
	public float StartPointY = 0;
	public  float ControlPointX = 20;
	public  float ControlPointY = 50;
	public float EndPointX = 50;
	public  float EndPointY = 0;
	public  float CurveX;
	public  float CurveY;
	public  float BezierTime = 0;
	public float speed = 1;

	void Update() {
		BezierTime = BezierTime + Time.deltaTime * speed;

		if (BezierTime >= 1){
			BezierTime = 0;
		}

		CurveX = (((1-BezierTime)*(1-BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
		this.transform.position = new Vector3(CurveX, CurveY, 0);
	}
}


