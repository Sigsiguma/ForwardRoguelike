using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.camera {
    public class FollowCamera : MonoBehaviour {

        [SerializeField]
        private Transform target_;

        private void Start() {
            this.LateUpdateAsObservable()
                .Where(_ => ingame.system.GameManager.Instance.TurnStep.Value != ingame.system.NextStep.None)
                .Subscribe(_ => {
                    var pos = transform.position;
                    pos.y = target_.position.y + 1.0f;
                    transform.position = pos;
                });
        }
    }
}
