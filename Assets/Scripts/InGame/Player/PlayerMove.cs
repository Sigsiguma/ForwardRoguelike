using UnityEngine;
using UniRx;
using DG.Tweening;

namespace ingame.player {
    public partial class PlayerAction {

        private const float move_speed_ = 0.3f;
        private int obstacle_mask_;

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
                    transform.DOMove(transform.position + new Vector3(-0.5f, 0.5f, 0f), move_speed_);
                    break;
                case PlayerDir.Front:
                    transform.DOMove(transform.position + new Vector3(0f, 0.5f, 0f), move_speed_);
                    break;
                case PlayerDir.Right:
                    transform.DOMove(transform.position + new Vector3(0.5f, 0.5f, 0f), move_speed_);
                    break;
            }
        }
    }
}
