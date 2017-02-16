using UnityEngine;
using UniRx;

namespace ingame.system {
    public class GameManager : SingletonMonoBehaviour<GameManager> {

        [SerializeField]
        private ingame.player.PlayerAction player_action_;

        private ReactiveProperty<NextStep> turn_step_;

        public ReadOnlyReactiveProperty<NextStep> TurnStep { get { return turn_step_.ToReadOnlyReactiveProperty(); } }

        private new void Awake() {
            turn_step_ = new ReactiveProperty<NextStep>(NextStep.Player);
        }

        //行動は移動かアクション(攻撃やアイテムを使用など)に分けられる
        private void Start() {

            player_action_.Action((next_step) => turn_step_.Value = next_step);

            turn_step_.Where(step => step == NextStep.EnemyMove)
                          .Subscribe(_ => {
                              Debug.Log("敵の移動");
                              turn_step_.Value = NextStep.Player;
                          } /*敵の移動*/)
                          .AddTo(this);

            turn_step_.Where(step => step == NextStep.EnemyAct)
                          .Subscribe(_ => {
                              Debug.Log("敵の行動");
                              turn_step_.Value = NextStep.Player;
                          } /*敵の行動*/)
                          .AddTo(this);
        }

    }
    //敵はプレイヤーの行動によって移動が先か行動が先か決まる
    public enum NextStep {
        Player,
        EnemyMove,
        EnemyAct
    };
}