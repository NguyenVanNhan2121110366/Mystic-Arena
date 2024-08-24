using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<AudioManager>();
            }
            return instance;
        }
    }
    #region Variable
    public AudioSource audioSrc;
    [SerializeField] private AudioClip soundDestroy;
    [SerializeField] private AudioClip soundContinuousDestruction;
    [SerializeField] private AudioClip soundDestroyFalse;
    [SerializeField] private AudioClip soundClick;
    [SerializeField] private AudioClip soundBuyFalse;
    [SerializeField] private AudioClip soundBuyItem;
    [SerializeField] private AudioClip soundAttack;
    [SerializeField] private AudioClip soundStab;
    #endregion
    #region Public
    public AudioClip SoundDestroy { get => soundDestroy; set => soundDestroy = value; }
    public AudioClip SoundContinuousDestruction { get => soundContinuousDestruction; set => soundContinuousDestruction = value; }
    public AudioClip SoundDestroyFalse { get => soundDestroyFalse; set => soundDestroyFalse = value; }

    public AudioClip SoundClick { get => soundClick; set => soundClick = value; }
    public AudioClip SoundBuyFalse { get => soundBuyFalse; set => soundBuyFalse = value; }
    public AudioClip SoundBuyItem { get => soundBuyItem; set => soundBuyItem = value; }
    public AudioClip SoundAttack { get => soundAttack; set => soundAttack = value; }
    public AudioClip SoundStab { get => soundStab; set => soundStab = value; }
    #endregion

    
    void Start()
    {
        this.audioSrc = GetComponent<AudioSource>();
    }

}
