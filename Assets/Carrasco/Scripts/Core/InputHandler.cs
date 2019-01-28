using System.Collections;
using System.Collections.Generic;
using Carrasco.Commands;
using Carrasco.Pleaceables;
using UnityEngine;
namespace Carrasco.Core
{
    public class InputHandler
    {
        MovePlaceableCommand movePlaceable;
        PlacePlaceableCommand placePlaceable;
        SelectPlaceableCommand selectPlaceable;
        DeselectPlaceableCommand deselectPlaceable;


        public InputHandler()
        {
            this.movePlaceable = new MovePlaceableCommand();
            this.placePlaceable = new PlacePlaceableCommand();
            this.selectPlaceable = new SelectPlaceableCommand();
            this.deselectPlaceable = new DeselectPlaceableCommand();
        }

        public Command Handle()
        {

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Aqui");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    Debug.Log(hit.transform.name);
                    var placeable = hit.transform.GetComponent<BasePlaceable>();
                    if (placeable && placeable.IsPlaced)
                    {
                        if (!GameManager.Instance.SelectedPlacedPlaceable)
                        {
                            GameManager.Instance.SelectedPlacedPlaceable = placeable;
                            return selectPlaceable;
                        }
                    }
                    else if (!placeable && GameManager.Instance.SelectedPlacedPlaceable)
                    {
                        return deselectPlaceable;
                    }

                    if (!placeable && GameManager.Instance.CurrPlaceable && !GameManager.Instance.CurrPlaceable.IsPlaced)
                    {
                        return placePlaceable;
                    }
                }

            }

            if (GameManager.Instance.CurrPlaceable && !GameManager.Instance.CurrPlaceable.IsPlaced)
            {
                return this.movePlaceable;
            }


            return null;
        }
    }
}
