using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace ingame.enemy {
    public class EnemyMove : MonoBehaviour {

        private Transform target_;
        private int obstacle_mask_;

        private void Awake() {
            obstacle_mask_ = LayerMask.GetMask(new string[] { "Wall", "Enemy" });
        }

        private void Start() {
            target_ = GameObject.FindWithTag("Player").transform;
        }

        public void Move() {
            ActionDir move_dir = DecideMoveDir();

            switch (move_dir) {
                case ActionDir.None:
                    return;
                case ActionDir.Left:
                    transform.DOMove(EnemyCommonSettings.left_down_, EnemyCommonSettings.action_speed_).SetRelative();
                    break;
                case ActionDir.Front:
                    transform.DOMove(EnemyCommonSettings.front_, EnemyCommonSettings.action_speed_).SetRelative();
                    break;
                case ActionDir.Right:
                    transform.DOMove(EnemyCommonSettings.right_down_, EnemyCommonSettings.action_speed_).SetRelative();
                    break;
            }
        }

        private ActionDir DecideMoveDir() {

            List<ActionDir> dirs = CanMoveDirList();

            if (dirs.Count == 0) {
                return ActionDir.None;
            }

            ActionDir target_dir = TargetDir();

            //ターゲットの方向に進める場合は進む
            foreach (var dir in dirs) {
                if (dir == target_dir) {
                    return target_dir;
                }
            }

            //ターゲットの方向が正面で、正面に行けない場合は左右どちらかランダムで
            if (target_dir == ActionDir.Front) {
                return dirs[Random.Range(0, dirs.Count)];
            }

            //ターゲットの方向が左右で、その方向に行けない場合は、正面へ行けるならば正面へ
            if (dirs.Contains(ActionDir.Front)) {
                return ActionDir.Front;
            }

            return dirs[0];
        }

        private ActionDir TargetDir() {
            if (target_.position.x < transform.position.x) {
                return ActionDir.Left;
            } else if (Mathf.Approximately(target_.position.x, transform.position.x)) {
                return ActionDir.Front;
            } else {
                return ActionDir.Right;
            }
        }

        private List<ActionDir> CanMoveDirList() {

            List<ActionDir> dirs = new List<ActionDir>();

            if (CheckCanMove(EnemyCommonSettings.left_down_)) {
                dirs.Add(ActionDir.Left);
            }

            if (CheckCanMove(EnemyCommonSettings.front_)) {
                dirs.Add(ActionDir.Front);
            }

            if (CheckCanMove(EnemyCommonSettings.right_down_)) {
                dirs.Add(ActionDir.Right);
            }

            return dirs;
        }

        private bool CheckCanMove(Vector2 ray_vec) {
            Vector3 ray_start_pos = transform.position + new Vector3(ray_vec.x / 2, ray_vec.y / 2, 0);

            var obj = Physics2D.Raycast(ray_start_pos, ray_vec, EnemyCommonSettings.ray_length_, obstacle_mask_);
            return obj.collider == null || obj.collider.gameObject == transform.gameObject;
        }


    }

}
