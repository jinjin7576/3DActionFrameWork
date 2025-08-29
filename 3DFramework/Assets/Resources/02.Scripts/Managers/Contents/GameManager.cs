using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager
{
    GameObject _player;

    HashSet<GameObject> _monster = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent; //int를 전달하는 액션

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instatiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monster.Add(go);
                if (OnSpawnEvent != null)
                {
                    OnSpawnEvent.Invoke(1);
                }
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }
        return go;
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
        {
            return Define.WorldObject.Unknown;
        }
        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);
        switch (type)
        {
            case Define.WorldObject.Monster:
                if (_monster.Contains(go))
                {
                    _monster.Remove(go);
                    OnSpawnEvent?.Invoke(-1);
                }
                break;
            case Define.WorldObject.Player:
                if (_player == go) _player = null;
                break;
        }
        Managers.Resource.Destroy(go);
    }
    public GameObject GetPlayer() { return _player; }
}
