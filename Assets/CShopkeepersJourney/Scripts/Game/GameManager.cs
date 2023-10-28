using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using BayatGames.SaveGameFree;

namespace com.vollmergames
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public float RemainingTime { get; private set; }
        public bool IsGameRunning { get; private set; }

        public LevelSettings currentLevel;

        public AudioMixer AudioMixer;
        public AudioSource TensionAudioSource;
        public AudioClip TensionAudioClip;
        public AudioClip GameEndAudioClip;
        public float TensionAudioStartRemainingTime = 30f;

        public List<ShopItemSpawner> ItemSpawners;
        public GameObject ItemSpawnerParent;

        public delegate void GameEventDelegate(); // Custom delegate type for game events.

        public event GameEventDelegate OnGameStart;
        public event GameEventDelegate OnGameEnd;
        public event GameEventDelegate OnGameAbort;
        [SerializeField]
        private int score;
        [SerializeField]
        private int lastScore;

        private Coroutine countDownRoutine;

        private bool tensionAudioPlaying = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            if (ItemSpawnerParent)
            {

                ItemSpawners = new List<ShopItemSpawner>();
                foreach (Transform item in ItemSpawnerParent.transform)
                {
                    var sis = item.gameObject.GetComponent<ShopItemSpawner>();
                    if (sis)
                    {
                        ItemSpawners.Add(sis);
                    }
                }
            }
        }

        internal void SetCurrentLevel(LevelSettings levelSettings)
        {
            currentLevel = levelSettings;
            lastScore = SaveGame.Load<int>(currentLevel.levelName, 0);
        }

        private void Start()
        {
            // You can subscribe to events here if needed.            
        }

        public void StartGame()
        {
            if (!IsGameRunning)
            {
                if (currentLevel == null)
                {
                    Debug.LogError("Current level null");
                }
                lastScore = SaveGame.Load<int>(currentLevel.levelName, 0);
                SetupSpawners();
                IsGameRunning = true;
                score = 0;
                // Notify subscribers that the game has started.
                OnGameStart?.Invoke();

                // Start the countdown coroutine.
                countDownRoutine = StartCoroutine(CountdownCoroutine());
            }
        }

        public void EndGame()
        {
            TensionAudioSource.Stop();
            tensionAudioPlaying = false;            
            if (IsGameRunning)
            {
                TensionAudioSource.PlayOneShot(GameEndAudioClip);
                IsGameRunning = false;
                StopCoroutine(countDownRoutine);
                ResetSpawners();
                // Notify subscribers that the game has ended.
                OnGameEnd?.Invoke();
                // save score               
                if (lastScore < score) {
                    Debug.Log("New high score saving...");
                    SaveGame.Save<int>(currentLevel.levelName, score);
                }

            }
        }

        private void ResetSpawners()
        {
            foreach (var spawner in ItemSpawners)
            {
                spawner.ResetSpawner();
            }
        }

        public void AbortGame()
        {
            TensionAudioSource.Stop();
            tensionAudioPlaying = false;
            if (IsGameRunning)
            {
                IsGameRunning = false;
                StopCoroutine(countDownRoutine);
                ResetSpawners();
                // Notify subscribers that the game has been aborted.
                OnGameAbort?.Invoke();
            }
        }

        public void PlayerScored()
        {
            // handle player scored
            var newPoints = currentLevel.scorePerItem;
            
            IncreaseScore(newPoints);
        }

        private IEnumerator CountdownCoroutine()
        {
            RemainingTime = currentLevel.timeLimit;

            while (RemainingTime > 0)
            {

                RemainingTime -= Time.deltaTime;

                if (RemainingTime < TensionAudioStartRemainingTime && !tensionAudioPlaying) {
                    TensionAudioSource.PlayOneShot(TensionAudioClip);
                    tensionAudioPlaying = true;
                }

                yield return null;
            }

            // End the game when the countdown is finished.
            EndGame();
        }

        void SetupSpawners()
        {

            Queue<ShopItemSpawner> spawnerQueue = new Queue<ShopItemSpawner>();
            ItemSpawners.ForEach(i => spawnerQueue.Enqueue(i));
            
            foreach (var learnItem in ShuffleList(currentLevel.learningItems))
            {
                var spawner = spawnerQueue.Dequeue();
                spawner.LearningItem = learnItem;
                spawner.Setup();
            }

        }

        private List<ChineseLearningItem> ShuffleList(List<ChineseLearningItem> inputList)
        {
            List<ChineseLearningItem> shuffledList = new List<ChineseLearningItem>(inputList);
            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                ChineseLearningItem value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }

        public void IncreaseScore(int points)
        {
            if (IsGameRunning)
            {
                score += points;
            }
        }

        public int GetScore()
        {
            return score;
        }

        public int GetLastScore() {
            return lastScore;
        }
    }
}
