using System;
using System.Collections.Generic;
using System.ComponentModel;
using PortScanner.Properties;

namespace PortScanner
{
    /// <summary>
    ///     Represents a TCP port.
    /// </summary>
    public class PortInfo : INotifyPropertyChanged
    {
        /// <summary>
        ///     Dictionary of ports and their descriptions as defined in the PortDictionary.txt resource.
        /// </summary>
        public static Dictionary<ushort, string> PortDictionary;

        private bool? _open;

        public PortInfo(ushort portNumber)
        {
            Port = portNumber;
            Description = PortDictionary.TryGetValue(portNumber, out string description) ? description : string.Empty;
        }

        /// <summary>
        ///     Number of the TCP port.
        /// </summary>
        public ushort Port { get; }

        /// <summary>
        ///     Short description of what this port is commonly used for.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Whether the port is open or not.
        /// </summary>
        public bool? Open
        {
            get => _open;
            set
            {
                _open = value;
                OnPropertyChanged("Open");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        ///     Initializes the port dictionary with the values obtained from the PortDictionary.txt resource.
        /// </summary>
        public static void InitializePortDictionary()
        {
            if (string.IsNullOrEmpty(Resources.PortDictionary)) return;

            PortDictionary = new Dictionary<ushort, string>();
            var textFileLines = Resources.PortDictionary.Replace("\r\n", "").Split(';');
            for (int i = 0; i < textFileLines.Length; i++)
            {
                if (textFileLines[i] == string.Empty) continue;
                string[] lineValues = textFileLines[i].Split('=');
                PortDictionary.Add(Convert.ToUInt16(lineValues[0]), lineValues[1]);
            }
        }

        #region Overrides

        /// <summary>
        ///     Returns a string with the data of the PortInfo object formatted for displaying in the ListBox control of the main
        ///     form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string returnStr = Port.ToString().PadRight(6, ' ') + Description.PadRight(33, ' ');
            switch (Open)
            {
                case true:
                    returnStr += "OPEN";
                    break;
                case false:
                    returnStr += "CLOSED";
                    break;
                default:
                    returnStr += "*";
                    break;
            }

            return returnStr;
        }

        /// <summary>
        ///     Compares two PortInfo objects based on their values of the Port property. Returns false if the object is not an
        ///     instance of PortInfo.
        /// </summary>
        /// <param name="obj">Object to be compared.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PortInfo portInfo)) return false;
            return portInfo.Port == Port;
        }

        public override int GetHashCode()
        {
            return Port;
        }

        #endregion
    }
}