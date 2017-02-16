using UnityEngine;
using UniRx;

namespace ingame.player {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerAction))]
    public class PlayerAnimation : MonoBehaviour {

        private Animator animator_;
        private PlayerAction player_action_;

        private int left_id_;
        private int front_id_;
        private int right_id_;

        private void Awake() {
            animator_ = GetComponent<Animator>();
            player_action_ = GetComponent<PlayerAction>();
            left_id_ = Animator.StringToHash("Left");
            front_id_ = Animator.StringToHash("Front");
            right_id_ = Animator.StringToHash("Right");
        }

        private void Start() {

            player_action_.PlayerDirAsObservable
                        .Subscribe(PlayerAnimationUpdate)
                        .AddTo(this);
        }

        private void PlayerAnimationUpdate(PlayerDir dir) {

            switch (dir) {
                case PlayerDir.Left:
                    animator_.SetTrigger(left_id_);
                    break;
                case PlayerDir.Front:
                    animator_.SetTrigger(front_id_);
                    break;
                case PlayerDir.Right:
                    animator_.SetTrigger(right_id_);
                    break;
            }
        }

    }
}