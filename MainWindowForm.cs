using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace SerialSuite
{
    public partial class MainWindowForm : Form
    {
        public SerialPort serialPort = new SerialPort();    // Initialise serial port
        int currentRow = 0;                                 // Update current row for displaying data

        public MainWindowForm()
        {
            InitializeComponent();
            InitSerial();                   // Create initial serialport values
            InitComboBoxPort();             // Update combobox to show only connected ports
            buttonPause.Enabled = true;     // Restrict user error
            buttonStop.Enabled = false;     // Restrict user error
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
                serialPort.Encoding = sPort.Encoding;
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
            OptionsWindowForm optionsWindow = new OptionsWindowForm(this);
            optionsWindow.Show();
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

            try // Check if COM port can be opened
            {
                serialPort.Open();
                labelStatusMsg.Text = "Connected to Port : " + serialPort.PortName + " Port";
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
            catch (Exception ex) // Throw excpetion for not found, alert user and reset butttons
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
                    while(rawData.Length > 0)
                    {
                        List<char> ch = new List<char>();

                        if (rawData.Length > 15)
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                ch.Add(rawData[i]);
                            }
                            rawData = rawData.Remove(0, 16);
                        }
                        else
                        {
                            for (int i = 0; i < rawData.Length; i++)
                            {
                                ch.Add(rawData[i]);
                            }
                            rawData = rawData.Remove(0);
                        }

                        char[] charValues = ch.ToArray();
                        string hexOutput = "";
                        foreach (char _eachChar in charValues)
                        {
                            int value = Convert.ToInt32(_eachChar);
                            hexOutput += String.Format("{0:X} ", value);
                        }

                        currentRow++;
                        string dat = new string(ch.ToArray());
                        DataGridViewRow row = (DataGridViewRow)serialDataGridView.Rows[0].Clone();
                        row.Cells[0].Value = currentRow;
                        row.Cells[1].Value = serialPort.PortName;
                        row.Cells[2].Value = hexOutput;
                        row.Cells[3].Value = dat;
                        serialDataGridView.Rows.Add(row);
                    }
                }
                #region Legacy
                /*
                if (this.serialDataGridView.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(UpdateHexText);
                    this.Invoke(d, new object[] { rawData });
                }
                else
                {
                    char[] charValues = rawData.ToCharArray();
                    string hexOutput = "";
                    int i = 0;
                    foreach (char _eachChar in charValues)
                    {
                        ++i;
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
                */
                #endregion
            }
            catch (Exception ex)
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
                // Default Serial Info
                serialPort.PortName = "COM0";
                serialPort.BaudRate = 125000;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.RtsEnable = true;
                serialPort.Encoding = Encoding.GetEncoding("iso-8859-1");
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
                    // Buttons
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonStart.Enabled = false;
                    buttonOptions.Enabled = false;
                    buttonUpdatePorts.Enabled = false;

                    // Combo boxes
                    comboBoxBaud.Enabled = false;
                    comboBoxPort.Enabled = false;
                    break;
                case "stop":
                    // Buttons
                    buttonPause.Enabled = true;
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = false;
                    buttonOptions.Enabled = true;
                    buttonUpdatePorts.Enabled = true;

                    // Combo boxes
                    comboBoxBaud.Enabled = true;
                    comboBoxPort.Enabled = true;
                    break;
                case "pause":
                    // Buttons
                    buttonStart.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonOptions.Enabled = false;

                    // Combo boxes
                    comboBoxBaud.Enabled = false;
                    comboBoxPort.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Export button allows for opening the populated gridview in excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                serialDataGridView.SelectAll();
                DataObject dataObj = serialDataGridView.GetClipboardContent();
                if (dataObj != null)
                    Clipboard.SetDataObject(dataObj);

                Excel.Application xlexcel;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}

delegate void SetTextCallback(string text);