using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialSuite
{
    public partial class Form1 : Form
    {
        Form2 F2 = new Form2();
        public SerialPort serialPort = new SerialPort();
        bool SerialPortPendingClose = false;

        public Form1()
        {
            InitializeComponent();
            //F2.Hide();          //hide "Options" window on startup
            InitSerial();       //create initial serialport values
            InitComboBoxPort(); //update combobox to show only connected ports
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start Pressed");
            buttonPause.Enabled = true;
            buttonStop.Enabled = true;
            buttonStart.Enabled = false; //function button

            comboBoxBaud.Enabled = false;
            comboBoxPort.Enabled = false;
            buttonOptions.Enabled = false;

            serialRead();
        }

        //Stop/close serial connection
        private void buttonStop_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Stop Pressed");
            buttonPause.Enabled = true;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false; //function button

            comboBoxBaud.Enabled = true;
            comboBoxPort.Enabled = true;
            buttonOptions.Enabled = true;

            labelStatusMsg.Text = "Stopped";

            SerialPortPendingClose = true;
        }

        //Pause incoming messages but don't stop/close connection
        private void buttonPause_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Pause Pressed");
            buttonStart.Enabled = true;
            buttonStop.Enabled = true;
            buttonPause.Enabled = false; //function button

            comboBoxBaud.Enabled = false;
            comboBoxPort.Enabled = false;
            buttonOptions.Enabled = false;

            labelStatusMsg.Text = "Paused";
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            F2.Show();
        }

        private void comboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
            Debug.WriteLine("Baudrate changed to: " + serialPort.BaudRate);
        }

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = (comboBoxPort.SelectedItem.ToString());
            Debug.WriteLine("Port changed to: " + serialPort.PortName);
        }
        
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
            catch (Exception e) //throw excpetion for not found, alert user and close program
            {
                labelStatusMsg.Text = "No Serial Device detected on : " + serialPort.PortName;
                buttonStart.Enabled = true;
                comboBoxBaud.Enabled = true;
                comboBoxPort.Enabled = true;
                buttonOptions.Enabled = true;
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

        //Get serial data, convert to hex and upload into input data text box
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
                        UpdateASCIIText(hexOutput);
                    }
                    this.textBoxHexView.Text += hexOutput.ToString();
                }
            }
        }

        public void UpdateASCIIText(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;
                    textBoxASCIIViewer.Text += ascii;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

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

        void InitComboBoxPort()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Debug.WriteLine(port);
                comboBoxPort.Items.AddRange(ports);
                comboBoxPort.Text = comboBoxPort.Items.Count.ToString() + " Port(s) found";
            }
        }
    }
}

delegate void SetTextCallback(string text);
