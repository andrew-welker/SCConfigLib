using SCConfigSplus.JSON;

namespace SCSplusConfig.Delegates
{
    /// <summary>
    /// Delegate for S+ OnConfigurationChanged event for a list object
    /// </summary>
    /// <param name="index">Index in the list</param>
    /// <param name="source">Object</param>
    public delegate void SourcesConfigChangedDel(ushort index, Source source);

    /// <summary>
    /// Delegate for S+ for OnConfigurationChanged event.
    /// </summary>
    /// <param name="sender">Object that triggered the event</param>
    /// <param name="config">Config to send to S+</param>
    public delegate void EnvControlsConfigChangedDel(object sender, EnvironmentControls config);
}