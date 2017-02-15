using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace utility.pool {
    public class ObjectPool {
        private readonly int max_object_num_;
        private GameObject cathe_prefab_;
        private List<GameObject> object_list_;

        public ObjectPool() {
            max_object_num_ = 0;
            cathe_prefab_ = null;
            object_list_ = null;
        }

        public ObjectPool(GameObject cathe_prefab, int max_object_num) {
            max_object_num_ = max_object_num;
            cathe_prefab_ = cathe_prefab;
            object_list_ = new List<GameObject>();
        }


        public GameObject GetInstance(System.Action<Transform> InitTransform = null, Transform parent = null) {

            Debug.Assert(object_list_ != null, "ObjectList is null!!!");

            for (int i = 0; i < object_list_.Count; ++i) {
                if (!object_list_[i].activeSelf) {
                    object_list_[i].SetActive(true);
                    InitTransform(object_list_[i].transform);
                    return object_list_[i];
                }
            }

            if (max_object_num_ > object_list_.Count) {
                GameObject obj = Object.Instantiate(cathe_prefab_);
                obj.SetActive(true);
                obj.transform.parent = parent;
                object_list_.Add(obj);
                InitTransform(obj.transform);
                return obj;
            }

            Debug.LogWarning("ObjectPool is max!!!");
            return null;
        }

        public GameObject GetInstance(Vector3 pos, Transform parent = null) {

            Debug.Assert(object_list_ != null, "ObjectList is null!!!");

            for (int i = 0; i < object_list_.Count; ++i) {
                if (!object_list_[i].activeSelf) {
                    object_list_[i].SetActive(true);
                    object_list_[i].transform.position = pos;
                    return object_list_[i];
                }
            }

            if (max_object_num_ > object_list_.Count) {
                GameObject obj = Object.Instantiate(cathe_prefab_);
                obj.SetActive(true);
                obj.transform.parent = parent;
                object_list_.Add(obj);
                obj.transform.position = pos;
                return obj;
            }

            Debug.LogWarning("ObjectPool is max!!!");
            return null;
        }

        public int GetActiveObjectNum() {

            int count = 0;

            for (int i = 0; i < object_list_.Count; ++i) {
                if (object_list_[i].activeSelf) {
                    ++count;
                }
            }

            return count;
        }

    }
}