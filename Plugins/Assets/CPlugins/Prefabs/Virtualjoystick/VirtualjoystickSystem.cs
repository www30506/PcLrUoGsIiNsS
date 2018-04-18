using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualjoystickSystem : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler {
	[SerializeField]private Transform joystickTransform;
	Vector3 startPos;//开始位置
	Vector3 dir;//方向
	[SerializeField]private float radius = 80;//需要移动的半径，这个要根据自己需要移动的距离改变哦，我的是80

	public void OnDrag(PointerEventData eventData){
		dir = (Input.mousePosition - startPos).normalized;
		joystickTransform.position = Input.mousePosition;

		float dis = Vector3.Distance(joystickTransform.position,startPos);
			if (dis > radius)
			joystickTransform.position = startPos + dir * radius;
	}

	public void OnEndDrag(PointerEventData eventData){
		joystickTransform.localPosition = Vector3.zero;
	}

	public void OnBeginDrag(PointerEventData eventData){
		startPos = joystickTransform.position;
	}

	public Vector2 GetDirection(){
		return dir; 
	}
}
