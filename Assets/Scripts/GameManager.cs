using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _shiPrefab;
    [SerializeField] private GameObject _spaceStation;
    [SerializeField] private Transform _shipStartPosition;
    [SerializeField] private Transform _stationStartPosition;
    [SerializeField] private SmoothFollow _camera;
    [SerializeField] private GameObject _inGameUi;
    [SerializeField] private GameObject _pausedUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private AsteroidSpawner _spawner;
    [SerializeField] private GameObject _currentShip;
    [SerializeField] private GameObject _currentSpaceStation;
    [SerializeField] private Boundary _boundary;
    [SerializeField] private GameObject _warningUI;
 
    public bool IsPlaying { get; private set; }

    private bool _isPaused;

    private void Start()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if (_currentShip == null)
            return;

        float distance = (_currentShip.transform.position - _boundary.transform.position).magnitude;

        if (distance > _boundary.GiveDestroyRadius())
            GameOver();
        else if (distance > _boundary.GiveWarningRadius())
            _warningUI.SetActive(true);
        else
            _warningUI.SetActive(false);
    }

    private void ShowUI(GameObject newUI)
    {
        GameObject[] allUI = { _inGameUi, _gameOverUI, _pausedUI, _mainMenuUI };

        foreach (GameObject UI in allUI)
            UI.SetActive(false);

        newUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ShowUI(_mainMenuUI);
        IsPlaying = false;
        _spawner.ChangeStatusSpawn(2);
    }

    public void StartGame()
    {
        ShowUI(_inGameUi);
        IsPlaying = true;
        
        if(_currentShip != null)
            Destroy(_currentShip);

        if(_currentSpaceStation != null)
            Destroy(_currentSpaceStation);

        _currentShip = Instantiate(_shiPrefab);
        _currentShip.transform.position = _shipStartPosition.position;
        _currentShip.transform.rotation = _shipStartPosition.rotation;

        _currentSpaceStation = Instantiate(_spaceStation);
        _currentSpaceStation.transform.position = _stationStartPosition.position;
        _currentSpaceStation.transform.rotation = _stationStartPosition.rotation;

        _camera.GetTarget(_currentShip.transform);
        _spawner.GetTarget(_currentSpaceStation.transform);
        _spawner.ChangeStatusSpawn(1);
    }

    public void GameOver()
    {
        ShowUI(_gameOverUI);
        IsPlaying = true;

        if(_currentShip != null)
            Destroy(_currentShip);

        if (_currentSpaceStation != null) 
            Destroy(_currentSpaceStation);

        _warningUI.SetActive(false);

        _spawner.ChangeStatusSpawn(2);
    }

    public void SetPaused(bool paused)
    {
        _inGameUi.SetActive(!paused);
        _pausedUI.SetActive(paused);

        if(paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

    }

    public Transform GiveTargetPosition()
    {
        return _currentSpaceStation.transform;
    }
}
