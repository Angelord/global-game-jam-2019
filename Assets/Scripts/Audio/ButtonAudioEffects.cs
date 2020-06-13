using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Audio {
    
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(EventTrigger))]
    public class ButtonAudioEffects : MonoBehaviour {
        
        private void Start() {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
            
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry hoverTrigger = new EventTrigger.Entry();
            hoverTrigger.eventID = EventTriggerType.PointerEnter;
            hoverTrigger.callback.AddListener(OnHover);
            trigger.triggers.Add(hoverTrigger);
        }

        private void OnClick() {
            EventManager.TriggerEvent(new ButtonClickedEvent());
        }

        private void OnHover(BaseEventData eventData) {
            EventManager.TriggerEvent(new ButtonHoveredEvent());
        }
    }
}