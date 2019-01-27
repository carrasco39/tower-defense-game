using System.Collections;
using System.Collections.Generic;
using Carrasco.Commands;
using UnityEngine;
namespace Carrasco.Core
{
    public class InputHandler
    {
        MovePlaceableCommand movePlaceable;
        PlacePlaceableCommand placePlaceable;

        public InputHandler()
        {
            this.movePlaceable = new MovePlaceableCommand();
            this.placePlaceable = new PlacePlaceableCommand();
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

            return null;
        }
    }
}
