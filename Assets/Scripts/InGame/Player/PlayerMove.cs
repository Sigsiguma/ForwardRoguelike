using UnityEngine;
using UniRx;
using DG.Tweening;

namespace ingame.player {
    public partial class PlayerAction {

        private int obstacle_mask_;
        private Subject<Unit> PlayerMoved;

        public IObservable<Unit> PlayerMovedAsObservable { get { return PlayerMoved.AsObservable(); } }

        private bool CheckCanMove(PlayerDir dir) {
            switch (dir) {
                case PlayerDir.Left: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0.5f), ray_length_, obstacle_mask_);
                        return obj.collider == null;
                    }
                case PlayerDir.Front: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(0f, 0.5f), ray_length_, obstacle_mask_);
                        return obj.collider == null;
                    }
                case PlayerDir.Right: {
                        var obj = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0.5f), ray_length_, obstacle_mask_);
                        return obj.collider == null;
                    }
                default:
                    return true;
            }
        }

        private void Move(PlayerDir dir) {

            switch (dir) {
                case PlayerDir.Left:
                    transform.DOMove(new Vector3(-0.5f, 0.5f, 0f), action_speed_)
                             .OnComplete(() => PlayerMoved.OnNext(Unit.Default))
                             .SetRelative();
                    break;
                case PlayerDir.Front:
                    transform.DOMove(new Vector3(0f, 0.5f, 0f), action_speed_)
                             .OnComplete(() => PlayerMoved.OnNext(Unit.Default))
                             .SetRelative();
                    break;
                case PlayerDir.Right:
                    transform.DOMove(new Vector3(0.5f, 0.5f, 0f), action_speed_)
                             .OnComplete(() => PlayerMoved.OnNext(Unit.Default))
                             .SetRelative();
                    break;
            }
        }
    }
}
