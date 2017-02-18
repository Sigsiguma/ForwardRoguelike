using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.player {
    public partial class PlayerAction : MonoBehaviour {

        public IObservable<PlayerDir> PlayerDirAsObservable { get { return PlayerDir_.AsObservable(); } }

        private Subject<PlayerDir> PlayerDir_;

        private const float max_flick_time_ = 0.5f;
        private const float flick_left_border_ = -100f;
        private const float flick_right_border_ = 100f;
        private const float tap_flick_border_ = 100f;
        private const float ray_length_ = 0.5f;

        private void Awake() {
            obstacle_mask_ = LayerMask.GetMask(new string[] { "Wall", "Enemy" });
            enemy_mask_ = LayerMask.GetMask(new string[] { "Enemy" });
            PlayerDir_ = new Subject<PlayerDir>();
            PlayerMoved = new Subject<Unit>();
        }

        public void Action(System.Action<ingame.system.NextStep> onNext) {

            var MoveAsObservable = ActionAsObservable().Where(dir => CheckCanMove(dir));
            var AttackAsObservable = ActionAsObservable().Where(dir => CheckCanAttack(dir));

            MoveAsObservable.Where(_ => ingame.system.GameManager.Instance.TurnStep.Value == ingame.system.NextStep.Player)
                            .Subscribe(dir => {
                                Move(dir);
                                Debug.LogError("Move");
                                onNext(ingame.system.NextStep.EnemyMove);
                            });


            AttackAsObservable.Where(_ => ingame.system.GameManager.Instance.TurnStep.Value == ingame.system.NextStep.Player)
                            .Subscribe(dir => {
                                Attack(dir);
                                Debug.LogError("Attack");
                                onNext(ingame.system.NextStep.EnemyAct);
                            });

            var pointer_event = gameObject.AddComponent<ObservablePointerClickTrigger>();

            pointer_event.OnPointerClickAsObservable()
                         .Where(_ => ingame.system.GameManager.Instance.TurnStep.Value == ingame.system.NextStep.Player)
                         .Subscribe(_ => {
                             Debug.Log("None");
                             onNext(ingame.system.NextStep.EnemyAct);
                         });
        }

        private IObservable<PlayerDir> ActionAsObservable() {
            return FlickVectorAsObservable().Where(vec => vec.magnitude > tap_flick_border_)
                                            .Select(flick_vec => AssortFlickDir(flick_vec))
                                            .Do(dir => PlayerDir_.OnNext(dir));
        }

        private PlayerDir AssortFlickDir(Vector3 flick_vec) {

            if (flick_vec.x < flick_left_border_) {
                return PlayerDir.Left;
            } else if (flick_vec.x > flick_right_border_) {
                return PlayerDir.Right;
            } else {
                return PlayerDir.Front;
            }
        }

        private IObservable<Vector3> FlickVectorAsObservable() {
            var start = this.UpdateAsObservable()
                            .Where(_ => Input.GetMouseButtonDown(0))
                            .Select(_ => Input.mousePosition);

            var end = this.UpdateAsObservable()
                          .TakeUntil(Observable.Timer(System.TimeSpan.FromSeconds(max_flick_time_)))
                          .Where(_ => Input.GetMouseButtonUp(0))
                          .Select(_ => Input.mousePosition)
                          .FirstOrDefault();

            return start.SelectMany(start_pos => end.Select(end_pos => end_pos - start_pos));
        }
    }

    public enum PlayerDir {
        Left,
        Front,
        Right
    };
}
