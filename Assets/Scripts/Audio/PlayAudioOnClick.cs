using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnClick : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        button.onClick.AddListener(PlayAudio);
    }

    private void PlayAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
