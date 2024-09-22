using UnityEngine;
using UnityEngine.UI;

public class VisualizerOfInformationAboutWaves : MonoBehaviour
{
    [SerializeField] private SceneManager SceneManager;
    [SerializeField] private Text TextWithInformation;
    [SerializeField] private string Prefix;

    private void Awake()
    {
        SceneManager.OnChangingWave += VisualizeWave;
    }

    private void OnDestroy()
    {
        SceneManager.OnChangingWave -= VisualizeWave;
    }

    private void VisualizeWave(int newWaveIndex)
    {
        TextWithInformation.text = $"{Prefix}{newWaveIndex}/{SceneManager.WavesAmount}";
    }
}