using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace ingame.stage {
    [RequireComponent(typeof(StageCreater))]
    public class StageManager : MonoBehaviour {

        [SerializeField]
        private Slider slide_;

        [SerializeField]
        private Transform player_;

        private StageCreater creater_;

        private float goal_y_ = 50f;

        private void Awake() {
            creater_ = GetComponent<StageCreater>();
        }

        private void Start() {
            slide_.minValue = 0;
            slide_.maxValue = goal_y_;
            creater_.StageCreate(goal_y_);

            player_.ObserveEveryValueChanged(transform => transform.position.y)
                   .Subscribe(pos_y => slide_.value = pos_y)
                   .AddTo(this);
        }

    }
}
