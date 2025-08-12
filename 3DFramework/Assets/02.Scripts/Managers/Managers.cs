using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;    
    ResourceManager _resource = new ResourceManager();
    InputManager _input = new InputManager();

    public static Managers instance { get { Init(); return s_instance; }  }

    public static InputManager input { get { return instance._input;  } }
    public static ResourceManager Resource { get { return instance._resource; } }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        _input.OnUpdate(); //��ǲ�Ŵ����� OnUpdate�� ����, Invoke�� �׼� ����
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" }; //�ڵ������ ������Ʈ �����
                go.AddComponent<Managers>();

            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
