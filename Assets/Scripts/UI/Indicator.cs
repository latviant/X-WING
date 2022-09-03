using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _distanceTo;
    [SerializeField] private Text _distanceLable;
    [SerializeField] private int _margin;
    [SerializeField] private Color _color { set { GetComponent<Image>().color = value; } get{ return GetComponent<Image>().color; } }

    private void Start()
    {
        _distanceLable.enabled = false;
        GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        if(_target == null)
        {
            Destroy(gameObject);
            return;
        }

        if(_distanceTo != null)
        {
            _distanceLable.enabled=true;

            var distance = (int)Vector3.Magnitude(_distanceTo.position - _target.position);
            _distanceLable.text = distance.ToString() + "m";
        }
        else
        {
            _distanceLable.enabled=false;
        }

        GetComponent<Image>().enabled = true;
        var viewportPoint = Camera.main.WorldToViewportPoint(_target.position);

        if(viewportPoint.z < 0)
        {
            viewportPoint.z = 0;
            viewportPoint = viewportPoint.normalized;
            viewportPoint.x *= -Mathf.Infinity;
        }

        var screenPoint = Camera.main.ViewportToScreenPoint(viewportPoint);

        screenPoint.x = Mathf.Clamp(screenPoint.x, _margin, Screen.width - _margin * 2);
        screenPoint.y = Mathf.Clamp(screenPoint.y, _margin, Screen.height - _margin * 2);
        var localPosition = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), screenPoint, Camera.main, out localPosition);
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = localPosition;    
    }

    public void GetTarget(Transform target)
    {
        _target = target;
    }

    public void GetColor(Color color)
    {
        _color = color; 
    }

    public void GetDistance(Transform target)
    {
        _distanceTo = target;
    }

}
