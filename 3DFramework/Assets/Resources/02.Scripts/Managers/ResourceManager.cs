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
        //1. 오리지날이 있으면 바로 사용, 없으면 아래처럼
        GameObject prefab = Load<GameObject>($"03.Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. 혹시 풀링된 오브젝트가 있다면 그것을 반환
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
        {
            go.name = go.name.Substring(0, index);
        }

        return go;
    }
    //3. 만약 풀링이 필요한 오브젝트라면 바로 삭제x 풀링매니저한테 위탁
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        Object.Destroy(go);
    }
}
