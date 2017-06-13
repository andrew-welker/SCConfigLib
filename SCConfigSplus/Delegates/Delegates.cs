using SCConfigSPlus.JSON;

namespace SCConfigSPlus.Delegates
{
    /// <summary>
    /// Delegate for S+ OnConfigurationChanged event for a list object
    /// </summary>
    /// <param name="index">Index in the list</param>
    /// <param name="source">Object</param>
    public delegate void SourcesConfigChangedDel(ushort index, Source source);

    /// <summary>
    /// Delegate for S+ OnConfigurationChanged event for a list object
    /// </summary>
    /// <param name="index">Index in the list</param>
    /// <param name="source">Object</param>
    public delegate void CamerasConfigChangedDel(ushort index, Camera camera);

    /// <summary>
    /// Delegate for S+ OnConfigurationChanged event for a list object
    /// </summary>
    /// <param name="index">Index in the list</param>
    /// <param name="source">Object</param>
    public delegate void DisplaysConfigChangedDel(ushort index, Display display);

    /// <summary>
    /// Delegate for S+ for OnConfigurationChanged event.
    /// </summary>
    /// <param name="sender">Object that triggered the event</param>
    /// <param name="config">Config to send to S+</param>
    public delegate void EnvControlsConfigChangedDel(object sender, EnvironmentControls config);

    /// <summary>
    /// Delegate for S+ for OnConfigurationChanged event.
    /// </summary>
    /// <param name="sender">Object that triggered the event</param>
    /// <param name="config">Config to send to S+</param>
    public delegate void DspControlsConfigChangedDel(object sender, Dsp config);

    /// <summary>
    /// Delegate for S+ for OnConfigurationChanged event.
    /// </summary>
    /// <param name="sender">Object that triggered the event</param>
    /// <param name="config">Config to send to S+</param>
    public delegate void VtcControlsConfigChangedDel(object sender, VideoConference config);

    /// <summary>
    /// Delegate for S+ for OnConfigurationChanged event.
    /// </summary>
    /// <param name="sender">Object that triggered the event</param>
    /// <param name="config">Config to send to S+</param>
    public delegate void SwitcherControlsConfigChangedDel(object sender, Switcher config);
}