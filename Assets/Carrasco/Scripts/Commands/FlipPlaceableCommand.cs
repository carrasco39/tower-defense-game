using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class FlipPlaceableCommand : Command
    {
        public override void Execute()
        {
            if (!GameManager.Instance.CurrPlaceable.IsConfirmPlacing)
            {
                var axis = Input.GetAxis("Mouse ScrollWheel") > 0 ? 0 : 1;
                GameManager.Instance.CurrPlaceable.FlipRotation(axis);
            }
        }
    }
}