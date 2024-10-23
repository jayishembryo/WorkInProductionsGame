using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private EventReference TheStyxCalls;

    private EventInstance musicInstance;

    private void Start()
    {
        StartMusic();
    }

    private void StartMusic()
    {
        musicInstance = RuntimeManager.CreateInstance(TheStyxCalls);
        musicInstance.start();
    }

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}
