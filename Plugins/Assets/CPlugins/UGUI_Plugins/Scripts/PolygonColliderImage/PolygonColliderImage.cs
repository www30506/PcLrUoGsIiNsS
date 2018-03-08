using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Image实现了接口ICanvasRaycastFilter
/// 该接口有函数IsRaycastLocationValid，返回点击是否有效
/// </summary>
[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderImage : Image {

	PolygonCollider2D collider;

	void Awake(){
		collider = GetComponent<PolygonCollider2D>();
	}

	override public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera){
		return ContainsPoint(collider.points,sp);
	}

	//多边形顶点，屏幕点击坐标
	bool ContainsPoint (Vector2[] polyPoints, Vector2 p){
		int j = polyPoints.Length - 1;
		bool inside = false;

		for (int i = 0; i < polyPoints.Length; i++)
		{
			polyPoints[i].x += transform.position.x;
			polyPoints[i].y += transform.position.y;
			if ( ((polyPoints[i].y < p.y && p.y <= polyPoints[j].y) || (polyPoints[j].y < p.y && p.y <= polyPoints[i].y)) &&
				(polyPoints[i].x + (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) * (polyPoints[j].x - polyPoints[i].x)) > p.x)
				inside = !inside;

			j = i;
		}
		return inside;
	}
}