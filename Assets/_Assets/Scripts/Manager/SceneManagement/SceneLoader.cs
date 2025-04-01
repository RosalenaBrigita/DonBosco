using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DonBosco
{
    public class SceneLoader : MonoBehaviour, ISaveLoad
    {
        private static SceneLoader instance;
        public static SceneLoader Instance { get { return instance; } }
        
        [SerializeField] private string startingScene;

        private string currentScene;
        public string CurrentScene { get { return currentScene; } }
        private LoadScene loadScene;



        private void Awake() 
        {
            instance = this;

            loadScene = GetComponent<LoadScene>();
        }

        private void OnEnable() 
        {
            SaveManager.Instance.Subscribe(this);

            MainMenuManager.Instance.onBackToMainMenu.AddListener(FireChangeScene);
        }

        private void OnDisable() 
        {
            SaveManager.Instance.Unsubscribe(this);

            MainMenuManager.Instance.onBackToMainMenu.RemoveListener(FireChangeScene);
        }



        public void LoadCurrentScene()
        {
            if(currentScene == null)
            {
                currentScene = startingScene;
            }
            ExecuteLoadScene(() => AddToLoad(currentScene));
        }
        
        public void UnloadCurrentSceneInstantly()
        {
            GameplayPlayer.Instance.ResetConfiner();
            SceneManager.UnloadSceneAsync(currentScene);
        }

        public void AddToLoad(string sceneName)
        {
            loadScene.AddToLoad(sceneName);
            SetCurrentScene(sceneName);
        }

        public void ForceAddToLoad(string sceneName)
        {
            loadScene.ForceAddToLoad(sceneName);
            SetCurrentScene(sceneName);
        }

        public void BackToMainMenu()
        {
            currentScene = "";
            FireChangeScene();
        }

        public void FireChangeScene()
        {
            GameEventsManager.Instance.playerEvents.ChangeScene(currentScene);
        }

        public void AddToUnload(string sceneName)
        {
            loadScene.AddToUnload(sceneName);
        }

        public void ExecuteLoadScene(Action processAction = null, Action OnDone = null)
        {
            loadScene.ExecuteLoadScene(processAction, OnDone);
        }

        public void ReloadCurrentScene()
        {
            ExecuteLoadScene(() => 
            {
                UnloadCurrentSceneInstantly();
                ForceAddToLoad(currentScene);
            });
            GameManager.ResumeGame();
        }



        public void SetCurrentScene(string sceneName)
        {
            currentScene = sceneName;
        }

        public string GetCurrentScene()
        {
            return currentScene;
        }



        public async Task Save(SaveData saveData)
        {
            saveData.currentScene = currentScene;
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if(saveData == null)
                return;

            currentScene = saveData.currentScene;
            await Task.CompletedTask;
        }
    }
}