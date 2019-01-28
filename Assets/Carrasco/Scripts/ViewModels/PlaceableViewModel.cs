
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
        Button[] buttons;
        void Start()
        {
            this.buttons = this.GetComponentsInChildren<Button>();
            this.buttons.Where(x => x.name == "DeleteBtn").First().gameObject.SetActive(false);
            this.buttons.Where(x => x.name != "DeleteBtn").Select(x =>
            {
                x.gameObject.SetActive(true);
                return x;
            });
        }

        void OnEnabled()
        {
            this.buttons.Where(x => x.name == "DeleteBtn").First().gameObject.SetActive(false);
            this.buttons.Where(x => x.name != "DeleteBtn").Select(x =>
            {
                x.gameObject.SetActive(true);
                return x;
            });
        }


        [Binding]
        public void OnConfirmPlace() => GameManager.Instance.CurrPlaceable.ConfirmPlace();
        [Binding]
        public void OnCancelPlace() => GameManager.Instance.CurrPlaceable.CancelPlace();
        [Binding]
        public void OnDelete()
        {
            GameManager.Instance.CurrPlaceable.gameObject.Recycle(GameManager.Instance.CurrPlaceable);
        }

        public void Update()
        {
            if (
                GameManager.Instance.CurrPlaceable &&
                GameManager.Instance.CurrPlaceable.gameObject == this.transform.parent.gameObject &&
                GameManager.Instance.CurrPlaceable.IsPlaced
                )
            {
                this.ToggleButtons("remove");
            }
        }

        public void ToggleButtons(string btn)
        {
            switch (btn)
            {
                case "remove":
                    this.buttons.Select(x =>
                    {
                        if(x.name == "DeleteBtn") {
                            x.gameObject.SetActive(true);
                            return x;
                        }
                        x.gameObject.SetActive(false);
                        return x;
                    });
                    break;
                default:
                    this.buttons.Select(x =>
                    {
                        if(x.name != "DeleteBtn") {
                            x.gameObject.SetActive(true);
                            return x;
                        }
                        x.gameObject.SetActive(false);
                        return x;
                    });
                    break;
            }
        }
    }
}