using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialSuite
{
    public partial class Form2 : Form
    {
        Form1 F1;
        SerialPort sPort = new SerialPort();

        public Form2()
        {
            InitializeComponent();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

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

        private void comboBoxHandshake_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxStopBits.SelectedIndex)
            {
                case 0:
                    sPort.Handshake = Handshake.None;
                    Debug.WriteLine("Handshake changed to None");
                    break;
                case 1:
                    sPort.Handshake = Handshake.RequestToSend;
                    Debug.WriteLine("Handshake changed to RequestToSend");
                    break;
                case 2:
                    sPort.Handshake = Handshake.RequestToSendXOnXOff;
                    Debug.WriteLine("Handshake changed to RequestToSendXOnXOff");
                    break;
                case 3:
                    sPort.Handshake = Handshake.XOnXOff;
                    Debug.WriteLine("Handshake changed to XOnXOff");
                    break;
            }
        }

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

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            F1.serialPort.Parity = sPort.Parity;
            F1.serialPort.StopBits = sPort.StopBits;
            F1.serialPort.DataBits = sPort.DataBits;
            F1.serialPort.Handshake = sPort.Handshake;
            F1.serialPort.RtsEnable = sPort.RtsEnable;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
