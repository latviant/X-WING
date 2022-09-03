using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _damageToSelf;

    private void HitObject(GameObject theObject)
    {
        var theirDamage = theObject.GetComponentInParent<DamageTaking>();

        if (theirDamage != null)
            theirDamage.TakeDamage(_damage);

        var ourDamage = this.GetComponentInParent<DamageTaking>();

        if(ourDamage != null)
            ourDamage.TakeDamage(_damageToSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.gameObject);
    }
}
