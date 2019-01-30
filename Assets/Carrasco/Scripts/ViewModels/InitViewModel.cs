using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Carrasco.ViewModels {
    [Binding]
    public class InitViewModel : MonoBehaviour, INotifyPropertyChanged  {

        public Button playButton;

        private float loadingBar;

        public event PropertyChangedEventHandler PropertyChanged;
        [Binding]
        public float LoadingBar {
            get {
                return loadingBar;
            }
            set {
                loadingBar = value;

                OnPropertyChanged("LoadingBar");
            }
        }
        void Start() {

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