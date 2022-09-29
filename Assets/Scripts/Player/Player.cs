using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Controller _controller;
    [SerializeField] float _speed;
    Animator _myAnimator;

    void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckBounds();
        _myAnimator.SetFloat("Horizontal", _controller.GetMovementInput().x);
        _myAnimator.SetFloat("Vertical", _controller.GetMovementInput().y);
        transform.position += _controller.GetMovementInput() * _speed * Time.deltaTime;
    }

    public void CheckBounds()
    {
        this.transform.position = GameManager.Instance.ApplyBounds(this.transform.position);
    }
}