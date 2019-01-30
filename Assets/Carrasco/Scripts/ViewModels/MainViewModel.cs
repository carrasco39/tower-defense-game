using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using Carrasco.Core;
using Carrasco.Pleaceables;
using Carrasco.Extensions;
using System;

namespace Carrasco.ViewModels
{
    [Binding]
    public class MainViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private string score;
        private string wave;
        private string life;

        [Binding]
        public string Score
        {
            get
            {
                return score;
            }
            set
            {
                if (score == value) return;

                score = value;

                OnPropertyChanged("Score");
            }
        }

        [Binding]
        public string Life
        {
            get
            {
                return life;
            }
            set
            {
                if (life == value) return;

                life = value;

                OnPropertyChanged("Life");
            }
        }

        [Binding]
        public string Wave {
            get {
                return this.wave;
            }
            set {
                if(wave == value) return;
                wave = value;

                OnPropertyChanged("Wave");
            }
        }



        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Binding]
        public void AddBarrier()
        {
            if (!GameManager.Instance.CurrPlaceable)
            {
                var barrier = FindObjectOfType<Barrier>();//Resources.Load<Barrier>("Barrier");
                if (barrier.Cost <= GameManager.Instance.Score)
                {
                    GameManager.Instance.CurrPlaceable = barrier.gameObject.Spawn(barrier).GetComponent<Barrier>();
                }
            }
        }
        [Binding]
        public void AddTower()
        {
            if (!GameManager.Instance.CurrPlaceable)
            {
                var tower = FindObjectOfType<Tower>();//Resources.Load<Tower>("Tower");
                if (tower.Cost <= GameManager.Instance.Score)
                {
                    GameManager.Instance.CurrPlaceable = tower.gameObject.Spawn(tower).GetComponent<Tower>();
                }
            }
        }
        void Update()
        {
            if (GameManager.Instance.Score >= 0)
            {
                this.Score = "" + Mathf.Round(GameManager.Instance.Score);
            }
            this.Wave = $"WAVE {GameManager.Instance.GameWave?.CurrentWave}";
            this.Life = "Life Left " + GameManager.Instance.Life;
        }
    }
}

