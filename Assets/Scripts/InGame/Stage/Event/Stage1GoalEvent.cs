using UnityEngine;
using System.Collections;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

namespace ingame.stage.events {
    public class Stage1GoalEvent : MonoBehaviour {

        private const float walk_time_ = 2.0f;
        private const string goal_tag_name_ = "Goal";
        private GameObject player_;
        private Vector3 goal_pos_;
        private Animator emotion_animator_;
        private int find_id_;


        private void Start() {
            goal_pos_ = GameObject.FindWithTag(goal_tag_name_).transform.position;
            player_ = GameObject.FindWithTag("Player");
            emotion_animator_ = player_.transform.FindChild("Emotion").GetComponent<Animator>();
            find_id_ = Animator.StringToHash("Find");

            player_.OnTriggerEnter2DAsObservable()
                   .Where(obj => obj.tag == "GoalEntrance")
                   .Subscribe(_ => StartCoroutine(EventStart()));

            player_.OnTriggerEnter2DAsObservable()
                   .Where(obj => obj.tag == goal_tag_name_)
                   .Subscribe(_ => Debug.Log("Clear"));
        }

        private IEnumerator EventStart() {

            yield return EmotionWait();
            player_.transform.DOMove(goal_pos_, walk_time_).SetEase(Ease.Linear);
        }

        private IEnumerator EmotionWait() {

            emotion_animator_.SetTrigger(find_id_);
            yield return null;

            yield return new WaitForAnimation(emotion_animator_, 0);
        }

    }
}
