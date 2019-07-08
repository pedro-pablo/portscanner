using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PortScanner
{
    class PortInfo : INotifyPropertyChanged
    {

        public static Dictionary<ushort, string> portDictionary;

        public PortInfo(ushort portNumber)
        {
            Port = portNumber;

            string description;
            portDictionary.TryGetValue(portNumber, out description);
            Description = description ?? String.Empty;
        }

        public ushort Port { get; private set; }

        public string Description { get; private set; }

        private bool? _open;
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
