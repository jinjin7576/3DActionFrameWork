using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager //�Ŵ������� �־ ���� ������Ʈ�� ���� �ʿ䰡 ��� �Ϲ����� C# ���Ϸ� ����
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
                if (!_pressed) //���� ������ �� �� ����
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
                    if (Time.time < _pressedTime + 0.2f) //���� �ð��� Ŭ���ߴٰ� �ν��� ����
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    //Ŭ���̵� �������� ���콺�� ���� �ൿ�� ������ �� �� �ۿ� ����
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
