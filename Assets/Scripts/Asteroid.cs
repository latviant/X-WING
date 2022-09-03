using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * _speed;
        var indicator = IndicatorManager.instance.AddIndicator(gameObject, Color.red);
        indicator.GetDistance(GameManager.instance.GiveTargetPosition());
    }
}
