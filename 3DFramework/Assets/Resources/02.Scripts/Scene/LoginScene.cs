using UnityEngine;

public class LoginScene : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        _sceneType = Define.Scene.Login;
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scnen.LoadScene(Define.Scene.Game);
            //SceneManager.LoadScene("Game");
        }
    }
}
