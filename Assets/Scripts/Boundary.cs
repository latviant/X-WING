using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private float _warningRadius;
    [SerializeField] private float _destroyRadius;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _warningRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _destroyRadius);
    }

    public float GiveWarningRadius()
    {
        return _warningRadius;
    }

    public float GiveDestroyRadius()
    {
        return _destroyRadius;
    }
}
