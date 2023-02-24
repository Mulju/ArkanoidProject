using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundControl : MonoBehaviour
{
    public bool inMenu;

    public AudioClip menuMove;
    public AudioClip menuSelect;

    public void PlayMoveSound()
    {
        float volume = 0.2f;
        GameManager.manager.PlaySound(menuMove, volume);
    }

    public void PlaySelectSound()
    {
        float volume = 0.2f;
        GameManager.manager.PlaySound(menuSelect, volume);
    }
}
