using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class Extension 
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent eventType = Define.UIEvent.Click)
    {
        UI_Base.AddUIEvent(go, action, eventType);
    }
}
