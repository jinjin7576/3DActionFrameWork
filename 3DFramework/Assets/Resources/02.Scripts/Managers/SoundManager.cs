using UnityEngine;
using System.Collections.Generic;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClip = new Dictionary<string, AudioClip>();

    public void Play(string path ,Define.Sound type = Define.Sound.Effect , float pitch = 1.0f) //선택적 매개변수와 필수 매개변수의 순서?
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

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
        }
        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        for(int i = 0; i < soundNames.Length-1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }
        _audioSources[(int)Define.Sound.Bgm].loop = true;
    }
    
    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        //만약 기존에 패스가 없다면 가져와서 딕셔너리에 추가
        if (_audioClip.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClip.Add(path, audioClip);
        }
        //없었으면 가져와서 있는 것을 리턴하고, 있었으면 있던 것을 리턴
        return audioClip;
    }
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClip.Clear();
    }
}
