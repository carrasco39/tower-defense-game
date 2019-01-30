using Carrasco.Core;
using Carrasco.Pleaceables;
using UnityEngine;

namespace Carrasco.Commands
{
    public class SelectPlaceableCommand : Command
    {
        public override void Execute()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                Debug.Log(hit.transform.name);
                var placeable = hit.transform.GetComponentInParent<BasePlaceable>();
                if (placeable && placeable.IsPlaced)
                {
                    if (!GameManager.Instance.SelectedPlacedPlaceable)
                    {
                        GameManager.Instance.SelectedPlacedPlaceable = placeable;
                        GameManager.Instance.SelectedPlacedPlaceable.OnPlacedSelected();
                    }
                }
            }
        }
    }
}