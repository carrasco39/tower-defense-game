using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class MovePlaceableCommand : Command
    {
        public override void Execute()
        {
            GameManager.Instance.CurrPlaceable.MovePlaceableObject();
        }
    }
}