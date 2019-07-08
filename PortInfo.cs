using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PortScanner
{

    /// <summary>
    /// Represents a TCP port.
    /// </summary>
    class PortInfo : INotifyPropertyChanged
    {

        /// <summary>
        /// Dictionary of ports and their descriptions as defined in the PortDictionary.txt resource.
        /// </summary>
        public static Dictionary<ushort, string> portDictionary;

        public PortInfo(ushort portNumber)
        {
            Port = portNumber;

            _ = portDictionary.TryGetValue(portNumber, out string description);
            Description = description ?? String.Empty;
        }

        /// <summary>
        /// Number of the TCP port.
        /// </summary>
        public ushort Port { get; private set; }

        /// <summary>
        /// Short description of what this port is commonly used for.
        /// </summary>
        public string Description { get; private set; }

        private bool? _open;

        /// <summary>
        /// Whether the port is open or not.
        /// </summary>
        public bool? Open
        {
            get
            {
                return _open;
            }
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
        /// Initializes the port dictionary with the values obtained from the PortDictionary.txt resource.
        /// </summary>
        public static void InitializePortDictionary()
        {
            if (String.IsNullOrEmpty(Properties.Resources.PortDictionary))
            {
                return;
            }

            portDictionary = new Dictionary<ushort, string>();
            string[] textFileLines = Properties.Resources.PortDictionary.Replace("\r\n", "").Split(';');
            for (int i = 0; i < textFileLines.Length; i++)
            {
                if (textFileLines[i] == String.Empty)
                {
                    continue;
                }
                string[] lineValues = textFileLines[i].Split('=');
                portDictionary.Add(Convert.ToUInt16(lineValues[0]), lineValues[1]);
            }
        }

        #region Overrides

        /// <summary>
        /// Returns a string with the data of the PortInfo object formatted for displaying in the ListBox control of the main form.
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
        /// Compares two PortInfo objects based on their values of the Port property. Returns false if the object is not an instance of PortInfo.
        /// </summary>
        /// <param name="obj">Object to be compared.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PortInfo portInfo))
            {
                return false;
            }
            return portInfo.Port == Port;
        }

        public override int GetHashCode()
        {
            return Port;
        }

        #endregion

    }
}
