using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void ButtonClickSound()
    {
        SoundEffectManager.Instance.Play(SFXType.Button);
    }
}
