using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDie;

    private void Start()
    {
        Destroy(gameObject, _timeToDie);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
