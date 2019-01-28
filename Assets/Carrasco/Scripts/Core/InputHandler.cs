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


        public InputHandler()
        {
            this.movePlaceable = new MovePlaceableCommand();
            this.placePlaceable = new PlacePlaceableCommand();
            this.selectPlaceable = new SelectPlaceableCommand();
        }

        public Command Handle()
        {

            if (GameManager.Instance.CurrPlaceable)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Here");
                    return this.placePlaceable;
                }

                return this.movePlaceable;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Aqui");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    Debug.Log(hit.transform.name);
                    var placeable = hit.transform.GetComponent<BasePlaceable>();
                    if (placeable)
                    {
                        Debug.Log("Entrando aqui");
                        GameManager.Instance.CurrPlaceable = placeable;
                        return selectPlaceable;
                    }
                }
            }

            return null;
        }
    }
}
