using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musics;
    [SerializeField] private AudioSource _musicSource;
    private int currentTrack = 0;

    public void SwitchMusic()
    {
        if (!_musicSource.isPlaying)
            PlayMusic();
        else StopMusic();
    }

    public void NextMusic()
    {
        if (_musicSource.isPlaying)
        {
            if (currentTrack == _musics.Length - 1)
            {
                currentTrack = 0;
            }
            else currentTrack++;

            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        _musicSource.clip = _musics[currentTrack];
        _musicSource.Play();
    }

    private void StopMusic()
    {
        _musicSource.Stop();
    }
}
