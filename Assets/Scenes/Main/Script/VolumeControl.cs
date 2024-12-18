using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;       // ������ ������ Slider
    public AudioSource audioSource;  // ������ AudioSource (��: ��� ����)

    void Start()
    {
        // �����̴��� �ʱⰪ�� AudioSource�� ������ ����
        volumeSlider.value = audioSource.volume;

        // �����̴� ���� ����� ������ ���� ����
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        audioSource.volume = volume;  // AudioSource�� ���� ����
    }
}
