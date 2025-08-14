using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class Extension 
{
    public static void BindUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click)
    {
        UI_Base.BindUIEvent(go, action, eventType);
    }
    public static T GetorAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetorAddComponent<T>(go);
    }
}
