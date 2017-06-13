namespace SCConfigSPlus.JSON
{
    /// <summary>
    /// Class for describing a DSP. Contains information on type, communications, and Audio conferencing.
    /// </summary>
    public class Dsp
    {
        /// <summary>
        /// Type of DSP. Could be QSC, Biamp, ClearOne, etc.
        /// </summary>
        public ushort Type { get; set; }

        /// <summary>
        /// Communications type. Could be RS-232, TCP/IP
        /// </summary>
        public ushort CommunicationsType { get; set; }

        /// <summary>
        /// IP Address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// TCP Port
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// Audio Conferencing. If greater than 0, the system has Audio Conferencing.
        /// </summary>
        public ushort AudioConference { get; set; }
    }
}