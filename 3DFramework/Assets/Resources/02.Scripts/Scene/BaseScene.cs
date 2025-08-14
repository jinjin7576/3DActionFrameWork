using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    
    void Start()
    {
        
    }

    protected virtual void Init()
    {
        
    }
    public abstract void Clear();
   
}
