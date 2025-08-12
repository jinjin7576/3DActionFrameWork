using UnityEngine;
using System;

public class InputManager //�Ŵ������� �־ ���� ������Ʈ�� ���� �ʿ䰡 ��� �Ϲ����� C# ���Ϸ� ����
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    
    public void OnUpdate()
    {
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //�������� ���
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else //Ŭ���� ���(�ѹ��̶� �������� ������ click
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);

                _pressed = false;
            }
        }
    }
}
