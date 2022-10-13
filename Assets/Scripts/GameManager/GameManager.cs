using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    static GameManager _instance;

    [Header("Player")]
    [SerializeField] int _credits;
    public Player playerReference { get { return _player; } }
    public Player _player;
    
    [Header("Managers")]
    [SerializeField] SaveJSON _jsonManager;
    LevelManager _levelManager;
    PauseManager _pauseManager;
    BoundManager _boundManager;
    AsteroidManager _asteroidManager;
    EnemyManager _enemyManager;
    OptionsManager _optionsManager;
    AudioManager _audioManager;

    [Header("Audio")]
    public AudioClip[] _audioClips;
    [SerializeField] AudioMixer _audioMixer;
    AudioSource _myAudioSource;

    [Header("Bounds")]
    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;
    [SerializeField] Color _colorBounds;
    [SerializeField] float _boundOffset;
    [SerializeField] Color _colorSpawner;

    [Header("Asteroids")]
    [SerializeField] float _spawnTimeAsteroid;

    [Header("Enemy")]
    [SerializeField] float _spawnTimeEnemy;
    [SerializeField] int _maxEnemies;
    [SerializeField] int _countDeadEnemies;

    [Header("Options")]
    [SerializeField] Image _soundOnIcon;
    [SerializeField] Image _soundOffIcon;

    [Header("Factories")]
    public BulletFactory bulletFactory;
    public AsteroidFactory asteroidFactory;
    public CreditsFactory creditsFactory;
    public EnemyBulletFactory enemyBulletFactory;
    public EnemyFactory enemyFactory;
    public FireBurstFactory fireBurstFactory;
    public FireRateFactory fireRateFactory;
    public ShieldFactory shieldFactory;
    public SinuousBulletFactory sinuousBulletFactory;
    public RandomBulletFactory randomBulletFactory;

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

        //Managers
        _levelManager = new LevelManager();
        _pauseManager = new PauseManager();
        _boundManager = new BoundManager(_boundWidth, _boundHeight);
        _asteroidManager = new AsteroidManager(_spawnTimeAsteroid, _boundWidth, _boundHeight, _boundOffset);
        _enemyManager = new EnemyManager(_spawnTimeEnemy, _boundWidth, _boundHeight, _boundOffset);
        _optionsManager = new OptionsManager();
        _myAudioSource = GetComponent<AudioSource>();
        _audioManager = new AudioManager(_myAudioSource, _audioClips);

        EventManager.SubscribeToEvent(Contants.EVENT_LOSEGAME,DefeatScene);

        //Credits
        _jsonManager.LoadGame();
        _credits = _jsonManager._data.credits;


    }

    void Start()
    {
        if(!PlayerPrefs.HasKey("_onMuted"))
        {
            PlayerPrefs.SetInt("_onMuted", 0);
            _optionsManager.Load();
        }
        else
            _optionsManager.Load();

        _optionsManager.UpdateButtonIncon(_soundOnIcon, _soundOffIcon);
        AudioListener.pause = _optionsManager._onMuted;
    }

    void Update()
    {
        if(_player)
        {
            _asteroidManager.ArtificialUpdate();

            if(_enemyManager.GetCounter() <= _maxEnemies)
                _enemyManager.ArtificialUpdate();
            else if (_countDeadEnemies >= _enemyManager.GetCounter())
                ChangeScene("Victory");
        }
    }

    public Vector3 ApplyBounds(Vector3 objectPosition) { return _boundManager.ApplyBounds(objectPosition); }
    void FindPlayer() { _player = FindObjectOfType<Player>(); }
    
    public void ChangeScene(string sceneToLoad) { _levelManager.ChangeScene(sceneToLoad); ResetEnemyCounters();}
    public void ResetEnemyCounters() { _countDeadEnemies = 0; _enemyManager.SetCounter(1); }
    public void ChangeMusic(AudioClip clip) { _audioManager.ChangeMusic(clip); }
    public void SetVolume(float volume) { _optionsManager.SetVolume(volume); }
    public void Pause() { _pauseManager.Pause(); }
    public void UnPause() { _pauseManager.UnPause(); }
    public void QuitGame() { _levelManager.QuitGame(); }
    public void SaveGame() { _jsonManager.SaveGame(); }
    public void LoadGame() { _jsonManager.LoadGame(); }
    public void Mute() { _optionsManager.Mute(_soundOnIcon, _soundOffIcon); }
    public void SetSoundIcons(Image soundOn, Image soundOff) { _soundOnIcon = soundOn; _soundOffIcon = soundOff; }
    public void SetCredits(int value) { _credits = value; }
    public void AddCredits(int value) { _credits += value; _jsonManager._data.credits = _credits; ; EventManager.TriggerEvent(Contants.EVENT_ADDCREDITUI, _credits); }
    public int GetCredits() { return _credits; }

    public void DefeatScene(params object[] param)
    {
        string _levelToChange = (string)param[0];
        _levelManager.ChangeScene(_levelToChange);
    }

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
        Gizmos.color = _colorSpawner;

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
    public SaveJSON GetJSONManager() { return _jsonManager; }
    public void SetCountDeadEnemies(int value) { _countDeadEnemies += value; }
}