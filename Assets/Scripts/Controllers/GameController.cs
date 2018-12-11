using System;
using DataBase;
using Model;
using SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private GameState _state;
        private GameData _gameData;

        private void Start()
        {
            _state = AssetDatabase.LoadAssetAtPath ("Assets/Game State.asset", typeof(GameState)) as GameState;
            if (_state != null) _gameData = _state.Data;
        }

        public void AddResource(ResourceType type, float value)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    _gameData.Gold += value;
                    break;
                case ResourceType.Brick:
                    _gameData.Bricks += value;
                    break;
                case ResourceType.Science:
                    _gameData.Science += value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        private void OnDisable()
        {
            _state.Data = _gameData;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
