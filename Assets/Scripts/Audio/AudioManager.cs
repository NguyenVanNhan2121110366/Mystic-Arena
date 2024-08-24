using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variable
    public AudioSource audioSrc;
    [SerializeField] private AudioClip soundDestroy;
    [SerializeField] private AudioClip soundContinuousDestruction;
    [SerializeField] private AudioClip soundDestroyFalse;
    #endregion
    #region Public
    public AudioClip SoundDestroy { get => soundDestroy; set => soundDestroy = value; }
    public AudioClip SoundContinuousDestruction { get => soundContinuousDestruction; set => soundContinuousDestruction = value; }
    public AudioClip SoundDestroyFalse { get => soundDestroyFalse; set => soundDestroyFalse = value; }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.audioSrc = GetComponent<AudioSource>();
    }

}
