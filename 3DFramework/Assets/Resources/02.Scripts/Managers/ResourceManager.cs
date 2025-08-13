using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instatiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //object를 붙이지 않으면 재귀하려고 할거라서
        //자료를 더 찾아볼것
        return Object.Instantiate(prefab, parent);
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        Object.Destroy(go);
    }
}
