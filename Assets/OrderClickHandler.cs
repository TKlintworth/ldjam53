using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OrderClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<GameObject> OnOrderClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnOrderClicked.Invoke(gameObject);
    }
}
