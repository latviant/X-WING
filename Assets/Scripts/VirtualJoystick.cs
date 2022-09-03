using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _thumb;
    [SerializeField] private Vector2 _delta;
    private Vector2 _originalPosition;
    private Vector2 _originalThumbPosition;

    private void Start()
    {
        _originalPosition = this.GetComponent<RectTransform>().localPosition;
        _originalThumbPosition = _thumb.localPosition;
        _thumb.gameObject.SetActive(false);
        _delta = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _thumb.gameObject.SetActive(true);
        Vector3 worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.enterEventCamera, out worldPoint);

        this.GetComponent<RectTransform>().position = worldPoint;
        _thumb.localPosition = _originalThumbPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.enterEventCamera, out worldPoint);
        _thumb.position = worldPoint;
        var size = GetComponent<RectTransform>().rect.size;

        _delta = _thumb.localPosition;
        _delta.x /= size.x / 2.0f;
        _delta.y /= size.y / 2.0f;

        _delta.x = Mathf.Clamp(_delta.x, -1.0f,1.0f);
        _delta.y = Mathf.Clamp(_delta.y, -1.0f,1.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localPosition = _originalPosition;
        _delta = Vector2.zero;

        _thumb.gameObject.SetActive(false);
    }

    public Vector2 GetDelta()
    {
        return _delta;
    }
}
