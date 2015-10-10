using MSNPSharp.IO;
namespace MSNPSharpClient
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("Settings")]
    public class UserSettings : MCLSerializer
    {
        public static string settingsdir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Butterfly Messenger");
        private static string fileName = Path.Combine(settingsdir, "settings.mcl");

        private string username = "writeemailhere@example.com";
        private string password = "tstmsnpsharp";
        private string lastStatus = "Online";
        private string useTcpGateway = "false";
        private string epName = Environment.MachineName;
        private string machineGuid = Guid.NewGuid().ToString();

        public string MachineGuid
        {
            get
            {
                return machineGuid;
            }
            set
            {
                machineGuid = value;
            }
        }

        public string EpName
        {
            get
            {
                return epName;
            }
            set
            {
                epName = value;
            }
        }

        public string UseTcpGateway
        {
            get
            {
                return useTcpGateway;
            }
            set
            {
                useTcpGateway = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string LastStatus
        {
            get
            {
                return lastStatus;
            }
            set
            {
                lastStatus = value;
            }
        }

        public static UserSettings Load(string filePassword)
        {
            if (!Directory.Exists(settingsdir)) Directory.CreateDirectory(settingsdir);
            return (UserSettings)LoadFromFile(fileName, MclSerialization.Cryptography | MclSerialization.Compression, typeof(UserSettings), filePassword, false);
        }

    }
};