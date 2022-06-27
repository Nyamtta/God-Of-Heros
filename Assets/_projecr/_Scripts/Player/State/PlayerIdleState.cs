using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodems
{
    public class PlayerIdleState : IState
    {
        private Player _player;
        private Camera _camera;
        private PlayerSettings _settings;

        public PlayerIdleState(Player player, Camera camera, PlayerSettings playerSettings)
        {
            _player = player;
            _camera = camera;
            _settings = playerSettings;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                DrawGroundRey();
        }

        private void DrawGroundRey()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, _settings.groundLayer))
                _player.setMovePoint(hit.point);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
