using UnityEngine;

public class Util
{
    //최상위 부모, 이름은 비교하지 않고 그 타입에만 해당하면 리턴
    // 재귀적으로 사용하여 자식만 찾을건지, 자식의 자식도 찾을 것인지
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false) //직속 자식만 찾기
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                } 
            }
        }
    }
}
