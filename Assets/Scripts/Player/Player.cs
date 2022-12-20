using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : PlayerBase
{
    void Awake()
    {
        GetComponentInChildren<MeshFilter>().mesh = GameManager.Instance.playerMesh;
        GetComponentInChildren<MeshRenderer>().material = GameManager.Instance.playerMaterial;
        _maxHealth = _data.maxHealth;
        _currentHealth = _data.maxHealth;
        _maxSpeed = _data.maxSpeed;
        _myAnimator = GetComponentInChildren<Animator>();
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponentInChildren<ParticleSystem>();
        _allObservers = new List<IObserver>();
        //_shootTimer = _shootTime;
        _canFire = true;
        _shooting = false;
        _isRandomBullet = GameManager.Instance.isRandomBull;
        _isSinuousBullet = GameManager.Instance.isSinuousBull;
    }

    private void Start()
    {
        EventManager.TriggerEvent(Contants.EVENT_INICIATEHEALTHBAR, _data.maxHealth, _currentHealth);
        EventManager.TriggerEvent(Contants.EVENT_INICIATECREDITS, GameManager.Instance.GetCredits());

        if (GameManager.Instance.GetIsGyro())
        {
            _controller.gameObject.SetActive(false);
            _joystickBase.gameObject.SetActive(false);
            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
        else if(!GameManager.Instance.GetIsGyro() && GameManager.Instance.GetSupportGyro()) {
            _controller.gameObject.SetActive(true);
            _joystickBase.gameObject.SetActive(true);
            _gyro = Input.gyro;
            _gyro.enabled = false; 
        }
       
    }

    void Update()
    {
        GameManager.Instance.GetBoundManager().CheckBounds(this);
        UpdateAnimatorVariables();
        if (!GameManager.Instance.GetIsGyro())
        {
            Movement(transform);
        }
        else
        {
            GyroMovement(transform);
        }

        if (_shooting)
            Shoot();
    }

    public void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }

    public void ChangeScene(string sceneToLoad) { GameManager.Instance.ChangeScene(sceneToLoad); }   
}