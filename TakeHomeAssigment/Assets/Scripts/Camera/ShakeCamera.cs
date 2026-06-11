using Unity.Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance;

    [SerializeField] CinemachineCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin channelPerlin;
    float moveTime;
    float moveTimeTotal;
    float initialIntensity;

    private void Awake()
    {
        Instance = this;
    }

    public void CameraMove(float intensity, float frequency, float time)
    {
        channelPerlin = cinemachineVirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        channelPerlin.AmplitudeGain = intensity;
        channelPerlin.FrequencyGain = frequency;
        initialIntensity = intensity;
        moveTimeTotal = time;
        moveTime = time;
    }

    private void Update()
    {
        if (moveTime > 0f)
        {
            moveTime -= Time.deltaTime;
            channelPerlin.AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (moveTime / moveTimeTotal));
        }
    }
}
