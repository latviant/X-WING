using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private VirtualJoystick _steering;
    [SerializeField] private float _timeToDelay;
    private ShipWeapons _currentWeapons;
    private bool _isFiring = false;

    public void SetWeapons(ShipWeapons weapons)
    {
        this._currentWeapons = weapons;
    }

    public void RemoveWeapons(ShipWeapons weapons)
    {
        if(this._currentWeapons == weapons)
            this._currentWeapons = null;
    }

    public void StartFiring()
    {
        StartCoroutine(FireWeapons());
    }

    private IEnumerator FireWeapons()
    {
        _isFiring = true;

        while(_isFiring)
        {
            if(this._currentWeapons != null)
                _currentWeapons.Fire();

            yield return new WaitForSeconds(_timeToDelay);
        }
    }

    public void StopFiring()
    {
        _isFiring = false;
    }

    public VirtualJoystick GetSterring()
    {
        return _steering;
    }
}
