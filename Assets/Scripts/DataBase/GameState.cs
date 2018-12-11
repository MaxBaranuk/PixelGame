using Model;
using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = "New Game State", menuName = "Game State", order = 51)]
    public class GameState : ScriptableObject
    {
        public GameData Data;
    }
}