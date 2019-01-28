using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class SelectPlaceableCommand : Command
    {
        public override void Execute()
        {
            GameManager.Instance.SelectedPlacedPlaceable.OnPlacedSelected();
        }
    }
}