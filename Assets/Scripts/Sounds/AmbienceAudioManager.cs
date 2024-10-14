using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbienceAudioManager : MonoBehaviour
{
    [SerializeField]
    private EventReference AmbienceSounds; 

    private EventInstance soundInstance;

    private void Awake()
    {
        // Create an FMOD Event Instance based on the provided event reference
        soundInstance = RuntimeManager.CreateInstance(AmbienceSounds);

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
        // Stops the instance but keeps it loaded in case you need it later
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void OnDestroy()
    {
        // Proper cleanup of FMOD instance when object is destroyed
        soundInstance.release();
    }
}
