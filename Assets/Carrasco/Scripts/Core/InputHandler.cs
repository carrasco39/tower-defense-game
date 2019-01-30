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
        CancelPlaceableCommand cancelPlaceable;
        FlipPlaceableCommand flipPlaceableCommand;


        public InputHandler()
        {
            this.movePlaceable = new MovePlaceableCommand();
            this.placePlaceable = new PlacePlaceableCommand();
            this.selectPlaceable = new SelectPlaceableCommand();
            this.deselectPlaceable = new DeselectPlaceableCommand();
            this.cancelPlaceable = new CancelPlaceableCommand();
            this.flipPlaceableCommand = new FlipPlaceableCommand();
        }

        public Command Handle()
        {

            if (Input.GetMouseButtonUp(0))
            {

                if (GameManager.Instance.SelectedPlacedPlaceable)
                {
                    return deselectPlaceable;
                }

                if (GameManager.Instance.CurrPlaceable && !GameManager.Instance.CurrPlaceable.IsPlaced)
                {
                    return placePlaceable;
                }

                if (!GameManager.Instance.SelectedPlacedPlaceable)
                {
                    return selectPlaceable;
                }
            }

            if (GameManager.Instance.CurrPlaceable && !GameManager.Instance.CurrPlaceable.IsPlaced)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    return this.flipPlaceableCommand;
                }

                if (Input.GetMouseButton(1))
                {
                    return this.cancelPlaceable;
                }
                
                return this.movePlaceable;
            }


            return null;
        }
    }
}
