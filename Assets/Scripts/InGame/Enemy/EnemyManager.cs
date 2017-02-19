using UnityEngine;
using System.Collections.Generic;
using ingame.system;
using UniRx;

namespace ingame.enemy {
    public class EnemyManager : MonoBehaviour {

        private List<EnemyAction> enemy_list_;
        private List<EnemyAction> attack_list_;
        private List<EnemyAction> move_list_;

        private void Awake() {
            enemy_list_ = new List<EnemyAction>();
            attack_list_ = new List<EnemyAction>();
            move_list_ = new List<EnemyAction>();
        }

        private void Start() {
            GameManager.Instance.TurnStep
                                .Where(turn => turn == NextStep.EnemyMove)
                                .Subscribe(_ => {
                                    AssortActionType();
                                    MovePriority();
                                })
                                .AddTo(this);

            GameManager.Instance.TurnStep
                                .Where(turn => turn == NextStep.EnemyAct)
                                .Subscribe(_ => {
                                    AssortActionType();
                                    AttackPriority();
                                })
                                .AddTo(this);
        }


        public void AddList(EnemyAction action) {
            if (action == null) return;
            enemy_list_.Add(action);
        }

        private void AssortActionType() {
            attack_list_.Clear();
            move_list_.Clear();

            foreach (var enemy in enemy_list_) {
                if (CheckCanAttack(enemy.transform, EnemyCommonSettings.left_down_) ||
                    CheckCanAttack(enemy.transform, EnemyCommonSettings.front_) ||
                    CheckCanAttack(enemy.transform, EnemyCommonSettings.right_down_)) {
                    attack_list_.Add(enemy);
                } else {
                    move_list_.Add(enemy);
                }
            }
        }

        private bool CheckCanAttack(Transform enemy, Vector2 ray_vec) {
            Vector3 ray_start_pos = enemy.position + new Vector3(ray_vec.x / 2, ray_vec.y / 2, 0);
            var obj = Physics2D.Raycast(ray_start_pos, ray_vec, EnemyCommonSettings.ray_length_, EnemyCommonSettings.player_mask_);
            return obj.collider != null;
        }

        private void AttackPriority() {
            foreach (var action in attack_list_) {
                action.Action(ActionType.Attack);
            }

            foreach (var action in move_list_) {
                action.Action(ActionType.Move);
            }
        }

        private void MovePriority() {
            foreach (var action in move_list_) {
                action.Action(ActionType.Move);
            }

            foreach (var action in attack_list_) {
                action.Action(ActionType.Attack);
            }
        }

    }
}
