using UnityEngine;
using UnityEngine.Playables;
using DonBosco;

[RequireComponent(typeof(PlayableDirector))]
[RequireComponent(typeof(SceneLoaderAgent))]
public class TimelineSceneLoader : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private SceneLoaderAgent sceneLoaderAgent;

    void Awake()
    {
        // Auto assign kalau belum di-set
        if (playableDirector == null)
            playableDirector = GetComponent<PlayableDirector>();
        if (sceneLoaderAgent == null)
            sceneLoaderAgent = GetComponent<SceneLoaderAgent>();
    }

    void Start()
    {
        playableDirector.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            sceneLoaderAgent.ExecuteLoadScene();
        }
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnTimelineStopped;
    }
}
