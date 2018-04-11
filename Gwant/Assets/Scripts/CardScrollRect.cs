using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScrollRect : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler, IScrollHandler {

    public int dragSpeed = 5;
    public int scrollSpeed = 25;
    public bool rightClick;
    int cardCount = 2;
    public Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Mouse enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //print("Mouse exit");
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            print("drag start");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            print("drag end");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //print("is dragging");
            float change = eventData.delta.x * (dragSpeed / 10f);
            TryMove(change);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            print("click");
    }

    public void OnScroll(PointerEventData eventData)
    {
        float change = -eventData.scrollDelta.y * scrollSpeed;
        TryMove(change);
    }

    private void TryMove(float change)
    {
        if ((CanMoveLeft() && change < 0) || (CanMoveRight() && change > 0))
        {
            transform.position += new Vector3(change, 0);
            pos = transform.position;
        }
    }

    private bool CanMoveLeft()
    {
        return CanMove(true);
    }
    private bool CanMoveRight()
    {
        return CanMove(false);
    }

    private bool CanMove(bool left)
    {
        Transform cards = transform.GetChild(0);
        GameObject first = cards.GetChild(0).gameObject;
        GameObject last = cards.GetChild(cards.childCount - 1).gameObject;
        if (left && last.GetComponent<RectTransform>().position.x > Screen.width - 100)
            return true;
        else if (!left && first.GetComponent<RectTransform>().position.x < 100)
            return true;
        return false;
    }
}
