using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    static GameManager _instance;
    bool _isPaused;
    public Player playerReference;

    //[Header("Bounds")]
    public float _boundWidth;
    public float _boundHeight;
    public Color _color;

    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Puase()
    {
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void UnPuase()
    {
        _isPaused = false;
        Time.timeScale = 1f;
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
    void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Vector3 topLeft = new Vector3(-_boundWidth, 0, _boundHeight);
        Vector3 topRight = new Vector3(_boundWidth, 0, _boundHeight);
        Vector3 botLeft = new Vector3(-_boundWidth, 0, -_boundHeight);
        Vector3 botRight = new Vector3(_boundWidth, 0, -_boundHeight);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
    }
}