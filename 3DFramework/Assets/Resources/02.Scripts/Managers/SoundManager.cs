using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    public void Play(Define.Sound type, string path, float pitch = 1.0f)
    {
        if (path.Contains("05.Sound/") == false)
        {
            path = $"05.Sound/{path}";
        }
        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! {path}");
                return;
            }
        }
        else
        {

        }
    }
}
