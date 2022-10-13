using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : Interactive 
{
    [SerializeField] int _value;

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        GameManager.Instance.AddCredits(_value);
        OnInteraction();
    }

    void OnEnable()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Credits s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(Credits s)
    {
        s.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.creditsFactory.ReturnCredits(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}