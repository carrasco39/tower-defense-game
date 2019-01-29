using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using Carrasco.Core;
using Carrasco.Pleaceables;
using Carrasco.Extensions;

namespace Carrasco.ViewModels
{
    [Binding]
    public class MainViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Binding]
        public void AddBarrier()
        {
            if (!GameManager.Instance.CurrPlaceable)
            {
                var barrier = Resources.Load<Barrier>("BarrierTest");
                GameManager.Instance.CurrPlaceable = barrier.gameObject.Spawn(barrier).GetComponent<Barrier>();
            }
        }
        [Binding]
        public void AddTower()
        {
            if (!GameManager.Instance.CurrPlaceable)
            {
                var tower = Resources.Load<Tower>("Tower");
                GameManager.Instance.CurrPlaceable = tower.gameObject.Spawn(tower).GetComponent<Tower>();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ToggleCanvas()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}

