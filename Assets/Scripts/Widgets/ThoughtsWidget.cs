using System;
using DefaultNamespace.Widgets;
using TMPro;
using UnityEngine;

public class ThoughtsWidget : Widget
{
    public RectTransform panelTransform;
    public TextMeshProUGUI thoughtsText;

    protected override void Start()
    {
        base.Start();
        Hide();
    }

    public void ShowTextFor(string text, float duration)
    {
        thoughtsText.text = text;
        ShowFor(duration);
    }
    
    protected override void InAnimation()
    {
        base.InAnimation();
        
        LeanTween.moveY(panelTransform, 75f, inOutAnimationTime);
    }

    protected override void OutAnimation()
    {
        base.OutAnimation();
        LeanTween.moveY(panelTransform, -100f, inOutAnimationTime);
    }
}
