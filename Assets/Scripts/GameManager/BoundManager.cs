using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoundManager
{
    float _boundWidth;
    float _boundHeight;

    public BoundManager(float boundWidth, float boundHeight)
    {
        _boundWidth = boundWidth;
        _boundHeight = boundHeight;
    }

    public Vector3 ApplyBounds(Vector3 objectPosition)
    {
        if (objectPosition.x > _boundWidth)
            objectPosition.x = _boundWidth;
        else if (objectPosition.x < -_boundWidth)
            objectPosition.x = -_boundWidth;

        if (objectPosition.z > _boundHeight)
            objectPosition.z = _boundHeight;
        else if (objectPosition.z < -_boundHeight)
            objectPosition.z = -_boundHeight;

        return objectPosition;
    }

    public void CheckBounds(Player player)
    {
        player.transform.position = GameManager.Instance.ApplyBounds(player.transform.position);
    }
}