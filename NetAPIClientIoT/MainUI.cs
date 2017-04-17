using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetAPIClientIoT
{
    public partial class MainUI : Form
    {
        //  Configuration directory and file location
        private const string configDir = "\\Temp";
        private const string configFile = "\\FormConfig.xml";
        /// Root element in the XML definition
        public static DataSet configDs = new DataSet("Configuration");
        /// Startup setting definition
        public static DataTable formFields = new DataTable("formFields");
        public Socket soConnect;

        public MainUI()
        {
            InitializeComponent();
            loadPersistedFieldsFile();
        }

        /// ----------------------------------------------------------------
        ///     CONFIGURATION FILE LOAD / SAVE SECTION
        /// ----------------------------------------------------------------
        /// <summary>
        /// Save data to XML file
        /// </summary>
        public static void persistFields()
        {
            configDs.Tables.Clear();
            configDs.Tables.Add(formFields);
            configDs.WriteXml(getConfigXMLFile());
        }

        /// <summary>
        /// GET method for config directory
        /// </summary>
        /// <returns>Directory string</returns>
        public static string getConfigXMLDir()
        {
            /// read the current directory location
            String modulePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            String solutionFolder = modulePath.Substring(0, modulePath.LastIndexOf(Path.DirectorySeparatorChar));
            return solutionFolder + configDir;
        }

        /// <summary>
        /// GET method for config file
        /// </summary>
        /// <returns>Full configuration file path string</returns>
        public static string getConfigXMLFile()
        {
            return getConfigXMLDir() + configFile;
        }

        /// <summary>
        /// Load data from XML file
        /// </summary>
        public static void loadPersistedFieldsFile()
        {
            try
            {
                //  check directory presence otherwise create dir
                if (!Directory.Exists(getConfigXMLDir()))
                {
                    Directory.CreateDirectory(getConfigXMLDir());
                }
                #region loading configuration files
                if (!File.Exists(getConfigXMLFile()))
                {
                    // create blank file instead getting exception MessageBox.Show("File not found " + getConfigXMLFile());
                    createEmptyFields();
                    persistFields();
                }
                configDs.Clear();
                /// read native configuration data from XML
                configDs.ReadXml(getConfigXMLFile(), XmlReadMode.Auto);
                formFields = configDs.Tables["formFields"];
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Global.ConfigRead : " + ex.Message);
            }
        }

        /// <summary>
        /// feed the xml with dummy data
        /// </summary>
        public static void createEmptyFields()
        {
            formFields.Rows.Add();
            formFields.Columns.Add("IP");
            formFields.Columns.Add("PORT");
            formFields.Columns.Add("APIKEY");
            formFields.Columns.Add("SENTCONTENT");

            formFields.Rows[0]["IP"] = "192.168.1.138";
            formFields.Rows[0]["PORT"] = "80";
            formFields.Rows[0]["APIKEY"] = "C6BCK1VBX2TY5HU1";
            formFields.Rows[0]["SENTCONTENT"] = "GET /img/4";
        }

        /// <summary>
        /// this method get fields setting from the form to save them after
        /// </summary>
        private static void captureFieldsToPersist()
        {
            String IPandPort = Program.mui.IPPortTx.Text;
            formFields.Rows[0]["IP"] = IPandPort.Split(':')[0];
            formFields.Rows[0]["PORT"] = IPandPort.Split(':')[1];
            formFields.Rows[0]["APIKEY"] = Program.mui.APIKeyTx.Text;
            formFields.Rows[0]["SENTCONTENT"] = Program.mui.DataToSentTx.Text;
            persistFields();
        }

        /// <summary>
        /// populate all data in the form
        /// </summary>
        private void loadPersistedFields()
        {
            loadPersistedFieldsFile();
            try
            {
                IPPortTx.Text = formFields.Rows[0]["IP"].ToString() + ":" + formFields.Rows[0]["PORT"].ToString();
                APIKeyTx.Text = formFields.Rows[0]["APIKEY"].ToString();
                DataToSentTx.Text = formFields.Rows[0]["SENTCONTENT"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Missing element in config file - delete the XML config file and restart the program: " + getConfigXMLFile() + ex.ToString());
            }
        }

        /// <summary>
        /// Main sendin / receiving method - one synchronous session
        /// </summary>
        /// <returns></returns>
        private string SentDataToSocket()
        {
            byte[] receivedData = new byte[4*1024]; //fix length because expected is just small amount of data
            int receiveDataLen = 0;
            String IPandPort = Program.mui.IPPortTx.Text;
            IPHostEntry dnsIp;
            try
            {
                dnsIp = Dns.GetHostEntry(IPandPort.Split(':')[0]);
            }
            catch (Exception e)
            {
                return ("\r\nUnable to resolve DNS name. " + e.ToString());
            }
            IPEndPoint ipEp = new IPEndPoint(dnsIp.AddressList[0], int.Parse(IPandPort.Split(':')[1]));
            soConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                soConnect.Connect(ipEp);
                Thread.Sleep(1000);
            }
            catch (SocketException e)
            {
                SentMessageBt.BackColor = Color.LightSlateGray;
                return ("\r\nUnable to connect to server. " + e.ToString());
            }

            DataToSentTx.Text = updateDataParameters(DataToSentTx.Text);
            soConnect.Send(Encoding.ASCII.GetBytes(DataToSentTx.Text+"\n\n"));

            soConnect.ReceiveTimeout = 1000;
            try
            {
                receiveDataLen = soConnect.Receive(receivedData);
            }
            catch (Exception e)
            {
                DataResponseTx.Text += "\r\nNo data received";
            }
            SentMessageBt.BackColor = Color.LightSlateGray;
            soConnect.Shutdown(SocketShutdown.Both);        // keep in mind to close session
            soConnect.Close();
            return ("\r\n" + Encoding.ASCII.GetString(receivedData, 0, receiveDataLen));
        }

        /// <summary>
        /// short manipulation method to update the body size and key and API URL
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string updateDataParameters(string text)
        {
            Regex rgx = new Regex("Host: \\S*");
            text = rgx.Replace(text, "Host: " + IPPortTx.Text.Substring(0, IPPortTx.Text.IndexOf(":")));

            rgx = new Regex("X-THINGSPEAKAPIKEY: \\S*");
            text = rgx.Replace(text, "X-THINGSPEAKAPIKEY: " + APIKeyTx.Text);

            rgx = new Regex("\\S(.*?)&");
            text = rgx.Replace(text, APIKeyTx.Text + "&", 1);

            int idxApiKey = text.IndexOf(APIKeyTx.Text + "&");
            int cnLen = text.Substring(idxApiKey, text.Length-idxApiKey).Length;
            rgx = new Regex("Content-Length: \\S*");
            text = rgx.Replace(text, "Content-Length: " + cnLen.ToString());

            return text;
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.mui.Invoke(new MethodInvoker(delegate ()
            {
                captureFieldsToPersist();
            }
            ));

            if ((soConnect!=null)&&(soConnect.Connected))
            {
                soConnect.Shutdown(SocketShutdown.Both);
                soConnect.Close();
            }
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            loadPersistedFields();
        }

        private void SentMessageBt_Click(object sender, EventArgs e)
        {
            Program.mui.Invoke(new MethodInvoker(delegate ()
            {
                SentMessageBt.BackColor = Color.Azure;
                DataResponseTx.AppendText(SentDataToSocket());
                DataResponseTx.ScrollToCaret();
            }
            ));
        }
    }
}
