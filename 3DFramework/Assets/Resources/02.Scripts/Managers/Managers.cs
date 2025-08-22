using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;    
    ResourceManager _resource = new ResourceManager();
    InputManager _input = new InputManager();
    UIManager _ui = new UIManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();

    public static Managers instance { get { Init(); return s_instance; }  }

    public static InputManager input { get { return instance._input;  } }
    public static ResourceManager Resource { get { return instance._resource; } }
    public static UIManager UI { get { return instance._ui; } }
    public static SceneManagerEx Scnen { get { return instance._scene; } }
    public static SoundManager Sound { get { return instance._sound; } }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        _input.OnUpdate(); //인풋매니저의 OnUpdate를 실행, Invoke로 액션 실행
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" }; //코드상으로 오브젝트 만들기
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
        }
    }
}
