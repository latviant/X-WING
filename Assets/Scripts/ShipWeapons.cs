using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapons : MonoBehaviour
{
    [SerializeField] GameObject _shotPrefab;
    [SerializeField] Transform[] _firePoints;
    private int _firePointIndex;

    private void Awake()
    {
        InputManager.instance.SetWeapons(this);
    }

    public void Fire()
    {
        if (_firePoints.Length == 0)
            return;

        var firePointsToUse = _firePoints[_firePointIndex];
        Instantiate(_shotPrefab, firePointsToUse.position, firePointsToUse.rotation);
        var audio = firePointsToUse.GetComponent<AudioSource>();
        if(audio)
            audio.Play();

        _firePointIndex++;

        if (_firePointIndex >= _firePoints.Length)
            _firePointIndex = 0;

    }

    public void OnDestroy()
    {
        if(Application.isPlaying == true)
            InputManager.instance.RemoveWeapons(this);
    }
}
