using UnityEngine;

namespace ingame.enemy {
    [RequireComponent(typeof(EnemyMove))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyAction : MonoBehaviour {

        private EnemyMove move_;
        private EnemyAttack attack_;

        private void Awake() {
            move_ = GetComponent<EnemyMove>();
            attack_ = GetComponent<EnemyAttack>();
        }

        public void Action(ActionType action) {
            switch (action) {
                case ActionType.Move:
                    move_.Move();
                    break;
                case ActionType.Attack:
                    attack_.Attack();
                    break;
                default:
                    return;
            }
        }
    }

    public enum ActionType {
        None,
        Move,
        Attack
    };
}
