using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class OrderPageButtons : MonoBehaviour
{
    public void OnChildPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log("Mouse entered: " + pointerEventData.pointerCurrentRaycast.gameObject.name);
        }
    }

    public void OnChildPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData.pointerEnter != null)
        {
            Debug.Log("Mouse exited: " + pointerEventData.pointerEnter.name);
        }
    }

    public void OnChildPointerClick(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log("Clicked: " + pointerEventData.pointerCurrentRaycast.gameObject.name);
            // if name is left arrow, call the OrderContainer's PreviousPage method
            // if name is right arrow, call the OrderContainer's NextPage method
            if (pointerEventData.pointerCurrentRaycast.gameObject.name == "LeftPageButtonColliderBox")
            {
                Debug.Log("Left Arrow clicked");
                // Call the OrderContainer's PreviousPage method
                OrderContainer orderContainer = FindObjectOfType<OrderContainer>();
                if (orderContainer != null)
                {
                    orderContainer.PreviousPage();
                }
            }
            else if (pointerEventData.pointerCurrentRaycast.gameObject.name == "RightPageButtonColliderBox")
            {
                Debug.Log("Right Arrow clicked");
                // Call the OrderContainer's NextPage method
                OrderContainer orderContainer = FindObjectOfType<OrderContainer>();
                if (orderContainer != null)
                {
                    orderContainer.NextPage();
                }
            }
        }
    }
}
