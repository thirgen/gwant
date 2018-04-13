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
    Rect screenRect = new Rect(50, 50, Screen.width - 100, Screen.height - 100);

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
        //
        if (left && !IsOnScreen(last))
            return true;
        else if (!left && !IsOnScreen(first))
            return true;
        /*
        if (left && last.GetComponent<RectTransform>().position.x > Screen.width - 100)
            return true;
        else if (!left && first.GetComponent<RectTransform>().position.x < 100)
            return true;
        */
        return false;
    }

    private bool IsOnScreen(GameObject go)
    {
        //https://answers.unity.com/questions/918955/is-there-a-simple-way-to-check-if-a-ui-element-is.html

        bool valid = true;
        Vector3[] corners = new Vector3[4];
        go.GetComponent<RectTransform>().GetWorldCorners(corners);
        
        foreach (Vector3 v in corners)
        {
            if (!screenRect.Contains(v))
            {
                valid = false;
                break;
            }
        }
        return valid;
    }

    private void Update()
    {
        //DRAW CARD SCROLL SAFE ZONE
        /* 
        Transform cards = transform.GetChild(0);
        GameObject first = cards.GetChild(0).gameObject;
        GameObject last = cards.GetChild(cards.childCount - 1).gameObject;


        Vector3[] firstcorners = new Vector3[4];
        first.GetComponent<RectTransform>().GetWorldCorners(firstcorners);
        Vector3[] lastcorners = new Vector3[4];
        last.GetComponent<RectTransform>().GetWorldCorners(lastcorners);
        for (int i = 0; i < 4; i += 2)
        {
            Debug.DrawLine(firstcorners[i], firstcorners[i + 1], Color.red);
            Debug.DrawLine(lastcorners[i], lastcorners[i + 1], Color.green);
        }
        Debug.DrawLine(new Vector3(screenRect.xMin, screenRect.yMin), new Vector3(screenRect.xMin, screenRect.yMax), Color.blue);
        Debug.DrawLine(new Vector3(screenRect.xMax, screenRect.yMin), new Vector3(screenRect.xMax, screenRect.yMax), Color.blue);
        Debug.DrawLine(new Vector3(screenRect.xMin, screenRect.yMin), new Vector3(screenRect.xMax, screenRect.yMin), Color.cyan);
        Debug.DrawLine(new Vector3(screenRect.xMin, screenRect.yMax), new Vector3(screenRect.xMax, screenRect.yMax), Color.cyan);
        */
    }
}
