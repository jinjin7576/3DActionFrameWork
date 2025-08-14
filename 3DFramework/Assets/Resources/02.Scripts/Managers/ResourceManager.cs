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
        GameObject prefab = Load<GameObject>($"03.Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)"); // UI_Inven_Item(Clone) -> �����̸� 
        if (index > 0)
        {
            go.name = go.name.Substring(0, index); //UI_Inven_Item -> ������ �̸� / (Clone) -> �̾Ƴ� �̸�
        }

        return go;
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
