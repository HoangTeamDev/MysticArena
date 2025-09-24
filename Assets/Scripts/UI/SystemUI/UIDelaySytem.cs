using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace UIScripts.SystemUI
{
    /// <summary>
    /// Hệ thống delay tiện ích dùng UniTask cho các thao tác UI bất đồng bộ.
    /// </summary>
    public static class UIDelaySystem
    {
        
        public static async UniTask WaitUntil(Func<bool> predicate, Action onComplete, MonoBehaviour context)
        {
            await WaitUntil(predicate, context);
            onComplete?.Invoke();
        }
        /// <summary>
        /// Chờ trong một khoảng thời gian (theo giây).
        /// </summary>
        public static async UniTask Delay(float seconds, MonoBehaviour context)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, context.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// Lặp lại hành động <paramref name="count"/> lần, mỗi lần cách nhau <paramref name="seconds"/> giây.
        /// </summary>
        public static async UniTask Repeat(float seconds, int count, Func<int, UniTask> action, MonoBehaviour context)
        {
            for (int i = 0; i < count; i++)
            {
                await Delay(seconds, context);
                await action(i);
            }
        }

        /// <summary>
        /// Thực hiện hiệu ứng Fade bằng DOTween rồi chờ cho hoàn tất.
        /// </summary>
     /*   public static async UniTask Fade(CanvasGroup canvasGroup, float toAlpha, float duration, MonoBehaviour context)
        {
           // var tween = canvasGroup.DOFade(toAlpha, duration);
            await tween.AsyncWaitForCompletion(); // ✅ đúng cách
        }*/
        /// <summary>
        /// Chờ đến khi điều kiện trả về true.
        /// </summary>
        public static async UniTask WaitUntil(Func<bool> predicate, MonoBehaviour context)
        {
            await UniTask.WaitUntil(predicate, PlayerLoopTiming.Update, context.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// Chờ cho đến khi một UniTask khác hoàn thành.
        /// </summary>
        public static async UniTask WaitTask(UniTask task, MonoBehaviour context)
        {
            await task.AttachExternalCancellation(context.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// Delay sau đó bật/tắt GameObject.
        /// </summary>
        public static async UniTask DelaySetActive(GameObject target, bool active, float delay, MonoBehaviour context)
        {
            await Delay(delay, context);
            if (target != null) target.SetActive(active);
        }

        /// <summary>
        /// Delay sau đó thay đổi trạng thái tương tác của UI element.
        /// </summary>
        public static async UniTask DelaySetInteractable(UnityEngine.UI.Selectable element, bool interactable, float delay, MonoBehaviour context)
        {
            await Delay(delay, context);
            if (element != null) element.interactable = interactable;
        }

        /// <summary>
        /// Delay sau đó đổi nội dung văn bản của Text.
        /// </summary>
        public static async UniTask DelaySetText(UnityEngine.UI.Text text, string content, float delay, MonoBehaviour context)
        {
            await Delay(delay, context);
            if (text != null) text.text = content;
        }

        /// <summary>
        /// Chờ đến khi CanvasGroup có thể sử dụng được, sau đó thực hiện Tween.
        /// </summary>
       /* public static async UniTask WaitThenTween(CanvasGroup canvasGroup, float targetAlpha, float duration, MonoBehaviour context)
        {
            await WaitUntil(() => canvasGroup != null && canvasGroup.gameObject.activeInHierarchy, context);
            await Fade(canvasGroup, targetAlpha, duration, context);
        }*/

        /// <summary>
        /// Thực hiện nhiều bước delay liên tiếp, mỗi bước có thời gian và hành động riêng.
        /// </summary>
        public static async UniTask SequentialDelay((float delay, Func<UniTask> step)[] steps, MonoBehaviour context)
        {
            foreach (var (delay, step) in steps)
            {
                await Delay(delay, context);
                await step();
            }
        }

        /// <summary>
        /// Chờ điều kiện đúng hoặc timeout (trả về true nếu predicate đúng trước timeout, ngược lại false).
        /// </summary>
        public static async UniTask<bool> WaitUntilOrTimeout(Func<bool> predicate, float timeout, MonoBehaviour context)
        {
            var token = context.GetCancellationTokenOnDestroy();
            var predicateTask = UniTask.WaitUntil(predicate, PlayerLoopTiming.Update, token);
            var timeoutTask = UniTask.Delay(TimeSpan.FromSeconds(timeout), DelayType.DeltaTime, PlayerLoopTiming.Update, token);
            return await UniTask.WhenAny(predicateTask, timeoutTask) == 0;
        }

        /// <summary>
        /// Chờ trong thời gian nhất định, có thể bị hủy bởi CancellationToken bên ngoài.
        /// </summary>
        public static async UniTask DelayWithCancel(float seconds, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);
        }

        /// <summary>
        /// Chờ cho đến khi animation với tên chỉ định kết thúc.
        /// </summary>
        public static async UniTask WaitAnimationEnd(Animator animator, string stateName, MonoBehaviour context)
        {
            var token = context.GetCancellationTokenOnDestroy();
            await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName), cancellationToken: token);
            await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f, cancellationToken: token);
        }
    }
}
