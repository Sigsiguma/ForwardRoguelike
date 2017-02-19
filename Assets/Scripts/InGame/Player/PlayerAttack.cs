using UnityEngine;
using DG.Tweening;
using UniRx;

namespace ingame.player {
    public partial class PlayerAction {

        private int enemy_mask_;
        private Subject<Unit> PlayerAttacked;

        public IObservable<Unit> PlayerAttackedAsObservable { get { return PlayerAttacked.AsObservable(); } }

        private bool CheckCanAttack(PlayerDir dir) {
            switch (dir) {
                case PlayerDir.Left: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0.5f), ray_length_, enemy_mask_);
                        return obj.collider != null;
                    }
                case PlayerDir.Front: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(0f, 0.5f), ray_length_, enemy_mask_);
                        return obj.collider != null;
                    }
                case PlayerDir.Right: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0.5f), ray_length_, enemy_mask_);
                        return obj.collider != null;
                    }
                default:
                    return false;
            }
        }

        private void Attack(PlayerDir dir) {

            switch (dir) {
                case PlayerDir.Left: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(transform.position + new Vector3(-0.25f, 0.25f, 0f), action_speed_ / 2));
                        sequence.Append(transform.DOMove(transform.position, action_speed_ / 2));
                        sequence.OnComplete(() => PlayerAttacked.OnNext(Unit.Default));
                        break;
                    }
                case PlayerDir.Front: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(transform.position + new Vector3(0f, 0.25f, 0f), action_speed_ / 2));
                        sequence.Append(transform.DOMove(transform.position, action_speed_ / 2));
                        sequence.OnComplete(() => PlayerAttacked.OnNext(Unit.Default));
                        break;
                    }
                case PlayerDir.Right: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(transform.position + new Vector3(0.25f, 0.25f, 0f), action_speed_ / 2));
                        sequence.Append(transform.DOMove(transform.position, action_speed_ / 2));
                        sequence.OnComplete(() => PlayerAttacked.OnNext(Unit.Default));
                        break;
                    }
            }
        }

    }
}