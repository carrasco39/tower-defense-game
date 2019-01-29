using UnityEngine;
using UnityWeld.Binding;
using Carrasco.Mobiles;
using System.ComponentModel;

namespace Carrasco.ViewModels
{
    [Binding]
    public class MobileViewModel : MonoBehaviour, INotifyPropertyChanged
    {

        private float health;
        [Binding]
        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                if(health == value) return;

                health = value;

                OnPropertyChanged("Health");
            }
        }

        private BaseMobile mobile;

        public event PropertyChangedEventHandler PropertyChanged;

        void Start()
        {
            this.mobile = GetComponentInParent<BaseMobile>();
        }

        void Update()
        {
            this.Health = this.mobile.Health / this.mobile.MaxHealth;
        }

        void OnPropertyChanged(string propertyName) {
            if(this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}