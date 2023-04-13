using DefaultNamespace.BuyPlace;
using DG.Tweening;
using Vegetables;

public class MoneyInPlayer : VegetableBase
{
    public void PushingToBuyPlace(BuyPlace target, PlayerMoneyStack player)
    {
        transform.DOJump(target.transform.position, _jumpForce, 1, _speed)
                 .OnComplete(() =>
                 { 
                    
                     target.StackIn();
                     player.StackOut(this);
                       
                 });
    }
   
}