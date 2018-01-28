public class SoundPlayer : Singleton<SoundPlayer>
{
    public void Awake()
    {
        this.PostEvent(SoundEvent.Music);
    }

    public void PostEvent(SoundEvent soundEvent)
    {
        UnityEngine.Transform t = this.gameObject.transform.Find(soundEvent.ToString());
        if (t == null)
        {
            UnityEngine.Debug.LogError("Cannot play SoundEvent " + soundEvent);
            return;
        }

        UnityEngine.AudioSource audioSource = t.gameObject.GetComponent<UnityEngine.AudioSource>();
        if (audioSource == null)
        {
            UnityEngine.Debug.LogError("Cannot play SoundEvent " + soundEvent);
            return;
        }

        audioSource.Play();
    }
}