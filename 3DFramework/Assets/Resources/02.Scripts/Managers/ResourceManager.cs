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

        //object�� ������ ������ ����Ϸ��� �ҰŶ�
        //�ڷḦ �� ã�ƺ���
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
