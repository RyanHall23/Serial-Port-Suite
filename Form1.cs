using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialSuite
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort = new SerialPort();    //initialise serial port
        int currentRow = 0;                                 //Update current row for displaying data

        public Form1()
        {
            InitializeComponent();
            InitSerial();                   //create initial serialport values
            InitComboBoxPort();             //update combobox to show only connected ports
            buttonPause.Enabled = true;    //restrict user error
            buttonStop.Enabled = false;     //restrict user error
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Use Form2's instance of a serialPort for types etc and copy values into primary serial port
        /// </summary>
        /// <param name="sPort"></param>
        public void SerialPortSet(SerialPort sPort)
        {
            try
            {
                serialPort.Parity = sPort.Parity;
                serialPort.StopBits = sPort.StopBits;
                serialPort.DataBits = sPort.DataBits;
                serialPort.Handshake = sPort.Handshake;
                serialPort.RtsEnable = sPort.RtsEnable;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// When button start is pressed, continue to disable the start button and baud/port buttons.
        /// Attempt read serial port that has been configured by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start Pressed");
            DisableButtons("start");
            SerialRead();
        }

        /// <summary>
        /// When stop button is pressed, renable start and baud/port buttons.
        /// Stop reading serial port data and close connections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Stop Pressed");
            DisableButtons("stop");
            labelStatusMsg.Text = "Stopped";

            try
            {
                serialPort.DiscardOutBuffer();
                serialPort.DiscardInBuffer();
                serialPort.Close();
                serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        /// <summary>
        /// When pause button is pressed, renable start button but keep port/baud locked as connection is not closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPause_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Pause Pressed");
            DisableButtons("pause");
            labelStatusMsg.Text = "Paused";
        }

        /// <summary>
        /// Options button opens a form2 instance and allows the user to edit more options in a new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2(this);
            F2.Show();
        }

        /// <summary>
        /// Button Update Ports, will update any connected ports without the need to restart to the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdatePorts_Click(object sender, EventArgs e)
        {
            InitComboBoxPort();
        }

        /// <summary>
        /// Button clear is used to clear the table so the user does not need to restart to the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            serialDataGridView.Rows.Clear();
            serialDataGridView.Refresh();
            currentRow = 0;

        }

        /// <summary>
        /// Baud combo box allows the user to select from predefined baudrates to reduce any user error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
            Debug.WriteLine("Baudrate changed to: " + serialPort.BaudRate);
        }

        /// <summary>
        /// Port combo box is dynamic and adjusts to connected ports when the application is open
        /// Selected serial port defaults to COM0 which may not be present
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = (comboBoxPort.SelectedItem.ToString());
            Debug.WriteLine("Port changed to: " + serialPort.PortName);
        }
        
        /// <summary>
        /// SerialRead carries out the main logic behind reading the data.
        /// Attempting to open the port with a try,catch is the first step
        /// If ok, read data and translate. Else, alert user and renable buttons
        /// </summary>
        private void SerialRead()
        {
            serialPort.Close();
            Debug.WriteLine("Opening Serial Port...");

            try //Check if COM port can be opened
            {
                serialPort.Open();
                labelStatusMsg.Text = "Connected to Port : " + serialPort.PortName + " Port";
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
            catch (Exception ex) //throw excpetion for not found, alert user and reset butttons
            {
                labelStatusMsg.Text = "No Serial Device detected on : " + serialPort.PortName;
                buttonStart.Enabled = true;
                comboBoxBaud.Enabled = true;
                comboBoxPort.Enabled = true;
                buttonOptions.Enabled = true;
                buttonUpdatePorts.Enabled = true;
                serialPort.Close();
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Handles data recieved from the serial port, and reads it. Which then moves onto the hex translation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            Debug.WriteLine("Data Received: " + inData);

            UpdateHexText(inData);
        }

        /// <summary>
        /// Updates the incremental ID, data Hex, and data Raw columns respectively
        /// </summary>
        /// <param name="rawData"></param>
        private void UpdateHexText(string rawData)
        {
            try
            {
                if (this.serialDataGridView.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(UpdateHexText);
                    this.Invoke(d, new object[] { rawData });
                }
                else
                {
                    char[] charValues = rawData.ToCharArray();
                    string hexOutput = "";
                    foreach (char _eachChar in charValues)
                    {
                        int value = Convert.ToInt32(_eachChar);
                        hexOutput += String.Format("{0:X} ", value);
                    }

                    DataGridViewRow row = (DataGridViewRow)serialDataGridView.Rows[0].Clone();
                    row.Cells[0].Value = currentRow;
                    row.Cells[1].Value = serialPort.PortName;
                    row.Cells[2].Value = hexOutput;
                    row.Cells[3].Value = rawData;
                    serialDataGridView.Rows.Add(row);

                    currentRow++;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        /// <summary>
        /// Intialises the serial data ready for the user to edit
        /// </summary>
        void InitSerial()
        {
            try
            {
                //Default Serial Info
                serialPort.PortName = "COM0";
                serialPort.BaudRate = 125000;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.RtsEnable = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Makes the port combo box dynamic rather than hvaing prestored indexes
        /// Alerts the user if there are any devices connected
        /// </summary>
        void InitComboBoxPort()
        {
            comboBoxPort.Items.Clear();

            try
            {
                // Get a list of serial port names.
                string[] ports = SerialPort.GetPortNames();
                comboBoxPort.Items.AddRange(ports);
                comboBoxPort.Text = comboBoxPort.Items.Count.ToString() + " Port(s) found";
                // Display each port name to the console.
                foreach (string port in ports)
                {
                    Debug.WriteLine(port);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Disable buttons contains the logic for disabling and enabling the correct buttons
        /// So they can't be repeatedly pressed, reducing user error
        /// </summary>
        /// <param name="buttonClicked"></param>
        void DisableButtons(String buttonClicked)
        {
            switch(buttonClicked)
            {
                case "start":
                    //buttons
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonStart.Enabled = false;
                    buttonOptions.Enabled = false;
                    buttonUpdatePorts.Enabled = false;

                    //combo boxes
                    comboBoxBaud.Enabled = false;
                    comboBoxPort.Enabled = false;
                    break;
                case "stop":
                    //buttons
                    buttonPause.Enabled = true;
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = false;
                    buttonOptions.Enabled = true;
                    buttonUpdatePorts.Enabled = true;

                    //combo boxes
                    comboBoxBaud.Enabled = true;
                    comboBoxPort.Enabled = true;
                    break;
                case "pause":
                    //buttons
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonOptions.Enabled = false;

                    //combo boxes
                    comboBoxBaud.Enabled = false;
                    comboBoxPort.Enabled = false;
                    break;
            }
        }

    }
}

delegate void SetTextCallback(string text);