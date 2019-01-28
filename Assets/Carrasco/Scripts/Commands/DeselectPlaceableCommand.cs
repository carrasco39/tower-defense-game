using Carrasco.Core;
using UnityEngine;

namespace Carrasco.Commands
{
    public class DeselectPlaceableCommand : Command
    {
        public override void Execute()
        {
            GameManager.Instance.SelectedPlacedPlaceable.OnPlacedDeselected();
        }
    }
}