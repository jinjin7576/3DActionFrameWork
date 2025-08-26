using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager //매니저스가 있어서 굳이 컴포넌트로 만들 필요가 없어서 일반적인 C# 파일로 만듦
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;
    
    public void OnUpdate()
    {
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) 
            {
                if (!_pressed) //아직 프레스 된 적 없음
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else 
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime + 0.2f) //누른 시간이 클릭했다고 인식할 정도
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    //클릭이든 프레스든 마우스를 떼는 행동이 무조건 들어갈 수 밖에 없음
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
