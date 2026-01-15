using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] Sprite music_Noraml;
    [SerializeField] Sprite music_Pressed;

    [SerializeField] Sprite music_None_Noraml;
    [SerializeField] Sprite music_None_Pressed;


    public void ButtonClickSound()
    {
        SoundEffectManager.Instance.Play(SFXType.Button);
    }

    public void MusicOnOff(Button button)
    {
        bool isMuted = AudioListener.volume == 0f;

        AudioListener.volume = isMuted ? 1f : 0f;

        if (!isMuted)
        {
            button.image.sprite = music_None_Noraml;
            SetSpriteState(button, false);
        }
        else
        {
            button.image.sprite = music_Noraml;
            SetSpriteState(button, true);
        }
    }

    void SetSpriteState(Button button, bool isOn)
    {
        SpriteState state = new SpriteState
        {
            highlightedSprite = isOn ? music_Noraml : music_None_Noraml,
            pressedSprite = isOn ? music_Pressed : music_None_Pressed,
            selectedSprite = isOn ? music_Noraml : music_None_Noraml,
            disabledSprite = isOn ? music_Noraml : music_None_Noraml
        };

        button.spriteState = state;
    }
}
