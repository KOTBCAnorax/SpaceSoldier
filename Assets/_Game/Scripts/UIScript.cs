using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Text _sensitivityText;
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private CanvasGroup _options;
    [SerializeField] private AnimationAndMovementController _player;

    private bool _optionsHidden = true;

    public void UpdateSensitivityValue()
    {
        _sensitivityText.text = _sensitivitySlider.value.ToString();
        _player.SetCameraRotationSpeed(_sensitivitySlider.value);
    }

    public void HideShowOptions()
    {
        if (_optionsHidden)
        {
            ShowOptions();
            _optionsHidden = false;
        }
        else
        {
            HideOptions();
            _optionsHidden = true;
        }
    }

    private void HideOptions()
    {
        _options.alpha = 0;
        _options.interactable = false;
    }

    private void ShowOptions()
    {
        _options.alpha = 1;
        _options.interactable = true;
    }

    private void Awake()
    {
        UpdateSensitivityValue();
    }
}
