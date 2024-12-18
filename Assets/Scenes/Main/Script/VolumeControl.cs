using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;       // 볼륨을 조절할 Slider
    public AudioSource audioSource;  // 연결할 AudioSource (예: 배경 음악)

    void Start()
    {
        // 슬라이더의 초기값을 AudioSource의 볼륨에 맞춤
        volumeSlider.value = audioSource.volume;

        // 슬라이더 값이 변경될 때마다 볼륨 조정
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        audioSource.volume = volume;  // AudioSource의 볼륨 조정
    }
}
