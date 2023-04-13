using DefaultNamespace.Store;
using DG.Tweening;

namespace Vegetables
{
  
    public class VegetableInPlayer : VegetableBase
    {
        public void PushingFromPlayer(Store target, PlayersStack player)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.StackIn();
                         player.StackOut();
                     });
        }
    }
}