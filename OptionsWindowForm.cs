using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SerialSuite
{
    public partial class OptionsWindowForm : Form
    {
        MainWindowForm mainWindow;                               // Initialises F1 form
        SerialPort sPort = new SerialPort();    // Used to copy create a template of the types
        UserSettings userSettings = new UserSettings();   // Creates an object to persist serial port settings

        public OptionsWindowForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Allows for access to Form1 to save user changes to the serial port configuration
        /// </summary>
        /// <param name="callingForm"></param> Is the source of which this form was invoked
        public OptionsWindowForm(Form callingForm)
        {
            mainWindow = callingForm as MainWindowForm;
            InitializeComponent();
            try
            {
                userSettings = ReadConfig();      // Attempt to read current serial port config
                CopyStoredSettings();   // Write settings to application
                UpdateTextBoxes();      // Update text boxes in advanced options window
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
                DefaultSettings();  // Config is non-existent, set to defaults
                StoreSettings();    // Copy default settings to peristence class
                WriteConfig(userSettings);    // Write persistance class to disk
            }
        }

        /// <summary>
        /// Class for persisting serial port settings
        /// </summary>
        [Serializable]
        public class UserSettings
        {
            public Handshake handshake;
            public Parity parity;
            public StopBits stopBits;
            public int dataBits;
            public bool rtsEnabled;
            public Encoding encoding;
        }

        /// <summary>
        /// Updates the parity selection from predefined valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxParity.SelectedIndex)
            {
                case 0:
                    sPort.Parity = Parity.None;
                    Debug.WriteLine("Parity changed to None");
                    break;
                case 1:
                    sPort.Parity = Parity.Odd;
                    Debug.WriteLine("Parity changed to Odd");
                    break;
                case 2:
                    sPort.Parity = Parity.Even;
                    Debug.WriteLine("Parity changed to Even");
                    break;
                case 3:
                    sPort.Parity = Parity.Mark;
                    Debug.WriteLine("Parity changed to Mark");
                    break;
                case 4:
                    sPort.Parity = Parity.Space;
                    Debug.WriteLine("Parity changed to Space");
                    break;
            }
        }

        /// <summary>
        /// Updates the stop bits for the serial port from defined valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxStopBits.SelectedIndex)
            {
                case 0:
                    sPort.StopBits = StopBits.One;
                    Debug.WriteLine("Parity changed to 1");
                    break;
                case 1:
                    sPort.StopBits = StopBits.OnePointFive;
                    Debug.WriteLine("Parity changed to 1.5");
                    break;
                case 2:
                    sPort.StopBits = StopBits.Two;
                    Debug.WriteLine("Parity changed to 2");
                    break;
            }
        }

        /// <summary>
        /// Updates the data bits from predefined valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDataBits.SelectedIndex)
            {
                case 0:
                    sPort.DataBits = 8;
                    Debug.WriteLine("DataBits changed to 8");
                    break;
                case 1:
                    sPort.DataBits = 7;
                    Debug.WriteLine("DataBits changed to 7");
                    break;
                case 2:
                    sPort.DataBits = 6;
                    Debug.WriteLine("DataBits changed to 6");
                    break;
                case 3:
                    sPort.DataBits = 5;
                    Debug.WriteLine("DataBits changed to 5");
                    break;
            }
        }

        /// <summary>
        /// Updates the handshake serial setting from valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxHandshake_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxHandshake.SelectedIndex)
            {
                case 0:
                    sPort.Handshake = Handshake.None;
                    Debug.WriteLine("Handshake changed to None");
                    comboBoxHandshake.Text = "None";
                    break;
                case 1:
                    sPort.Handshake = Handshake.RequestToSend;
                    Debug.WriteLine("Handshake changed to RequestToSend");
                    comboBoxHandshake.Text = "RequestToSend";
                    break;
                case 2:
                    sPort.Handshake = Handshake.RequestToSendXOnXOff;
                    Debug.WriteLine("Handshake changed to RequestToSendXOnXOff");
                    comboBoxHandshake.Text = "RequestToSendXOnXOff";
                    break;
                case 3:
                    sPort.Handshake = Handshake.XOnXOff;
                    Debug.WriteLine("Handshake changed to XOnXOff");
                    comboBoxHandshake.Text = "XOnXOff";
                    break;
            }
        }

        /// <summary>
        /// For changing the encoding type of the serial port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentEncoding = comboBoxEncoding.Text;
            sPort.Encoding = Encoding.GetEncoding(currentEncoding);
        }

        /// <summary>
        /// Sets RTS enabled to true via radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonRTStrue_CheckedChanged(object sender, EventArgs e)
        {
            sPort.RtsEnable = true;
            Debug.WriteLine("RTS changed to " + sPort.RtsEnable.ToString());
        }

        /// <summary>
        /// Sets RTS enabled to false via radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonRTSfalse_CheckedChanged(object sender, EventArgs e)
        {
            sPort.RtsEnable = false;
            Debug.WriteLine("RTS changed to " + sPort.RtsEnable.ToString());
        }

        /// <summary>
        /// If confirmed, copies the settings to the main serial port and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                mainWindow.SerialPortSet(sPort);    // Call set function and pass set version of serial port
                StoreSettings();            // Store port settings for persisting
                WriteConfig(userSettings);            // Store port settings peristence
                Debug.WriteLine("Settings Confirmed");
                this.Hide();                // Close window
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// If canceled, disregards any changes and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Changes Cancelled");   //Don't do any changes inc. store/writes/reads
            this.Hide();    //close window
        }

        /// <summary>
        /// Sets serial port settings to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDefault_Click(object sender, EventArgs e)
        {
            DefaultSettings();          // Restore port settings to default
            mainWindow.SerialPortSet(sPort);    // Set port settings to default 
            StoreSettings();            // Store default port settings ready for persistence
            WriteConfig(userSettings);            // Save port settings with peristence
            Debug.WriteLine("Settings set to default");
            this.Hide();
        }

        /// <summary>
        /// Writes the persistance class object UserSettings to disk for next instance of menu or startup
        /// </summary>
        /// <param name="port"></param>
        public static void WriteConfig(UserSettings port)
        {
            string dir = @".\";
            string serializationFile = Path.Combine(dir, "sps.bin");

            //serialize
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, port);
            }

        }

        /// <summary>
        /// Reads stored peristance on disk
        /// </summary>
        /// <returns>
        /// Returns a UserSettings class object from the serialised data
        /// </returns>
        public UserSettings ReadConfig()
        {
            string dir = @".\";
            string serializationFile = Path.Combine(dir, "sps.bin");
            UserSettings savedPort;

            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                savedPort = (UserSettings)bformatter.Deserialize(stream);
            }
            return savedPort;
        }

        /// <summary>
        /// Copies settings from the read persistance class on disk to the serial port object
        /// </summary>
        public void StoreSettings()
        {
            userSettings.parity = sPort.Parity;
            userSettings.stopBits = sPort.StopBits;
            userSettings.dataBits = sPort.DataBits;
            userSettings.handshake = sPort.Handshake;
            userSettings.rtsEnabled = sPort.RtsEnable;
            userSettings.encoding = sPort.Encoding;
        }

        /// <summary>
        /// Sets the serial port object back to "default"
        /// </summary>
        public void DefaultSettings()
        {
            sPort.Parity = Parity.None;
            sPort.StopBits = StopBits.One;
            sPort.DataBits = 8;
            sPort.Handshake = Handshake.None;
            sPort.RtsEnable = true;
            sPort.Encoding = Encoding.GetEncoding("iso-8859-1");
        }
        
        /// <summary>
        /// Writes settings from the serial port to the peristance class object
        /// </summary>
        public void CopyStoredSettings()
        {
            sPort.DataBits = userSettings.dataBits;
            sPort.StopBits = userSettings.stopBits;
            sPort.RtsEnable = userSettings.rtsEnabled;
            sPort.Handshake = userSettings.handshake;
            sPort.Parity = userSettings.parity;
            sPort.Encoding = userSettings.encoding;
        }

        /// <summary>
        /// Updates text boxes text with user settings to show peristing of data
        /// </summary>
        public void UpdateTextBoxes()
        {
            comboBoxDataBits.Text = userSettings.dataBits.ToString();
            comboBoxHandshake.Text = userSettings.handshake.ToString();
            comboBoxParity.Text = userSettings.parity.ToString();

            // Check for stopbits value and set text accordingly
            switch (userSettings.stopBits)
            {
                case StopBits.One:
                    comboBoxStopBits.Text = "1";
                    break;
                case StopBits.OnePointFive:
                    comboBoxStopBits.Text = "1.5";
                    break;
                case StopBits.Two:
                    comboBoxStopBits.Text = "2";
                    break;
            }

            //If RTS is enabled mark correct radio buttons as checked/unchecked
            if (userSettings.rtsEnabled)
            {
                radioButtonRTSfalse.Checked = false;
                radioButtonRTStrue.Checked = true;
            }
            else
            {
                radioButtonRTSfalse.Checked = true;
                radioButtonRTStrue.Checked = false;
            }

            
            switch(userSettings.encoding.ToString())
            {
                case "System.Text.Latin1Encoding":
                    comboBoxEncoding.Text = "iso-8859-1";
                    break;
                case "System.Text.UnicodeEncoding":
                    comboBoxEncoding.Text = "unicode";
                    break;
                case "System.Text.ASCIIEncoding":
                    comboBoxEncoding.Text = "ascii";
                    break;
            }
        }
    }
}