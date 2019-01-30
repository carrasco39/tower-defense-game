
using UnityEngine;
using UnityEngine.UI;
using UnityWeld;
using UnityWeld.Binding;
using Carrasco.Core;
using Carrasco.Extensions;
using System.Linq;
namespace Carrasco.ViewModels
{
    [Binding]
    public class PlaceableViewModel : MonoBehaviour
    {
        public Button[] buttons;
        void Start()
        {
            this.ToggleCanvas(0);
        }

        [Binding]
        public void OnConfirmPlace() => GameManager.Instance.CurrPlaceable.ConfirmPlace();
        [Binding]
        public void OnCancelPlace() => GameManager.Instance.CurrPlaceable.CancelPlace();
        [Binding]
        public void OnRemove() => GameManager.Instance.SelectedPlacedPlaceable.RemovePlaced();

        void Update() {
            if(GameManager.Instance.SelectedPlacedPlaceable && GameManager.Instance.SelectedPlacedPlaceable.canvas.gameObject == this.gameObject) {
                this.ToggleCanvas(1);
                return;
            }
            this.ToggleCanvas(0);
        }

        private void ToggleCanvas(int num)
        {
            switch (num)
            {
                case 0:
                    foreach (var button in this.buttons)
                    {
                        if (button.name == "DeleteBtn")
                        {
                            button.gameObject.SetActive(false);
                            continue;
                        }
                        button.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    foreach (var button in this.buttons)
                    {
                        if (button.name == "DeleteBtn")
                        {
                            button.gameObject.SetActive(true);
                            continue;
                        }
                        button.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}