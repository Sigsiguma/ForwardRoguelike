using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.camera {
    public class FollowCamera : MonoBehaviour {

        [SerializeField]
        private Transform target_;

        private float goal_y_ = 50f;

        private void Start() {
            this.LateUpdateAsObservable()
                .Where(_ => target_.position.y < goal_y_)
                .Subscribe(_ => {
                    var pos = transform.position;
                    pos.y = target_.position.y + 1.0f;
                    transform.position = pos;
                });
        }
    }
}
