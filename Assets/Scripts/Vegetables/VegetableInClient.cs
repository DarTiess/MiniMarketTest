using DefaultNamespace.Cash;
using DG.Tweening;
using Vegetables;

namespace DefaultNamespace.Clients
{
    public class VegetableInClient: VegetableBase
    {
        public void PushingToCashBox(CashTable target, ClientStack client)
        {
            transform.DOJump(target.BoxPlace.position, _jumpForce, 1, _speed)
                  .OnComplete(() =>
                  {
                    target.StackIn(client);
                    client.StackOut();
                  });
          }
          
    }
}