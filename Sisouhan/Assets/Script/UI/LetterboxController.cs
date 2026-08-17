using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.UI
{
    /// <summary>
    /// Keeps every screen-space UI canvas inside a 16:9 frame and fills the
    /// unused screen area with black letterboxing.
    /// </summary>
    public sealed class LetterboxController : MonoBehaviour
    {
        private const float TargetAspect = 16f / 9f;
        private const string ContentRootName = "LetterboxContent";

        private static LetterboxController _instance;

        private Canvas _barCanvas;
        private RectTransform _leftBar;
        private RectTransform _rightBar;
        private RectTransform _topBar;
        private RectTransform _bottomBar;
        private int _lastWidth;
        private int _lastHeight;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateController()
        {
            if (FindFirstObjectByType<LetterboxController>() != null)
                return;

            GameObject controllerObject = new GameObject(nameof(LetterboxController));
            controllerObject.AddComponent<LetterboxController>();
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            CreateBars();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            ApplyLetterboxing();
        }

        private void Update()
        {
            if (_lastWidth == Screen.width && _lastHeight == Screen.height)
                return;

            UpdateBars();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (_instance == this)
                _instance = null;
        }

        private void OnSceneLoaded(Scene _, LoadSceneMode __)
        {
            StartCoroutine(ApplyAfterSceneLoad());
        }

        private IEnumerator ApplyAfterSceneLoad()
        {
            yield return null;
            ApplyLetterboxing();
        }

        private void ApplyLetterboxing()
        {
            Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (Canvas canvas in canvases)
            {
                if (canvas == _barCanvas || !canvas.isRootCanvas || canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                    continue;

                CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
                if (scaler == null)
                    continue;

                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920f, 1080f);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

                CreateContentFrame(canvas.transform as RectTransform);
            }

            UpdateBars();
        }

        private void CreateContentFrame(RectTransform canvasTransform)
        {
            if (canvasTransform == null || canvasTransform.Find(ContentRootName) != null)
                return;

            List<Transform> children = new List<Transform>();
            for (int i = 0; i < canvasTransform.childCount; i++)
            {
                children.Add(canvasTransform.GetChild(i));
            }

            GameObject frameObject = new GameObject(ContentRootName, typeof(RectTransform), typeof(AspectRatioFitter));
            RectTransform frame = frameObject.GetComponent<RectTransform>();
            frame.SetParent(canvasTransform, false);
            frame.anchorMin = new Vector2(0.5f, 0.5f);
            frame.anchorMax = new Vector2(0.5f, 0.5f);
            frame.pivot = new Vector2(0.5f, 0.5f);
            frame.anchoredPosition = Vector2.zero;

            AspectRatioFitter fitter = frameObject.GetComponent<AspectRatioFitter>();
            fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            fitter.aspectRatio = TargetAspect;

            foreach (Transform child in children)
            {
                child.SetParent(frame, false);
            }
        }

        private void CreateBars()
        {
            GameObject canvasObject = new GameObject("Letterbox Bars", typeof(RectTransform), typeof(Canvas));
            canvasObject.transform.SetParent(transform, false);

            _barCanvas = canvasObject.GetComponent<Canvas>();
            _barCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _barCanvas.overrideSorting = true;
            _barCanvas.sortingOrder = 32767;

            _leftBar = CreateBar("Left");
            _rightBar = CreateBar("Right");
            _topBar = CreateBar("Top");
            _bottomBar = CreateBar("Bottom");
        }

        private RectTransform CreateBar(string barName)
        {
            GameObject barObject = new GameObject(barName, typeof(RectTransform), typeof(Image));
            RectTransform bar = barObject.GetComponent<RectTransform>();
            bar.SetParent(_barCanvas.transform, false);

            Image image = barObject.GetComponent<Image>();
            image.color = Color.black;
            image.raycastTarget = false;
            return bar;
        }

        private void UpdateBars()
        {
            _lastWidth = Screen.width;
            _lastHeight = Screen.height;

            if (_lastWidth <= 0 || _lastHeight <= 0)
                return;

            float screenAspect = (float)_lastWidth / _lastHeight;

            if (screenAspect > TargetAspect)
            {
                float contentWidth = TargetAspect / screenAspect;
                float sideWidth = (1f - contentWidth) * 0.5f;

                SetBar(_leftBar, 0f, 0f, sideWidth, 1f);
                SetBar(_rightBar, 1f - sideWidth, 0f, 1f, 1f);
                SetBar(_topBar, 0f, 0f, 0f, 0f);
                SetBar(_bottomBar, 0f, 0f, 0f, 0f);
            }
            else if (screenAspect < TargetAspect)
            {
                float contentHeight = screenAspect / TargetAspect;
                float sideHeight = (1f - contentHeight) * 0.5f;

                SetBar(_leftBar, 0f, 0f, 0f, 0f);
                SetBar(_rightBar, 0f, 0f, 0f, 0f);
                SetBar(_topBar, 0f, 1f - sideHeight, 1f, 1f);
                SetBar(_bottomBar, 0f, 0f, 1f, sideHeight);
            }
            else
            {
                SetBar(_leftBar, 0f, 0f, 0f, 0f);
                SetBar(_rightBar, 0f, 0f, 0f, 0f);
                SetBar(_topBar, 0f, 0f, 0f, 0f);
                SetBar(_bottomBar, 0f, 0f, 0f, 0f);
            }
        }

        private static void SetBar(RectTransform bar, float minX, float minY, float maxX, float maxY)
        {
            bool isVisible = maxX > minX && maxY > minY;
            bar.gameObject.SetActive(isVisible);

            if (!isVisible)
                return;

            bar.anchorMin = new Vector2(minX, minY);
            bar.anchorMax = new Vector2(maxX, maxY);
            bar.offsetMin = Vector2.zero;
            bar.offsetMax = Vector2.zero;
        }
    }
}
