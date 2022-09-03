using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private Rigidbody _asteroidPrefab;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _variance;
    [SerializeField] private Transform _target;
    private bool _isCanSpawn = true;

    private void Start()
    {
        StartCoroutine(CreateAsteroids());
    }

    IEnumerator CreateAsteroids()
    {
        while(true)
        {
            float nextSpawnTime = _spawnRate + Random.Range(-_variance, _variance);
            yield return new WaitForSeconds(nextSpawnTime);
            yield return new WaitForFixedUpdate();
            CreateNewAsteroid();
        }
    }

    private void CreateNewAsteroid()
    {
        if (_isCanSpawn == false)
            return;

        var asteroidPosition = Random.onUnitSphere * _radius;
        asteroidPosition.Scale(transform.lossyScale);
        asteroidPosition += transform.position;
        var newAsteroid = Instantiate(_asteroidPrefab);
        newAsteroid.transform.position = asteroidPosition;
        newAsteroid.transform.LookAt(_target);  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, _radius);
    }

    public void DestroyAllAsteroids()
    {
        foreach(var asteroid in FindObjectsOfType<Asteroid>())
            Destroy(asteroid.gameObject);
    }

    public void ChangeStatusSpawn(int number)
    {
        switch (number)
        {
            case 1:
                _isCanSpawn = true;
                break;

            case 2:
                _isCanSpawn = false;
                break;
        }
    }

    public void GetTarget(Transform target)
    {
        _target = target;
    }

}
