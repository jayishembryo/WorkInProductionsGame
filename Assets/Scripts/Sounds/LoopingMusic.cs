using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LoopingMusic : MonoBehaviour
{
    [SerializeField]
    private EventReference DefaultMusic;

    private EventInstance soundInstance;

    private void Awake()
    {
        // Create an FMOD Event Instance based on the provided event reference
        soundInstance = RuntimeManager.CreateInstance(DefaultMusic);

        PlaySoundLoop();
    }

    private void Update()
    {
        UpdateSoundPosition();
    }

    private void UpdateSoundPosition()
    {
        var attributes = RuntimeUtils.To3DAttributes(gameObject);
        soundInstance.set3DAttributes(attributes);
    }

    private void PlaySoundLoop()
    {
        // Start playing the sound instance
        soundInstance.start();
    }

    public void StopSound()
    {
        // Just in Case
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void OnDestroy()
    {
        soundInstance.release();
    }
}
