using Carrasco.Core;
using Carrasco.Extensions;

namespace Carrasco.Commands {
    public class CancelPlaceableCommand : Command
    {
        public override void Execute()
        {
            var go = GameManager.Instance.CurrPlaceable;
            go.gameObject.Recycle(go);
            GameManager.Instance.CurrPlaceable = null;
        }
    }
}