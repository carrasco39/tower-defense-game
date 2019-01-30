using UnityEngine;
using Carrasco.Mobiles;
using Carrasco.Extensions;
using System.Collections.Generic;

namespace Carrasco.Core
{
    using System;
    using static WavePreset;

    public class GameWave : MonoBehaviour
    {
        public WavePreset wavePreset;
        public Transform[] spawners;
        private List<WaveModel> wavesToPlay;
        private int currWave;
        private float currTimer = 0;
        private int currSpawnNumber = 0;

        public int CurrentWave
        {
            get
            {
                return this.currWave+1;
            }
        }


        void Start()
        {
            this.currWave = 0;
            this.wavesToPlay = new List<WaveModel>();
            if (!this.wavePreset) this.wavePreset = Resources.Load<WavePreset>("DefaultWavePreset");
            for (var i = 0; i < this.wavePreset.Waves.Length; i++)
            {
                this.wavesToPlay.Add(this.wavePreset.Waves[i]);
            }
            this.PlayWave();
        }


        async void PlayWave()
        {
            Debug.Log($"WAVE {this.currWave + 1}");
            if (this.currWave < this.wavesToPlay.Count)
            {
                this.currSpawnNumber = this.wavesToPlay[this.currWave].SpawnNumber;
                while (this.currSpawnNumber > 0)
                {
                    var enemyIdx = UnityEngine.Random.Range(0, this.wavesToPlay[this.currWave].Enemy.Length - 1);
                    var enemy = this.wavesToPlay[this.currWave].Enemy[enemyIdx].gameObject.Spawn(this.wavesToPlay[this.currWave].Enemy[enemyIdx]).GetComponent<Enemy>();
                    var rand = UnityEngine.Random.Range(0, spawners.Length);
                    enemy.transform.position = this.spawners[rand].transform.position;
                    await new WaitUntil(() => enemy.agent);
                    enemy.agent.enabled = true;
                    enemy.Move();
                    await new WaitForSeconds(this.wavesToPlay[this.currWave].SpawnDelay);
                    this.currSpawnNumber--;
                }
                return;
            }
            this.wavesToPlay = null;
            GameManager.Instance.GameEndEvent.Invoke();
        }

        void Update()
        {
            if (this.wavesToPlay != null && this.currSpawnNumber <= 0)
            {
                if (this.EnemiesOnScene() <= 0 || this.currTimer >= this.wavesToPlay[this.currWave].WaveDuration)
                {
                    this.currTimer = 0f;
                    this.currWave++;
                    this.PlayWave();
                }
                this.currTimer += Time.deltaTime;
            }
        }

        int EnemiesOnScene()
        {
            return FindObjectsOfType<Enemy>().Length;
        }
    }
}