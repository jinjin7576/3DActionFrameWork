using UnityEngine;
using System;
using System.Collections;

public class CursorController : MonoBehaviour
{
    Texture2D _attackIcon;
    Texture2D _basicIcon;

    CursorType _cursorType = CursorType.None;

    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    public enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    private void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("06.Res/Attack");//공격일때의 커서
        _basicIcon = Managers.Resource.Load<Texture2D>("06.Res/Basic");//공격일때의 커서
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_basicIcon, new Vector2(_attackIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
