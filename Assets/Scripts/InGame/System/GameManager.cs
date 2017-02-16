using UnityEngine;
using UniRx;

namespace ingame.system {
    public class GameManager : MonoBehaviour {

        [SerializeField]
        private ingame.player.PlayerAction player_action_;

        private Subject<NextStep> turn_step_;

        private void Awake() {
            turn_step_ = new Subject<NextStep>();
        }

        //行動は移動かアクション(攻撃やアイテムを使用など)に分けられる
        private void Start() {
            turn_step_.Where(step => step == NextStep.Player)
                      .Subscribe(_ => {
                          player_action_.Action((next_step) => turn_step_.OnNext(next_step));
                      })
                      .AddTo(this);

            turn_step_.Where(step => step == NextStep.EnemyMove)
                      .Subscribe(_ => {
                          Debug.Log("敵の移動");
                          turn_step_.OnNext(NextStep.Player);
                      } /*敵の移動*/)
                      .AddTo(this);

            turn_step_.Where(step => step == NextStep.EnemyAct)
                      .Subscribe(_ => {
                      } /*敵の行動*/)
                      .AddTo(this);

            turn_step_.OnNext(NextStep.Player);
        }

    }
    //敵はプレイヤーの行動によって移動が先か行動が先か決まる
    public enum NextStep {
        Player,
        EnemyMove,
        EnemyAct
    };
}