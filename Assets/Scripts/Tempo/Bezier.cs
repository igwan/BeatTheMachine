using System;
using UnityEngine ;
using System.Collections;

public class Bezier {
    public float StartPointX = 0;
	public float StartPointY = 0;
	public  float ControlPointX = 20;
	public  float ControlPointY = 50;
	public float EndPointX = 50;
	public  float EndPointY = 0;
	public  float CurveX;
	public  float CurveY;
	public  float BezierTime = 0;
	private float z;
	private float speed ;
	private Vector3 target ;

	public void setCurve(float startPointX, float startPointY, float controlPointX, float controlPointY, Vector3 target, float z, float speed){
		this.StartPointX = startPointX;
		this.StartPointY = startPointY;

		this.ControlPointX = controlPointX;
		this.ControlPointY = controlPointY;

		this.EndPointX = target.x;
		this.EndPointY = target.y;

		this.target = target;
		this.speed = speed;
		this.z = z;

		this.BezierTime = 0;
	}

	public Vector3 UpdateCurve() {
		BezierTime = BezierTime + Time.deltaTime * speed;

		if (BezierTime >= 1){
			return target;
		}

		CurveX = (((1-BezierTime)*(1-BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
		return new Vector3(CurveX, CurveY, z);
   }
}


