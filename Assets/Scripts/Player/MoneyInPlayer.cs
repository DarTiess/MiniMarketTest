using DefaultNamespace.BuyPlace;
using DefaultNamespace.Cash;
using DG.Tweening;
using UnityEngine;

public class MoneyInPlayer : MonoBehaviour
{
    private float _speed;
    private float _jumpForce;
  

    public void InitMoney(float speed, float jumpForce)
    {
        _speed = speed;
        _jumpForce = jumpForce;
       
    }

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