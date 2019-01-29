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
                var barrier = Resources.Load<Barrier>("Barrier");
                if (barrier.Cost < GameManager.Instance.Score)
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
                var tower = Resources.Load<Tower>("Tower");
                if (tower.Cost < GameManager.Instance.Score)
                {
                    GameManager.Instance.CurrPlaceable = tower.gameObject.Spawn(tower).GetComponent<Tower>();
                }
            }
        }
        void Update()
        {
            if (GameManager.Instance.Score > 0)
            {

                this.Score = "" + Mathf.Round(GameManager.Instance.Score);
            }
        }
    }
}

