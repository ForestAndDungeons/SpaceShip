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

    [SerializeField] int _levelAmount;
    [SerializeField] float _transitionTime;

    [Header("Player")]
    [SerializeField] int _credits;
    [SerializeField] bool _isGyro  = false;
    [SerializeField] bool _supportGyro;
    public Player playerReference { get { return _player; } }
    public Player _player;
    [SerializeField] Mesh _defaultMesh;
    [SerializeField] Material _defaultMaterial;
    public Mesh playerMesh;
    public Material playerMaterial;

    [SerializeField] float _notificationTime;

    [Header("Shop")]
    public int skinCheckID;
    public int powerUpCheckID;
    public List<int> boughtItemsID = new List<int>();
    public bool defaultBull;
    public bool isRandomBull;
    public bool isSinuousBull;

    [Header("Screen Manager")]
    public Transform mainCanvas;
    [SerializeField] Transform mainTransf;
    [SerializeField] Slider _sliderLoad;
    [SerializeField] GameObject _loadObj;
    LoadSceneManager _loadScreenManager;

    [Header("Managers")]
    SaveJSON _jsonManager;
    LevelManager _levelManager;
    PauseManager _pauseManager;
    BoundManager _boundManager;
    AsteroidManager _asteroidManager;
    EnemyManager _enemyManager;
    OptionsManager _optionsManager;
    AudioManager _audioManager;
    NotificationManager _notificationManager;
    ScreenManager _screenManager;

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
    [SerializeField] Enemy[] _prefabBoss;

    [Header("Factories")]
    [HideInInspector] public BulletFactory bulletFactory;
    [HideInInspector] public AsteroidFactory asteroidFactory;
    [HideInInspector] public CreditsFactory creditsFactory;
    [HideInInspector] public EnemyBulletFactory enemyBulletFactory;
    [HideInInspector] public EnemyFactory enemyFactory;
    [HideInInspector] public FireBurstFactory fireBurstFactory;
    [HideInInspector] public FireRateFactory fireRateFactory;
    [HideInInspector] public FireTimeFactory fireTimeFactory;
    [HideInInspector] public ShieldFactory shieldFactory;
    [HideInInspector] public HealFactory healFactory;
    [HideInInspector] public SinuousBulletFactory sinuousBulletFactory;
    [HideInInspector] public RandomBulletFactory randomBulletFactory;
    [HideInInspector] public AsteroidBigFactory asteroidBigFactory;
    [HideInInspector] public LevelUpBulletFactory levelUpBulletFactory;

    bool flag = false;

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
        StartCoroutine(GetLoadSlider());
        _myAudioSource = GetComponent<AudioSource>();

        //Managers
        _jsonManager = new SaveJSON();
        _levelManager = new LevelManager();
        _pauseManager = new PauseManager();
        _boundManager = new BoundManager(_boundWidth, _boundHeight);
        _asteroidManager = new AsteroidManager(_spawnTimeAsteroid, _boundWidth, _boundHeight, _boundOffset);
        _enemyManager = new EnemyManager(_spawnTimeEnemy, _boundWidth, _boundHeight, _boundOffset);
        _optionsManager = new OptionsManager();
        _audioManager = new AudioManager(_myAudioSource, _audioClips);
        _notificationManager = new NotificationManager(_notificationTime);
        _screenManager = new ScreenManager();

        EventManager.SubscribeToEvent(Contants.EVENT_LOSEGAME,DefeatScene);

        if (!PlayerPrefs.HasKey("_onMuted"))
        {
            PlayerPrefs.SetInt("_onMuted", 0);
            _optionsManager.Load();
        }
        else
            _optionsManager.Load();

        AudioListener.pause = _optionsManager._onMuted;

        if (playerMesh==null && playerMaterial==null)
        {
            playerMesh = _defaultMesh;
            playerMaterial = _defaultMaterial;
        }
        if (isRandomBull == false && isSinuousBull == false)
        {
            defaultBull = true;
        }

        if (!SystemInfo.supportsGyroscope && !Application.isEditor){ _supportGyro = false; }
        else { _supportGyro = true; }
    }

    void Update()
    {
        if(_player)
        {
            _asteroidManager.ArtificialUpdate();

            if(_enemyManager.GetCounter() < _maxEnemies)
                _enemyManager.ArtificialUpdate();
        }
    }

    public Vector3 ApplyBounds(Vector3 objectPosition) { return _boundManager.ApplyBounds(objectPosition); }
    void FindPlayer() { _player = FindObjectOfType<Player>(); }
    public void TransitionLevel() { _player.GetComponentInChildren<Animator>().SetTrigger("nextLevel"); }
    public IEnumerator GetLoadSlider() {
        yield return new WaitForSeconds(0.5f);
        _loadScreenManager = FindObjectOfType<LoadSceneManager>();
        if (_loadScreenManager!=null)
        {
            _loadObj = _loadScreenManager.loadObj;
            _sliderLoad = _loadScreenManager.sliderLoad;
            _loadObj.SetActive(false);
        }
    }
    public void ChangeScene(string sceneToLoad) { _levelManager.ChangeScene(sceneToLoad); ResetEnemyCounters(); StartCoroutine(GetLoadSlider()); }
    public void ChangeSceneAsync(string sceneToLoad) {_loadObj.SetActive(true); StartCoroutine(_levelManager.LoadAsyncScreen(sceneToLoad, _sliderLoad)); ResetEnemyCounters(); }
    public void NextLevel()
    {
        for (int i = 1; i < _levelAmount; i++)
        {
            if(SceneManager.GetActiveScene().name.EndsWith(_levelAmount.ToString()))
            {
                _levelManager.ChangeScene("Victory");
            }
            else if (SceneManager.GetActiveScene().name.EndsWith(i.ToString()))
            {
                SaveCredits();
                SaveGame();
                ResetEnemyCounters();
                ChangeScene($"Level{i+1}");
            }
        }
    }

    public void QuitGame() { _levelManager.QuitGame(); }

    public void ResetEnemyCounters() { _countDeadEnemies = 0; _enemyManager.SetCounter(0); }

    public void ChangeMusic(AudioClip clip) { _audioManager.ChangeMusic(clip); }

    public void Mute(Image soundOnIcon, Image soundOffIcon) { _optionsManager.Mute(soundOnIcon, soundOffIcon); }
    public void UpdateButtonIcon(Image soundOnIcon, Image soundOffIcon) { _optionsManager.UpdateButtonIcon(soundOnIcon, soundOffIcon); }

    public void Pause() { _pauseManager.Pause(); }
    public void UnPause() { _pauseManager.UnPause(); }
    

    public void SaveGame() { _jsonManager.SaveGame(); }
    public void DeleteSave() { _jsonManager.DeleteSave(); }
    public void LoadGame()
    {
        _jsonManager.LoadGame();
        foreach (var item in _jsonManager._data.boughtItemsID)
        {
            if (!boughtItemsID.Contains(item))
            {
                boughtItemsID.Add(item);
            }
        }
    }
    public void SetCredits(int value) { _credits = value; }
    public int GetCredits() { return _credits; }
    public void AddCredits(int value) { _credits += value; EventManager.TriggerEvent(Contants.EVENT_ADDCREDITUI, _credits); }
    public void SaveCredits() { _jsonManager._data.credits = _credits; }

    public int GetCountDeadEnemies() { return _countDeadEnemies; }
    public int GetMaxEnemies() { return _maxEnemies; }
    public Enemy GetPrefabBoss()
    {
        for (int i = 1; i <= _levelAmount; i++)
        {
            if (SceneManager.GetActiveScene().name.EndsWith(i.ToString()))
            {
                return _prefabBoss[i-1];
            }
        }
        return null;
    }
    public ScreenManager GetScreenManager() { return _screenManager; }
    public BoundManager GetBoundManager() { return _boundManager; }
    public EnemyManager GetEnemyManager() { return _enemyManager; }
    public AsteroidManager GetAsteroidManager() { return _asteroidManager; }
    public SaveJSON GetJSONManager() { return _jsonManager; }
    public bool GetIsGyro() { return _isGyro; }
    public void InvertIsGyro() { _isGyro = !_isGyro; }
    public bool GetSupportGyro() { return _supportGyro; }
    public void SetCountDeadEnemies(int value) { _countDeadEnemies += value; }
    public void ResetBoughtItems() { boughtItemsID = new List<int>(); }

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
        GizmoSpawner();
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

    void GizmoSpawner()
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
}