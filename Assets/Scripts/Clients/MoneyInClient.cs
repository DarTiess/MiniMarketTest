using DG.Tweening;
using Vegetables;

namespace DefaultNamespace.Cash
{
    public class MoneyInClient : VegetableBase
    {
        public void PushingToChashTable(CashTable target, ClientMoneyStack clientStack)
        {
            transform.DOJump(target.MoneyPlace.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.StackMoneyIn();
                         clientStack.DisableMoney();
                     });
        }
    }
}