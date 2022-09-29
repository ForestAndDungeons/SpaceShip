using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float _scrollSpeed;
    float _yScroll;
    MeshRenderer _meshRenderer;
    Material _material;
    Vector2 _offset;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        _yScroll = Time.time * _scrollSpeed;
        _offset = new Vector2(0f, _yScroll);
        _material.mainTextureOffset = _offset;
    }
}
