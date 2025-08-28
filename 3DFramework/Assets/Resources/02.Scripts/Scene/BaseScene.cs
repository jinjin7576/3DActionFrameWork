using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene _sceneType { get; protected set; } = Define.Scene.Unknown;

    protected CameraController _camera;

    void Start()
    {

    }

    protected virtual void Init()
    {
        Object obj = FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Managers.Resource.Instatiate("UI/EventSystem").name = "@EventSystem";
        }
        _camera = Camera.main.gameObject.GetorAddComponent<CameraController>();
    }
    public abstract void Clear();
   
}
