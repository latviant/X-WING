using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
    [SerializeField] private int _hitPoints;
    public GameObject _destructionPrefab;
    [SerializeField] private bool _isGameOver;

    public void TakeDamage(int amount)
    {
        _hitPoints -= amount;

        if (_hitPoints <= 0)
        {
            Destroy(gameObject);

            if(_destructionPrefab != null)
            {
                Instantiate(_destructionPrefab, transform.position, transform.rotation);
            }

            if (_isGameOver == true)
                GameManager.instance.GameOver();
        }
    }
}
