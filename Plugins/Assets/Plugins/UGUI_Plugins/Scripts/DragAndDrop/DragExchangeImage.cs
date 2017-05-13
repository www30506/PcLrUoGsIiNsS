using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class DragExchangeImage : MonoBehaviour {
	private GameObject grid;
	public static GameObject tempObj;
	private int currentCount;
	public enum Lock{None,LockX, LockY}
	public Lock _lock;
	private Transform thisTransform;

	void Awake(){
		thisTransform = this.transform;
		BoxCollider2D _boxCollider2D = this.GetComponent<BoxCollider2D>();
		grid = thisTransform.parent.gameObject;
		_boxCollider2D.size = grid.GetComponent<GridLayoutGroup>().cellSize;

		EventTrigger _trigger = this.GetComponent<EventTrigger>();

		EventTrigger.Entry _entry = new EventTrigger.Entry();
		_entry.eventID = EventTriggerType.PointerDown;
		_entry.callback = new EventTrigger.TriggerEvent();
		UnityAction<BaseEventData> call = new UnityAction<BaseEventData>(OnDown);
		_entry.callback.AddListener(call);
		_trigger.triggers.Add(_entry);

		EventTrigger.Entry _entry2 = new EventTrigger.Entry();
		_entry2.eventID = EventTriggerType.PointerUp;
		_entry2.callback = new EventTrigger.TriggerEvent();
		UnityAction<BaseEventData> call2 = new UnityAction<BaseEventData>(OnUp);
		_entry2.callback.AddListener(call2);
		_trigger.triggers.Add(_entry2);

		EventTrigger.Entry _entry3 = new EventTrigger.Entry();
		_entry3.eventID = EventTriggerType.Drag;
		_entry3.callback = new EventTrigger.TriggerEvent();
		UnityAction<BaseEventData> call3 = new UnityAction<BaseEventData>(OnDrag);
		_entry3.callback.AddListener(call3);
		_trigger.triggers.Add(_entry3);
	}

	void Start () {}

	void Update () {}

	/**********************************************************
	 * 						OnDown
	 * *******************************************************/
	public void OnDown(BaseEventData p ){
		currentCount = thisTransform.GetSiblingIndex();
		thisTransform.SetParent (grid.transform.parent, true);
		this.GetComponent<BoxCollider2D>().enabled = false;

		DragExchangeImage.tempObj = Instantiate(Resources.Load("DragAndDrop/SpaceImage")) as GameObject;
		DragExchangeImage.tempObj.transform.SetParent(grid.transform, true);
		DragExchangeImage.tempObj.transform.SetSiblingIndex(currentCount);
	}

	/**********************************************************
	 * 						OnDrag
	 * *******************************************************/
	public void OnDrag(BaseEventData p){
		this.GetComponent<RectTransform>().pivot.Set(0,0);
		if(_lock == Lock.LockY){
			float _y = thisTransform.position.y;
			thisTransform.position = new Vector2(Input.mousePosition.x, _y);
		}
		else if(_lock == Lock.LockX){
			float _x = thisTransform.position.x;
			thisTransform.position = new Vector2(_x, Input.mousePosition.y);
		}
		else{
			thisTransform.position = Input.mousePosition;
		}


		RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition,-Vector2.up);
		if(hit.collider != null){
			int _hitCount = hit.transform.GetSiblingIndex();
			if(_hitCount != currentCount){
				DragExchangeImage.tempObj.transform.SetSiblingIndex(_hitCount);
				currentCount = _hitCount;
			}
		}
	}


	/**********************************************************
	 * 						OnUp
	 * *******************************************************/
	public void OnUp(BaseEventData p){
		thisTransform.SetParent (grid.transform, true);
		thisTransform.SetSiblingIndex(tempObj.transform.GetSiblingIndex());

		this.GetComponent<BoxCollider2D>().enabled = true;
		Destroy(DragExchangeImage.tempObj);
	}
}