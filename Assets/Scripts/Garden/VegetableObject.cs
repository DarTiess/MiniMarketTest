using System.Collections;
using DefaultNamespace.Store;
using DG.Tweening;
using UnityEngine;

namespace Garden
{
    public class VegetableObject: MonoBehaviour
    {
        private float _growSpeed;
        private float _height;
        private float _timer;
        private GardenGrowing _parent;
        private Vector3 _startPosition;

        public void InitVegetable(float speed, float height, float timer, GardenGrowing parent)
        {
            _growSpeed = speed;
            _height = height;
            _timer = timer;
            _parent = parent;
          transform.localPosition -= new Vector3(0, _height, 0);
          _startPosition = transform.position;
            SetToStartPosition();
           
        }

        public void Growing()
        {
            StartCoroutine(Grow());
        }

        public void PushingToPlayer(PlayersStack target)
        {
            transform.DOJump(target.transform.position, 4f, 1, 0.5f)
                     .OnComplete(() =>
                     {
                         SetToStartPosition();
                         target.StackVegetable();
                     });
        }

        IEnumerator Grow()
        {
            yield return new WaitForSeconds(_timer);
           
            transform.DOLocalMoveY(_height, _growSpeed).SetEase(Ease.InElastic)
                     .OnComplete(() =>
                     {
                         _parent.VagetableIsReady(this);
                     });
        }

        private void SetToStartPosition()
        {
            transform.position = _startPosition; 
            Growing();
        }

        public void PushingFromPlayer(Store target, PlayersStack player)
        {
            transform.DOJump(target.transform.position, 4f, 1, 0.5f)
                     .OnComplete(() =>
                     {
                         target.StackIn();
                         player.StackOut();
                     });
        }
    }
}