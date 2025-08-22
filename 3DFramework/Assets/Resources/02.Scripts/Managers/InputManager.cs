using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager //매니저스가 있어서 굳이 컴포넌트로 만들 필요가 없어서 일반적인 C# 파일로 만듦
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //UI를 눌렀을 때 마우스 클릭 액션이 발행되지 않도록
        {
            return;
        }
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //프레스일 경우
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else //클릭한 경우(한번이라도 프레스를 했으면 click
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);

                _pressed = false;
            }
        }
    }
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
