using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeWifi;

namespace WiFi_Start
{
    public partial class MeasureRSSI : Form
    {
        public MeasureRSSI()
        {
            InitializeComponent();
        }

        private void MeasureRSSI_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "SSID";
            dataGridView1.Columns[1].Name = "Quality";
            dataGridView1.Columns[2].Name = "Security";
            dataGridView1.Columns[3].Name = "Cipher";
            foreach (var col in dataGridView1.Columns)
            {
                ((DataGridViewColumn)col).Width = dataGridView1.Width / dataGridView1.Columns.Count;
            }

            FullfillGrid(GetNetworkList());
        }

        private void FullfillGrid(List<List<string>> wifi)
        {
            label1.Text = "Общее число wi-fi сетей: " + wifi.Count;
            dataGridView1.RowCount = wifi.Count;
            for (int i = 0; i < wifi.Count; i++)
            {
                for (int j = 0; j < wifi[i].Count; j++)
                {
                    dataGridView1[j, i].Value = wifi[i][j];
                }
            }
        }

        private List<List<string>> GetNetworkList()
        {
            List<List<string>> wifiList = new List<List<string>>();
            WlanClient client = new WlanClient();

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {

                Wlan.WlanAvailableNetwork[] wlanBssEntries = wlanIface.GetAvailableNetworkList(0);

                for (int i = 0; i < wlanBssEntries.Length; i++)
                {
                    wifiList.Add(new List<string>());
                    wifiList[i].Add(System.Text.ASCIIEncoding.ASCII.GetString(wlanBssEntries[i].dot11Ssid.SSID).Trim((char)0));
                    //wifiList[i].Add(network.dot11Ssid.SSIDLength.ToString());
                    //wifiList[i].Add(network.morePhyTypes.ToString());
                    wifiList[i].Add(wlanBssEntries[i].wlanSignalQuality.ToString() + "%"); // качество связи в процентах
                    wifiList[i].Add(wlanBssEntries[i].dot11DefaultAuthAlgorithm.ToString());//.Trim((char)0)); // тип безопасности
                    wifiList[i].Add(wlanBssEntries[i].dot11DefaultCipherAlgorithm.ToString());//.Trim((char)0)); // тип шифрования
                }
            }

            return wifiList;
        }

        private string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FullfillGrid(GetNetworkList());
            }
            catch (Exception exc)
            {
                MessageBox.Show("ERROR Приложение будет закрыто. > \n" + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
