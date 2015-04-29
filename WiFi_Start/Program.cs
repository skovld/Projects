using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;
using NativeWifi;
using System.Windows.Forms;

namespace WiFi_Start
{
    class Program
    {
        static void GetWifiNets(List<List<string>> list)
        {
            foreach (List<string> innerlist in list)
            {
                Console.Write(" >>> ");
                foreach (string str in innerlist)
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine();
            }
                
        }

        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new MeasureRSSI());
            
            #region WlanClient



            /*
            WlanClient client = new WlanClient();
            //WlanClient.WlanInterface interf = new WlanClient.WlanInterface(wlan, new Wlan.WlanInterfaceInfo());

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                // Lists all networks with WEP security
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                foreach (Wlan.WlanAvailableNetwork network in networks)
                {
                    if (network.dot11DefaultCipherAlgorithm == Wlan.Dot11CipherAlgorithm.WEP)
                    {
                        Console.WriteLine("Found WEP network with SSID {0}.", GetStringForSSID(network.dot11Ssid));
                    }
                }
            }
             */
            #endregion
        }
    }
}