using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.camera {
    public class FollowCamera : MonoBehaviour {

        [SerializeField]
        private Transform target_;

        private void Start() {
            this.LateUpdateAsObservable()
                .Subscribe(_ => {
                    var pos = transform.position;
                    pos.y = target_.position.y + 1.0f;
                    transform.position = pos;
                });
        }
    }
}
