using Game.Characters;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Game/Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Characters")]
        [SerializeField] CharacterSettings _mage;
        [SerializeField] CharacterSettings _warrior;

        public CharacterSettings Mage => _mage;
        public CharacterSettings Warrior => _warrior;
    }
}