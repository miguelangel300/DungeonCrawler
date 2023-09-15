using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.SoundManagerNamespace;


public class AudioManager : MonoBehaviour
{

	public static AudioManager Instance = null;
	
	// Initialize the singleton instance.
	

		/*public Slider SoundSlider;
        public Slider MusicSlider;
        public InputField SoundCountTextBox;
        public Toggle PersistToggle;*/

        public List<AudioSource> SoundAudioSources = new List<AudioSource>();
		public GameObject[] cajaDeSonidos;
		public GameObject[] cajaDeMusicas;
		public List<AudioSource> MusicAudioSources = new List<AudioSource>();

		public AudioClip[] clips;
		public AudioClip[] musicas;

		int indexGlobal;


	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);

	}


	private void Start() 
	{ 
		SoundManager.StopSoundsOnLevelLoad = !true;
		for (int i = 0; i < clips.Length; i++)
		{
			cajaDeSonidos[i].AddComponent<AudioSource>();
			SoundAudioSources.Add(cajaDeSonidos[i].GetComponent<AudioSource>());
			SoundAudioSources[i].clip = clips[i];
		}


		for (int i = 0; i < musicas.Length; i++)
		{
			cajaDeMusicas[i].AddComponent<AudioSource>();
			MusicAudioSources.Add(cajaDeMusicas[i].GetComponent<AudioSource>());
			MusicAudioSources[i].clip = musicas[i];
		}

	}


        public void PlaySound(int index)
        {
			indexGlobal = index;
            int count;
            /*if (!int.TryParse(SoundCountTextBox.text, out count))
            {
                count = 1;
            }*/
            //while (count-- > 0)
            //{
                /*SoundAudioSources[0].PlayOneShotSoundManaged(clips[index]);
				Debug.Log("está sonando " + clips[index].name);*/

				SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
				/*Debug.Log("está sonando " + clips[index].name);*/
            //}
        }

        public void PlayMusic(int index)
        {
            MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, true);
        }




        /*public void SoundVolumeChanged()
        {
            SoundManager.SoundVolume = SoundSlider.value;
        }

        public void MusicVolumeChanged()
        {
            SoundManager.MusicVolume = MusicSlider.value;
        }

        public void PersistToggleChanged(bool isOn)
        {
            SoundManager.StopSoundsOnLevelLoad = !isOn;
        }*/



		public void StopAll()
		{
			SoundManager.StopAll();
		}

		public bool Get_IsPlaying()
		{
			//if (SoundAudioSources.clip.length == 0) return false;
			if (SoundAudioSources[indexGlobal].clip == null) return false;
			return SoundAudioSources[indexGlobal].isPlaying;
		}


}



