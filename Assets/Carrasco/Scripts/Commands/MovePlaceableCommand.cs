using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class MovePlaceableCommand : Command
    {
        public override void Execute()
        {
            if (!GameManager.Instance.CurrPlaceable.IsConfirmPlacing)
            {
                GameManager.Instance.CurrPlaceable.MovePlaceableObject();
            }
        }
    }
}