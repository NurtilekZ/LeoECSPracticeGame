using Game.Runtime.Components;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime.Systems
{
    public class EndGameSystem : IEcsRunSystem
    {
        private EcsCustomInject<SceneService> _sceneService;
        private EcsFilterInject<Inc<CollisionEvt>> _collisionEvtFilter;
        private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;
        private EcsPoolInject<CollisionEvt> _collisionEvtPool;
        private EcsPoolInject<PlayerTag> _playerTagPool;
        
        public void Run(IEcsSystems systems)
        {
            if (_sceneService.Value.GameIsOver)
                return;
            
            CheckLoseCondition();
            CheckWindCondition();
        }

        private void CheckWindCondition()
        {
            if (Time.timeSinceLevelLoad <= 10)
                return;
            
            _sceneService.Value.GameIsOver = true;
            StopAllUnits();
            ShowEndgamePopup("You Win");
        }

        private void CheckLoseCondition()
        {
            foreach (var entity in _collisionEvtFilter.Value)
            {
                ref var collisionEvt = ref _collisionEvtPool.Value.Get(entity);
                var collidedEntity = collisionEvt.CollidedEntity;
                
                if(!_playerTagPool.Value.Has(collidedEntity))
                    continue;

                _sceneService.Value.GameIsOver = true;
                StopAllUnits();
                ShowEndgamePopup("You Lose");
            }
        }

        private void StopAllUnits()
        {
            foreach (var entity in _unitCmpFilter.Value)
            {
                _unitCmpFilter.Pools.Inc1.Del(entity);
            }
        }

        private void ShowEndgamePopup(string message)
        {
            var popupWindow = _sceneService.Value.PopupView;
            
            popupWindow.SetActive(true);
            popupWindow.SetDescription(message);
            popupWindow.SetButtonText("Restart");
            popupWindow.Button.onClick.RemoveAllListeners();
            popupWindow.Button.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}