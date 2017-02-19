using UnityEngine;
using UniRx;

namespace ingame.enemy {
    [RequireComponent(typeof(EnemyMove))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyAnimation : MonoBehaviour {

        private Animator animator_;
        private EnemyMove move_;
        private EnemyAttack attack_;

        private int left_id_;
        private int front_id_;
        private int right_id_;

        private void Awake() {
            animator_ = GetComponent<Animator>();
            move_ = GetComponent<EnemyMove>();
            attack_ = GetComponent<EnemyAttack>();
            left_id_ = Animator.StringToHash("Left");
            front_id_ = Animator.StringToHash("Front");
            right_id_ = Animator.StringToHash("Right");
        }

        private void Start() {
            move_.MoveDirAsObservable
                 .DistinctUntilChanged()
                 .Subscribe(EnemyAnimationUpdate)
                 .AddTo(this);

            attack_.AttackDirAsObservable
                   .DistinctUntilChanged()
                   .Subscribe(EnemyAnimationUpdate)
                   .AddTo(this);
        }

        private void EnemyAnimationUpdate(ActionDir dir) {

            switch (dir) {
                case ActionDir.Left:
                    animator_.SetTrigger(left_id_);
                    break;
                case ActionDir.Front:
                    animator_.SetTrigger(front_id_);
                    break;
                case ActionDir.Right:
                    animator_.SetTrigger(right_id_);
                    break;
            }
        }
    }
}
