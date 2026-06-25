using UnityEngine;

public class HitPause : MonoBehaviour
{
  private static HitPause instance;

  private float remainingPauseTime;
  private float originalTimeScale = 1f;
  private bool isPaused;

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  private static void Bootstrap()
  {
    if (instance != null) return;

    GameObject go = new GameObject("[HitPause]");
    instance = go.AddComponent<HitPause>();
    DontDestroyOnLoad(go);
  }

  public static void Request(float duration)
  {
    if (instance == null) return;
    instance.StartPause(duration);
  }

  private void StartPause(float duration)
  {
    if (!isPaused)
    {
      originalTimeScale = Time.timeScale;
      Time.timeScale = 0f;
      isPaused = true;
      remainingPauseTime = duration;
    }
    else
    {
        remainingPauseTime = Mathf.Max(remainingPauseTime, duration);
    }
  }
  
  private void Update()
  {
    if (!isPaused) return;
      
    remainingPauseTime -= Time.unscaledDeltaTime;

    if (remainingPauseTime <= 0f)
    {
      Time.timeScale = originalTimeScale;
      isPaused = false;
    }
  }
}
