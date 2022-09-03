using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrust : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        var offset =  Vector3.forward * _speed * Time.deltaTime;
        this.transform.Translate(offset);
    }
}
