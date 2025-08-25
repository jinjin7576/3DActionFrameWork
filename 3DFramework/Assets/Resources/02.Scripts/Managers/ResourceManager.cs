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
        //1. ���������� ������ �ٷ� ���, ������ �Ʒ�ó��
        GameObject prefab = Load<GameObject>($"03.Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. Ȥ�� Ǯ���� ������Ʈ�� �ִٸ� �װ��� ��ȯ
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
        {
            go.name = go.name.Substring(0, index);
        }

        return go;
    }
    //3. ���� Ǯ���� �ʿ��� ������Ʈ��� �ٷ� ����x Ǯ���Ŵ������� ��Ź
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        Object.Destroy(go);
    }
}
