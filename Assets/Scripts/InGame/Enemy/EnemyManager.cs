using UnityEngine;
using System.Collections.Generic;

namespace ingame.enemy {
    public class EnemyManager : MonoBehaviour {

        private List<EnemyAction> enemy_list_;
        private List<EnemyAction> attack_list_;
        private List<EnemyAction> move_list_;

        private void Awake() {
            enemy_list_ = new List<EnemyAction>();
            attack_list_ = new List<EnemyAction>();
            move_list_ = new List<EnemyAction>();
        }

        private void EnemysAction() {

        }

        public void AddList(EnemyAction action) {

            if (action == null) return;

            enemy_list_.Add(action);
        }
    }
}
