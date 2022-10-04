using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    static GameManager _instance;
    public Player playerReference { get { return _player; } }
    public Player _player;

    LevelManager _levelManager;
    PauseManager _pauseManager;
    BoundManager _boundManager;
    AsteroidManager _asteroidManager;

    [Header("Bounds")]
    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;
    [SerializeField] Color _colorBounds;

    [Header("Asteroids")]
    [SerializeField] float _boundOffset;
    [SerializeField] float _spawnTimeAsteroid;
    [SerializeField] Color _colorAsteroid;

    void Awake()
    {
        Time.timeScale = 1f;

        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _levelManager = new LevelManager();
        _pauseManager = new PauseManager();
        _boundManager = new BoundManager(_boundWidth, _boundHeight);
        _asteroidManager = new AsteroidManager(_spawnTimeAsteroid, _boundWidth, _boundHeight, _boundOffset);
    }

    void Update()
    {
        _asteroidManager.ArtificialUpdate();
    }

    public Vector3 ApplyBounds(Vector3 objectPosition) { return _boundManager.ApplyBounds(objectPosition); }
    void FindPlayer() { _player = FindObjectOfType<Player>(); }
    void Pause() { _pauseManager.Pause(); }
    void UnPause() { _pauseManager.UnPause(); }
    void ChangeScene(string sceneToLoad) { _levelManager.ChangeScene(sceneToLoad); }

    void OnLevelWasLoaded()
    {
        FindPlayer();
    }

    void OnDrawGizmos()
    {
        GizmoBounds();
        GizmoAsteroids();
    }

    void GizmoBounds()
    {
        Gizmos.color = _colorBounds;

        Vector3 topLeft = new Vector3(-_boundWidth, 0, _boundHeight);
        Vector3 topRight = new Vector3(_boundWidth, 0, _boundHeight);
        Vector3 botLeft = new Vector3(-_boundWidth, 0, -_boundHeight);
        Vector3 botRight = new Vector3(_boundWidth, 0, -_boundHeight);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
    }

    void GizmoAsteroids()
    {
        Gizmos.color = _colorAsteroid;

        Vector3 topLeftOffset = new Vector3(-_boundWidth, 0, _boundHeight + _boundOffset);
        Vector3 topRightOffset = new Vector3(_boundWidth, 0, _boundHeight + _boundOffset);
        Vector3 botLeftOffset = new Vector3(-_boundWidth, 0, _boundHeight + _boundOffset / 2);
        Vector3 botRightOffset = new Vector3(_boundWidth, 0, _boundHeight + _boundOffset / 2);

        Gizmos.DrawLine(topLeftOffset, topRightOffset);
        Gizmos.DrawLine(topRightOffset, botRightOffset);
        Gizmos.DrawLine(botRightOffset, botLeftOffset);
        Gizmos.DrawLine(botLeftOffset, topLeftOffset);
    }

    public BoundManager GetBoundManager() { return _boundManager; }
}