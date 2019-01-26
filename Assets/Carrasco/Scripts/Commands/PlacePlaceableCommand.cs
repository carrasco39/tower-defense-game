using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class PlacePlaceableCommand : Command
    {
        public override void Execute()
        {
            GameManager.Instance.CurrPlaceable.MovePlaceableObject();
        }
    }
}