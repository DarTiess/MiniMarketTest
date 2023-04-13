using DefaultNamespace.Clients;
using DG.Tweening;
using Vegetables;

namespace DefaultNamespace.Cash
{
    public class BoxInCashTable : VegetableBase
    {
        public void Pushing(ClientStack target, CashTable source)
        {
            transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                     .OnComplete(() =>
                     {
                         target.HadBox();
                         source.DisableBox();
                     });
        }
    }
}