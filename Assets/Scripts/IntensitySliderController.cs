
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntensitySliderController : MonoBehaviour
{
    public Slider intensitySlider;
    public TMP_Text intensityLabel;

    public int Intensity
    {
        get
        {
            if (intensitySlider == null)
                return 100;

            return Mathf.RoundToInt(intensitySlider.value / 100f) * 100;
        }
    }

    private void Awake()
    {
        if (intensitySlider == null)
            intensitySlider = GetComponent<Slider>();

        if (intensitySlider == null)
        {
            Debug.LogError("Intensity Slider is not assigned!");
            enabled = false;
            return;
        }

        intensitySlider.minValue = 100;
        intensitySlider.maxValue = 1000;
        intensitySlider.wholeNumbers = true;
        intensitySlider.interactable = true;

        intensitySlider.onValueChanged.AddListener(OnSliderChanged);

        UpdateLabel(intensitySlider.value);
    }

    private void OnSliderChanged(float value)
    {
        float snappedValue = Mathf.Clamp(
            Mathf.Round(value / 100f) * 100f,
            100f,
            1000f
        );

        if (!Mathf.Approximately(value, snappedValue))
        {
            intensitySlider.SetValueWithoutNotify(snappedValue);
        }

        UpdateLabel(snappedValue);

        Debug.Log("Projectile Intensity: " + (int)snappedValue);
    }

    private void UpdateLabel(float value)
    {
        if (intensityLabel != null)
        {
            intensityLabel.text =
                "Projectile Intensity: " + Mathf.RoundToInt(value);
        }
    }

    private void OnDestroy()
    {
        if (intensitySlider != null)
        {
            intensitySlider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }
}
