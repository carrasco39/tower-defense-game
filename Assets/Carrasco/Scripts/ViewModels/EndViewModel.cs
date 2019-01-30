using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Carrasco.Core;

namespace Carrasco.ViewModels {
    [Binding]
    public class EndViewModel : MonoBehaviour, INotifyPropertyChanged  {

        public Button playButton;

        private string gameStatus;

        public event PropertyChangedEventHandler PropertyChanged;
        [Binding]
        public string GameStatus {
            get {
                return gameStatus;
            }
            set {
                gameStatus = value;

                OnPropertyChanged("GameStatus");
            }
        }
        void Start() {
            this.GameStatus = GameManager.Instance.Life > 0 ? "You Won!" : "You Lose!";

        }
        [Binding]
        public async void OnPlay() {
            playButton.gameObject.SetActive(false);
            await SceneManager.LoadSceneAsync("Game");
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}