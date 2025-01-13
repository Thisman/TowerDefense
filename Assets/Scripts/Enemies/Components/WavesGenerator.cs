using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class WaveGenerator
    {
        [Serializable]
        public class Wave
        {
            public string Name;
            public float WaveHealth;
            public List<Batch> Batches;
            public List<string> PossibleEnemies;
        }

        [Serializable]
        public class Batch
        {
            public int Count;
            public float Health;
            public GameObject Enemy;
        }

        private readonly string _waveResourcesPath = "Data/Waves/waves";
        private readonly string _enemiesResourcesPath = "Prefabs/Enemies";
        private Dictionary<string, GameObject> _loadedEnemies;
        private List<Wave> _loadedWaves;

        public List<Wave> ProcessWaves()
        {
            LoadObjectsFromResources();
            LoadWavesFromResources();

            foreach (var wave in _loadedWaves)
            {
                wave.Batches = GenerateBatches(wave.PossibleEnemies, wave.WaveHealth);
            }

            return _loadedWaves;
        }

        private List<Batch> GenerateBatches(List<string> possibleEnemies, float waveHealth)
        {
            var batches = new List<Batch>();
            float totalHealth = 0;

            var random = new System.Random();

            while (totalHealth < waveHealth)
            {
                var enemy = possibleEnemies[random.Next(possibleEnemies.Count)];
                var count = random.Next(1, 10); // Random count between 1 and 10
                var health = (float)Math.Round(random.NextDouble() * 10, 2); // Random health between 0 and 10 (rounded to 2 decimals)

                batches.Add(new Batch
                {
                    Enemy = _loadedEnemies[enemy],
                    Count = count,
                    Health = health
                });

                totalHealth += health;
            }

            return batches;
        }

        private void LoadObjectsFromResources()
        {
            _loadedEnemies = new Dictionary<string, GameObject>();

            // Load all GameObjects from the specified folder
            GameObject[] objects = Resources.LoadAll<GameObject>(_enemiesResourcesPath);

            // Populate the dictionary
            foreach (var obj in objects)
            {
                if (!_loadedEnemies.ContainsKey(obj.name))
                {
                    _loadedEnemies.Add(obj.name, obj);
                }
                else
                {
                    Debug.LogWarning($"Duplicate object name found: {obj.name}. Skipping.");
                }
            }

            Debug.Log($"Loaded {_loadedEnemies.Count} objects from {_enemiesResourcesPath}.");
        }

        private void LoadWavesFromResources()
        {
            var _wavesJson = Resources.Load<TextAsset>(_waveResourcesPath);

            if (_wavesJson == null)
            {
                Debug.LogError($"Resource not found at {_waveResourcesPath}");
                return;
            }

            string jsonInput = _wavesJson.text;

            // Deserialize JSON into a list of waves
            _loadedWaves = JsonUtility.FromJson<WaveList>(jsonInput).waves;
        }

        [Serializable]
        private class WaveList
        {
            public List<Wave> waves;
        }
    }
}
