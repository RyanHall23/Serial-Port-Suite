using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SerialSuite
{
    public partial class Form2 : Form
    {
        Form1 F1;       //initialises F1 variable
        SerialPort sPort = new SerialPort();    //used to copy create a template of the types
        CurrentSettings userSettings;
        string xmlFileName = "advancedSettings.xml";
        
        public Form2()
        {
            InitializeComponent();
            this.Hide();
            try
            {
                //SerializeDataRead(xmlFileName);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                userSettings.port.Parity = sPort.Parity;
                userSettings.port.StopBits = sPort.StopBits;
                userSettings.port.DataBits = sPort.DataBits;
                userSettings.port.Handshake = sPort.Handshake;
                userSettings.port.RtsEnable = sPort.RtsEnable;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        struct CurrentSettings
        {
            public string parity;
            public string stopBits;
            public string DataBits;
            public string handshake;
            public string requestToSend;
            public SerialPort port;
        }
        /// <summary>
        /// Allows for access to Form1 to save user changes to the serial port configuration
        /// </summary>
        /// <param name="callingForm"></param> Is the source of which this form was invoked
        public Form2(Form callingForm)
        {
            F1 = callingForm as Form1;
            InitializeComponent();
        }


        /// <summary>
        /// Updates the parity selection from predefined valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
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
        private void comboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
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
                case 3:
                    sPort.StopBits = StopBits.None;
                    Debug.WriteLine("Parity changed to None");
                    break;
            }
        }

        /// <summary>
        /// Updates the data bits from predefined valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxStopBits.SelectedIndex)
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
        private void comboBoxHandshake_SelectedIndexChanged(object sender, EventArgs e)
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
        /// Updates the RTS setting from valid options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxRTS_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxRTS.SelectedIndex)
            {
                case 0:
                    sPort.RtsEnable = true;
                    Debug.WriteLine("RTS changed to TRUE");
                    break;
                case 1:
                    sPort.RtsEnable = false;
                    Debug.WriteLine("RTS changed to FALSE");
                    break;
            }
        }

        /// <summary>
        /// If confirmed, copies the settings to the main serial port and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                F1.SerialPortSet(sPort);    // call set function and pass set version of serial port
                //SerializeDataSet(xmlFileName);
                this.Hide();                // close window
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
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();    //close window
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            sPort.Parity = Parity.None;
            sPort.StopBits = StopBits.One;
            sPort.DataBits = 8;
            sPort.Handshake = Handshake.None;
            sPort.RtsEnable = true;
            F1.SerialPortSet(sPort);
            this.Hide();
            //SerializeDataSet(xmlFileName);
        }

        private void SerializeDataSet(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(CurrentSettings));
            DataSet ds = new DataSet("advDataSet");
            DataTable t = new DataTable("table1");
            DataColumn c = new DataColumn("setting");
            t.Columns.Add(c);
            ds.Tables.Add(t);
            DataRow r;
            //r = userSettings;
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, ds);
            writer.Close();
        }

        private void SerializeDataRead(string filename)
        {
            CurrentSettings myObject;
            XmlSerializer mySerializer = new XmlSerializer(typeof(CurrentSettings));
            FileStream myFileStream = new FileStream(filename, FileMode.Open);
            myObject = (CurrentSettings)mySerializer.Deserialize(myFileStream);
            userSettings = myObject;
        }

        private void SerDataWrite()
        {

        }

        private void serDataLoad()
        { 
}
        
    }
}
