using UnityEngine;
using DG.Tweening;

namespace ingame.enemy {
    public class EnemyAttack : MonoBehaviour {

        private int player_mask_;

        private void Awake() {
            player_mask_ = LayerMask.GetMask(new string[] { "Player" });
        }

        public void Attack() {
            ActionDir attack_dir = DecideAttackDir();

            switch (attack_dir) {
                case ActionDir.None:
                    return;
                case ActionDir.Left: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(new Vector2(EnemyCommonSettings.left_down_.x / 2, EnemyCommonSettings.left_down_.y / 2),
                                         EnemyCommonSettings.action_speed_ / 2.0f).SetRelative());
                        sequence.Append(transform.DOMove(transform.position, EnemyCommonSettings.action_speed_ / 2.0f));
                        break;
                    }
                case ActionDir.Front: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(new Vector2(EnemyCommonSettings.front_.x / 2, EnemyCommonSettings.front_.y / 2),
                                         EnemyCommonSettings.action_speed_ / 2.0f).SetRelative());
                        sequence.Append(transform.DOMove(transform.position, EnemyCommonSettings.action_speed_ / 2.0f));
                        break;
                    }
                case ActionDir.Right: {
                        var sequence = DOTween.Sequence();
                        sequence.Append(transform.DOMove(new Vector2(EnemyCommonSettings.right_down_.x / 2, EnemyCommonSettings.right_down_.y / 2),
                                         EnemyCommonSettings.action_speed_ / 2.0f).SetRelative());
                        sequence.Append(transform.DOMove(transform.position, EnemyCommonSettings.action_speed_ / 2.0f));
                        break;
                    }
            }
        }

        private ActionDir DecideAttackDir() {

            if (CheckCanAttack(EnemyCommonSettings.left_down_)) {
                return ActionDir.Left;
            }

            if (CheckCanAttack(EnemyCommonSettings.front_)) {
                return ActionDir.Front;
            }

            if (CheckCanAttack(EnemyCommonSettings.right_down_)) {
                return ActionDir.Right;
            }

            return ActionDir.None;
        }

        private bool CheckCanAttack(Vector2 ray_vec) {

            Vector3 ray_start_pos = transform.position + new Vector3(ray_vec.x / 2, ray_vec.y / 2, 0);
            var obj = Physics2D.Raycast(ray_start_pos, ray_vec, EnemyCommonSettings.ray_length_, player_mask_);

            return obj.collider != null;
        }

    }
}
