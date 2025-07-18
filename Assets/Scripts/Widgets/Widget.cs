using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Widgets
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Widget : MonoBehaviour
    {
        public float inOutAnimationTime = 0.1f; 
            
        protected bool _isVisible = true;
        private CanvasGroup _canvas;
        private Coroutine _hiding;

        protected virtual void Start()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void ShowFor(float time)
        {
            Show();
            
            if (_hiding is not null)  StopCoroutine(_hiding);
            _hiding = StartCoroutine(WaitToHide(time));
        }

        private IEnumerator WaitToHide(float time)
        {
            yield return new WaitForSeconds(time);
            Hide();
        }

        public void Show()
        {
            if (_isVisible) return;
            
            _isVisible = true;
            gameObject.SetActive(true);
            InAnimation();
        }
        
        public void Hide()
        {
            if (!_isVisible) return;
            
            OutAnimation();
        }

        //use at end of out anim
        protected void Deactivate()         
        {
            gameObject.SetActive(false);
            _isVisible = false;
        }

        protected virtual void InAnimation()
        {
            LeanTween.value(gameObject, (float v) => _canvas.alpha = v, 0f, 1f, inOutAnimationTime);
        }
        
        protected virtual void OutAnimation()
        {
            LeanTween.value(gameObject, (float v) => _canvas.alpha = v, 1f, 0f, inOutAnimationTime).setOnComplete(Deactivate);
        }
    }
}