using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    Button,
    Correct,
    InCorrect,
    GameOver
}

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [System.Serializable]
    public class SFXData
    {
        public SFXType type;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    [Header("Effect Sounds")]
    [SerializeField] private List<SFXData> sfxList;

    private Dictionary<SFXType, SFXData> sfxDict;
    private AudioSource audioSource;

    private void Awake()
    {
        // ΩÃ±€≈Ê
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Dictionary ±∏º∫
        sfxDict = new Dictionary<SFXType, SFXData>();
        foreach (var sfx in sfxList)
        {
            if (!sfxDict.ContainsKey(sfx.type))
                sfxDict.Add(sfx.type, sfx);
        }
    }

    public void Play(SFXType type)
    {
        if (!sfxDict.ContainsKey(type))
        {
            Debug.LogWarning($"SFX not found: {type}");
            return;
        }

        var sfx = sfxDict[type];
        audioSource.PlayOneShot(sfx.clip, sfx.volume);
    }
}
