using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene _sceneType { get; protected set; } = Define.Scene.Unknown;

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
    }
    public abstract void Clear();
   
}
