using SLABHIDTOUART_DLL;

using System.Text;


using CP2110Lib;
using MultimeterLib;





namespace UT61XX_Multimeter_Tester
{


    public partial class Form1 : Form
    {

        public const byte NULL = 0;
        public const ushort vid = 0x10c4;
        public const ushort pid = 0xea80;


        bool connected = false;


        void RefreshForm()
        {

            if (connected)
            {
                button1.Text = "Disconnect";
  
                button2.Enabled = true;
            }
            else
            {
                button1.Text = "Connect";
                button2.Enabled = false;

            }




        }



        UT61BPlus_Multimeter device;
        public Form1()
        {
            InitializeComponent();

            RefreshForm();


            cbPort.Items.Clear();


            List<string> serialList = PC.GetSerialList(UT61BPlus_Multimeter.pid, UT61BPlus_Multimeter.vid);

            foreach (string serial in serialList)
            {

                cbPort.Items.Add(serial);

            }
          


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!connected)
            {
                if (cbPort.Items.Count > 0)
                {
                    device = new UT61BPlus_Multimeter((uint)cbPort.SelectedIndex);
                    if (device.Connect())
                    {
                        connected = true;
                    }

                }
            }
            else
            {

                if (device.Disconnect())
                {
                    connected = false;
                }

            }

            RefreshForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                if (device.GetValue(out Structs.TestValue value))
                {
                    this.richTextBox1.AppendText(Operation.TransToString(value));
                }
            }
        }
    }
}