using UnityEngine;
using System.Collections.Generic;
using UniRx;
using ingame.enemy;

namespace ingame.stage {
    public class StageCreater : MonoBehaviour {

        [SerializeField]
        private GameObject[] tiled_prefab_;

        [SerializeField]
        private GameObject[] enemy_prefab_;

        [SerializeField]
        private ingame.player.PlayerAction player_;

        [SerializeField]
        private EnemyManager enemys_;

        private const float tile_size_ = 0.5f;
        private List<GameObject> tiled_list_;

        private void Awake() {
            tiled_list_ = new List<GameObject>();
        }

        public void StageCreate(float max_y) {
            StageInit();
            player_.PlayerMovedAsObservable
                   .Where(_ => player_.transform.position.y < max_y)
                   .Subscribe(_ => StageUpdate());
        }

        private void StageInit() {
            float min_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane)).y - tile_size_;
            float max_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane)).y + tile_size_;

            int tile_num = (int)((max_pos_y - min_pos_y) / tile_size_);

            for (int i = 0; i <= tile_num; ++i) {
                tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)],
                 new Vector3(0f, min_pos_y + tile_size_ * i, 0f), Quaternion.identity, transform));
            }
        }

        private void StageUpdate() {

            Destroy(tiled_list_[0]);
            tiled_list_.Remove(tiled_list_[0]);

            float next_tile_pos = tiled_list_[tiled_list_.Count - 1].transform.position.y + tile_size_;
            tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)], new Vector3(0f, next_tile_pos, 0f)
                            , Quaternion.identity, transform));

            CreateEnemy(next_tile_pos);
        }

        private void CreateEnemy(float pos_y) {
            if (Random.Range(0, 100) >= 30) {
                return;
            }

            float pos_x = DecideEnemyPosX();
            var enemy = Instantiate(enemy_prefab_[Random.Range(0, enemy_prefab_.Length)], new Vector3(pos_x, pos_y, 0f),
                                     Quaternion.identity) as GameObject;

            enemys_.AddList(enemy.GetComponent<EnemyAction>());
        }

        private float DecideEnemyPosX() {
            float[] pos_list = new float[] { -tile_size_ * 2, -tile_size_, 0, tile_size_, tile_size_ * 2 };
            return pos_list[Random.Range(0, pos_list.Length)];
        }

    }
}