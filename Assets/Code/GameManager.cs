using Assets.Code.WeaponModule.Interfaces;
using Zenject;

namespace Assets.Code
{
    public class GameManager : IInitializable, ITickable
    {
        [Inject]
        private DebugModule.DebugManager _debugManager;

        public void Initialize()
        {
            _debugManager.Log("Game manager init", layer:"GameManager");
        }

        public void Tick()
        {
            
        }
        //return character
        public void CreateCharacter(int characterId)
        {
            
        }
        //return character with specific weapon
        public void CreateCharacterWithWeapon(int characterId, IWeapon weapon)
        {
            
        }
    }
}