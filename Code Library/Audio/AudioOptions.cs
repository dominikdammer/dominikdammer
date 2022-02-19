/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using UnityEngine;
using UnityEngine.UI;

//using FMOD
public class AudioOptions : MonoBehaviour
{
    #region serialized fields

    [SerializeField]
    private Slider masterSlider;

    [SerializeField]
    private Slider musicSlider;


    #endregion serialized fields

    #region fields

    private FMOD.Studio.Bus masterBus;

    private FMOD.Studio.Bus musicBus;


	#endregion fields

	#region initialization and shutdown

	private void OnEnable ()
	{
        GetBusses ();
        SetVolumes ();
	}

    /// <summary>
    /// Initial set of the fmod busses
    /// </summary>
	private void GetBusses ()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus ("bus:/");
        musicBus = FMODUnity.RuntimeManager.GetBus ("bus:/Music");
    }

    /// <summary>
    /// initial set of the volume values (bus and slider)
    /// PlayerPrefs.GetFloat ---> 0.5f is the default value if never one was setted
    /// </summary>
    private void SetVolumes ()
    {
        float masterVolume = PlayerPrefs.GetFloat ("savedMasterVolume", 0.5f);
        masterBus.setVolume (SliderToBusVolume(masterVolume));
        masterSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat ("savedMusicVolume", 0.5f);
        musicBus.setVolume (SliderToBusVolume (musicVolume));
        musicSlider.value = musicVolume;
    }

    #endregion initialization and shutdown

    #region handling

    /// <summary>
    /// On value change method for the master slider
    /// </summary>
    public void SetMasterLevel (float volume)
	{
        Debug.LogError(SliderToBusVolume(volume));
        masterBus.setVolume (SliderToBusVolume (volume));
        PlayerPrefs.SetFloat ("savedMasterVolume", volume);
    }

    /// <summary>
    /// On value change method for the music slider
    /// </summary>
    public void SetMusicLevel (float volume)
    {
        Debug.LogError(SliderToBusVolume(volume));
        musicBus.setVolume (SliderToBusVolume (volume));
        PlayerPrefs.SetFloat ("savedMusicVolume", volume);
    }



    #endregion handling

    #region utils

    /// <summary>
    /// Converts slider value (0.0f - 1.0f) to volume value of fmod
    /// </summary>
    private float SliderToBusVolume(float volume) => volume; //Mathf.Pow (10.0f, volume / 20f);

    #endregion utils

}