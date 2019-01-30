using UnityEngine;
using Carrasco.Mobiles;

namespace Carrasco.Core
{
    [CreateAssetMenu(fileName = "New Wave Preset", menuName = "Wave/New Wave Preset")]
    public class WavePreset : ScriptableObject
    {
        public WaveModel[] Waves;

        [System.Serializable]
        public class WaveModel
        {
            public Enemy[] Enemy;
            [Tooltip("Total number of enemies to spawn")]
            public int SpawnNumber;
            [Tooltip("Delay of the spawn for each enemy in seconds")]
            public float SpawnDelay;
            [Tooltip("Duration of the Wave in seconds")]
            public float WaveDuration;
        }
    }
}