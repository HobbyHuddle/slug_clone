using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "New Character SFX", menuName = "Game/Character SFX", order = 0)]
    public class CharacterSfx : ScriptableObject
    {
        public AudioClip run;
        public AudioClip die;
    }
}