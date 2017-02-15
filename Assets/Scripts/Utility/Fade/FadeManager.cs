using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * 使い方
 * シーンのManagerがあればManager,なければそれに相当するもののスクリプトのAwakeにて
 * utility.fade.FadeManager.Init()
 * を呼んでください。
 * 
 *  FadeIn (1秒でフェードし、完了後にActionを呼ぶ)
 *  utility.fade.FadeManager.FadeIn(1.0f, Action);
 * 
 *  FadeOut (1秒でフェードし、完了後にActionを呼ぶ)
 *  utility.fade.FadeManager.FadeOut(1.0f, Action);
 * 
 *  FadeInOut (1秒でフェードインし、titleにシーン遷移した後、 完了後にActionを呼ぶ)
 *  utility.fade.FadeManager.FadeInOut(1.0f, title, Action)
 */
namespace utility.fade {
    public static class FadeManager {

        private const int reference_resolution_x_ = 1600;
        private const int reference_resolution_y_ = 900;

        private static MonoBehaviour mono_behaviour_ = null;
        private static GameObject fade_manager_ = null;
        private static Image fade_image_ = null;

        public static void Init() {

            var obj = GameObject.Find("FadeManager");

            if (obj == null) {
                fade_manager_ = new GameObject("FadeManager");
                Object.DontDestroyOnLoad(fade_manager_);
                mono_behaviour_ = fade_manager_.AddComponent<GetMonoBehaviour>().mono_behaviour_;
                CreateCanvas();
                Fade.Init(mono_behaviour_, fade_image_);
                Debug.Log("<color=green> FadeManager is initialized!!! </color>");
            }
        }

        private static void CreateCanvas() {
            Canvas canvas = fade_manager_.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 99;

            CanvasScalerSetting();

            fade_manager_.AddComponent<GraphicRaycaster>();

            FadeImageSetting();
        }

        private static void CanvasScalerSetting() {
            CanvasScaler scaler = fade_manager_.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(reference_resolution_x_, reference_resolution_y_);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }

        private static void FadeImageSetting() {
            GameObject ui_image = new GameObject("FadeImage");
            ui_image.transform.SetParent(fade_manager_.transform, false);

            fade_image_ = ui_image.AddComponent<Image>();
            fade_image_.color = new Color(0, 0, 0, 0);

            RectTransform transform = ui_image.transform as RectTransform;

            transform.anchorMin = new Vector2(0, 0);
            transform.anchorMax = new Vector2(1, 1);
        }
    }
}

