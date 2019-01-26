using System.Collections;
using System.Collections.Generic;
using Carrasco.Commands;
using UnityEngine;
namespace Carrasco.Core
{
    public class InputHandler
    {
        MovePlaceableCommand movePlaceable;

        public InputHandler()
        {
            this.movePlaceable = new MovePlaceableCommand();
        }

        public Command Handle()
        {
            if(GameManager.Instance.CurrPlaceable) return movePlaceable;

            return null;
        }
    }
}
