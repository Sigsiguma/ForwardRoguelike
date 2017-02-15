using UnityEngine;
using System.Collections;

/*
 * 使い方
 * タイマーを使用したい場所にて、
 * var stop_watch_ =  new utility.stopwatch.StopWatch();
 * でタイマーを作成して下さい
 * 
 *  60秒カウントした後、Actionを呼ぶ
 *  stop_watch_.CountDownStart(60, Action);
 */
namespace utility.stopwatch {
    public class StopWatch {

        private bool is_pause_ = false;
        private IEnumerator coroutine_ = null;
        private MonoBehaviour mono_behaviour_ = null;

        public int CurrentTime_ { get; private set; }


        public StopWatch(MonoBehaviour mono_behaviour) {
            mono_behaviour_ = mono_behaviour;
        }

        public void Release() {
            coroutine_ = null;
            mono_behaviour_ = null;
        }

        public void CountDownStart(int start_time, System.Action onFinish = null) {

            if (coroutine_ != null) {
                Debug.LogWarning("Coroutine is already start!");
                return;
            }

            coroutine_ = CountDownCoroutine(start_time, onFinish);
            mono_behaviour_.StartCoroutine(coroutine_);
        }

        public void Pause() {

            if (coroutine_ == null) {
                Debug.LogWarning("Coroutine is null!");
                return;
            }

            if (is_pause_) {
                Debug.LogWarning("Timer is already paused!!");
                return;
            }

            is_pause_ = true;
            mono_behaviour_.StopCoroutine(coroutine_);
        }

        public void Resume() {

            if (coroutine_ == null) {
                Debug.LogWarning("Coroutine is null!");
                return;
            }

            if (!is_pause_) {
                Debug.LogWarning("Timer is not pause!!");
                return;
            }

            is_pause_ = false;
            mono_behaviour_.StartCoroutine(coroutine_);
        }

        private IEnumerator CountDownCoroutine(int start_time, System.Action onFinish = null) {

            CurrentTime_ = start_time;

            while (CurrentTime_ > 0) {
                yield return new WaitForSeconds(1);

                if (!is_pause_) {
                    --CurrentTime_;
                }

            }

            if (onFinish != null) {
                onFinish();
            }
        }

    }
}

