using System;
using Events;
using UnityEngine;

namespace Audio {
    public class SfxManager : MonoBehaviour {
        
        public AK.Wwise.Event ButtonClickEv;
        public AK.Wwise.Event ButtonHoveredEv;
        public AK.Wwise.Event SpeechMomEv;
        public AK.Wwise.Event SpeechKidEv;
        public AK.Wwise.Event CandyCollectedEv;
        
        private void Start() {
            EventManager.AddListener<ButtonClickedEvent>(HandleButtonClickedEvent); 
            EventManager.AddListener<ButtonHoveredEvent>(HandleButtonHoveredEvent);
            EventManager.AddListener<KidSaidEvent>(HandleKidSaidEvent);
            EventManager.AddListener<MomSaidEvent>(HandleMomSaidEvent);
            EventManager.AddListener<CandyCollectedEvent>(HandleCollectedEvent);
        }

        private void OnDestroy() {
            EventManager.RemoveListener<ButtonClickedEvent>(HandleButtonClickedEvent); 
            EventManager.RemoveListener<ButtonHoveredEvent>(HandleButtonHoveredEvent);
            EventManager.RemoveListener<KidSaidEvent>(HandleKidSaidEvent);
            EventManager.RemoveListener<MomSaidEvent>(HandleMomSaidEvent);
            EventManager.RemoveListener<CandyCollectedEvent>(HandleCollectedEvent);
        }

        private void HandleButtonClickedEvent(ButtonClickedEvent gameEvent) {
            ButtonClickEv.Post(gameObject);
        }

        private void HandleButtonHoveredEvent(ButtonHoveredEvent gameEvent) {
            ButtonHoveredEv.Post(gameObject);
        }

        private void HandleKidSaidEvent(KidSaidEvent gameEvent) {
            SpeechKidEv.Post(gameObject);
        }

        private void HandleMomSaidEvent(MomSaidEvent gameEvent) {
            SpeechMomEv.Post(gameObject);
        }

        private void HandleCollectedEvent(CandyCollectedEvent gameEvent) {
            CandyCollectedEv.Post(gameObject);
        }
    }
}