using UnityEngine;

namespace DefaultNamespace.Widgets
{
    public class Widget : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
            //In animation
        }
        
        public void Hide()
        {
            //Out animation
            gameObject.SetActive(false);
        }
    }
}