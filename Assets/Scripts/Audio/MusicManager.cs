using UnityEngine;

namespace Audio {
    public class MusicManager : MonoBehaviour {

        private static bool musicInitialized = false;

        public AK.Wwise.Event MusicStartEvent;
        public AK.Wwise.State PauseState;
        public AK.Wwise.State PlayState;
        public AK.Wwise.State ExploreState;
        public AK.Wwise.State BattleState;
        public AK.Wwise.State ShelterState;
        
        private void Start() {
            if (!musicInitialized) {
                MusicStartEvent.Post(gameObject);
                musicInitialized = true;
            }

            EventManager.AddListener<GamePausedEvent>(HandleGamePausedEvent);
            EventManager.AddListener<GameResumedEvent>(HandleGameResumedEvent);
            EventManager.AddListener<StageStartedEvent>(HandleStageStartedEvent);
            EventManager.AddListener<StageLostEvent>(HandleStageLostEvent);
            EventManager.AddListener<WaveStartedEvent>(HandleWaveStartedEvent);
            EventManager.AddListener<WaveOverEvent>(HandleWaveOverEvent);
            EventManager.AddListener<ShopEnteredEvent>(HandleShopEnteredEvent);
            EventManager.AddListener<ShopExitedEvent>(HandleShopExitedEvent);
        }

        private void OnDestroy() {
            EventManager.RemoveListener<GamePausedEvent>(HandleGamePausedEvent);
            EventManager.RemoveListener<GameResumedEvent>(HandleGameResumedEvent);
            EventManager.RemoveListener<StageStartedEvent>(HandleStageStartedEvent);
            EventManager.RemoveListener<StageLostEvent>(HandleStageLostEvent);
            EventManager.RemoveListener<WaveStartedEvent>(HandleWaveStartedEvent);
            EventManager.RemoveListener<WaveOverEvent>(HandleWaveOverEvent);
            EventManager.RemoveListener<ShopEnteredEvent>(HandleShopEnteredEvent);
            EventManager.RemoveListener<ShopExitedEvent>(HandleShopExitedEvent);
        }

        private void HandleGamePausedEvent(GamePausedEvent gameEvent) {
            PauseState.SetValue();
        }

        private void HandleStageStartedEvent(StageStartedEvent gameEvent) {
            ExploreState.SetValue();
            PlayState.SetValue();
        }

        private void HandleStageLostEvent(StageLostEvent gameEvent) {
            PauseState.SetValue();
        }

        private void HandleGameResumedEvent(GameResumedEvent gameEvent) {
            PlayState.SetValue();
        }

        private void HandleWaveStartedEvent(WaveStartedEvent gameEvent) {
            BattleState.SetValue();
        }
        
        private void HandleWaveOverEvent(WaveOverEvent gameEvent) {
            ExploreState.SetValue();
        }

        private void HandleShopEnteredEvent(ShopEnteredEvent gameEvent) {
            ShelterState.SetValue();
        }
        
        private void HandleShopExitedEvent(ShopExitedEvent gameEvent) {
            ExploreState.SetValue();
        }
    }
}
