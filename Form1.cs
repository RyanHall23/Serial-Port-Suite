using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialSuite
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort = new SerialPort();    //initialise serial port
        bool SerialPortPendingClose = false;                //control serial ports access

        public Form1()
        {
            InitializeComponent();
            InitSerial();       //create initial serialport values
            InitComboBoxPort(); //update combobox to show only connected ports
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
            serialPort.Parity = sPort.Parity;
            serialPort.StopBits = sPort.StopBits;
            serialPort.DataBits = sPort.DataBits;
            serialPort.Handshake = sPort.Handshake;
            serialPort.RtsEnable = sPort.RtsEnable;
        }

        /// <summary>
        /// When button start is pressed, continue to disable the start button and baud/port buttons.
        /// Attempt read serial port that has been configured by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start Pressed");
            DisableButtons("start");
            serialRead();
        }

        /// <summary>
        /// When stop button is pressed, renable start and baud/port buttons.
        /// Stop reading serial port data and close connections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Stop Pressed");
            DisableButtons("stop");
            labelStatusMsg.Text = "Stopped";

            SerialPortPendingClose = true;
        }

        /// <summary>
        /// When pause button is pressed, renable start button but keep port/baud locked as connection is not closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPause_Click(object sender, EventArgs e)
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
        private void buttonOptions_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2(this);
            F2.Show();
        }

        /// <summary>
        /// Baud combo box allows the user to select from predefined baudrates to reduce any user error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
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
        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = (comboBoxPort.SelectedItem.ToString());
            Debug.WriteLine("Port changed to: " + serialPort.PortName);
        }
        
        /// <summary>
        /// SerialRead carries out the main logic behind reading the data.
        /// Attempting to open the port with a try,catch is the first step
        /// If ok, read data and translate. Else, alert user and renable buttons
        /// </summary>
        private void serialRead()
        {
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            serialPort.Close();

            Debug.WriteLine("Opening Serial Port...");

            try //Check if COM port can be opened
            {
                serialPort.Open();
                labelStatusMsg.Text = "Connected to Port : " + serialPort.PortName + " Port";
            }
            catch (Exception e) //throw excpetion for not found, alert user and reset butttons
            {
                labelStatusMsg.Text = "No Serial Device detected on : " + serialPort.PortName;
                buttonStart.Enabled = true;
                comboBoxBaud.Enabled = true;
                comboBoxPort.Enabled = true;
                buttonOptions.Enabled = true;
                serialPort.Close();
                Debug.WriteLine(e);
            }

            void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
            {
                if(!SerialPortPendingClose)
                {
                    SerialPort sp = (SerialPort)sender;
                    string inData = sp.ReadExisting();
                    Debug.WriteLine("Data Received: " + inData);

                    UpdateHexText(inData);
                }
                else
                {
                    serialPort.Close();
                }
            }
        }

        /// <summary>
        /// Update Hex utilises the tab view, by translating the raw data to hex and writes it to the textbot
        /// </summary>
        /// <param name="text"></param>
        private void UpdateHexText(string text)
        {
            if (this.textBoxHexView.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateHexText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if(buttonPause.Enabled)
                {
                    char[] charValues = text.ToCharArray();
                    string hexOutput = "";
                    foreach (char _eachChar in charValues)
                    {
                        int value = Convert.ToInt32(_eachChar);
                        hexOutput += String.Format("{0:X} ", value);
                        UpdateRawText(_eachChar);
                    }
                    this.textBoxHexView.Text += hexOutput.ToString();
                }
            }
        }

        /// <summary>
        /// Upate ASCII text utilises the tab view and the ASCII partition by translating the data into readable ASCII values
        /// </summary>
        /// <param name="hexString"></param>
        public void UpdateRawText(Char data)
        {
            try
            {
                textBoxRawViewer.Text += data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Intialises the serial data ready for the user to edit
        /// </summary>
        void InitSerial()
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

        /// <summary>
        /// Makes the port combo box dynamic rather than hvaing prestored indexes
        /// Alerts the user if there are any devices connected
        /// </summary>
        void InitComboBoxPort()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(ports);
            // Display each port name to the console.
            foreach (string port in ports)
            {
                Debug.WriteLine(port);
                comboBoxPort.Text = comboBoxPort.Items.Count.ToString() + " Port(s) found";
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