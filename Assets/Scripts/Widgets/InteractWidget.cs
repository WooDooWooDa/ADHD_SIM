using DefaultNamespace.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractWidget : Widget
{
    public TextMeshProUGUI interactText; 
    public Transform letsDoItTransform;
    public Transform progressTransform;
    public Slider progressSlider;

    public bool isInteracting
    {
        get => _isInteracting;
        set
        {
            _isInteracting = value;
            progressTransform.gameObject.SetActive(_isInteracting);
            letsDoItTransform.gameObject.SetActive(!_isInteracting);
        }
    }
    private bool _isInteracting;

    public void UpdateInteractText(string text)
    {
        interactText.text = text;
    }
    
    public void UpdateInteractProgress(float total, float progress)
    {
        progressSlider.value = progress / total;
    }
}
