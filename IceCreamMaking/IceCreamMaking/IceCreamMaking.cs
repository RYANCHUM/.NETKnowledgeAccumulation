using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;
using static System.Threading.Thread;

namespace IceCreamMaking
{
    public partial class IceCreamMaking : Form
    {
        delegate void AsyncUpdateUI(EnumClass.UiType type, string message);

        delegate void AsyncCallTaskFinish(int number);

        public EnumClass.ProcessType windows1Process;

        public EnumClass.ProcessType windows2Process;


        public IceCreamMaking()
        {
            InitializeComponent();
            log1.Text = "冰激凌制作窗口1";
            log2.Text = "冰激凌制作窗口2";
            log3.Text = "物料更换窗口";
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            IceCreamMakingService iceCreamMaking = new IceCreamMakingService();
            iceCreamMaking.UpdateUIDelegate += UpdateUIState;
            iceCreamMaking.CallTaskFinishDelegate += UpdateButtonState;
            Thread thread = new Thread(new ParameterizedThreadStart(iceCreamMaking.Windows1ToMakeIceCream));
            thread.IsBackground = true;
            thread.Start();
            if(thread.IsAlive)
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IceCreamMakingService iceCreamMaking = new IceCreamMakingService();
            iceCreamMaking.UpdateUIDelegate += UpdateUIState;
            iceCreamMaking.CallTaskFinishDelegate += UpdateButtonState;
            Thread thread = new Thread(new ParameterizedThreadStart(iceCreamMaking.Windows2ToMakeIceCream));
            thread.IsBackground = true;
            thread.Start();
            if(thread.IsAlive)
            {
                button2.Enabled = false;
            }
        }

        private void UpdateButtonState(EnumClass.SignalType signalType)
        {
            if(InvokeRequired)
            {
                this.Invoke(new AsyncCallTaskFinish(delegate (int signalType1)

                {
                    switch (signalType)
                    {
                        case EnumClass.SignalType.StartMaking01:
                            button1.Enabled = true;
                            break;
                        case EnumClass.SignalType.StartMaking02:
                            button2.Enabled = true;
                            break;
                        case EnumClass.SignalType.ChangeMaterial:
                            button1.Enabled = true;
                            button2.Enabled = true;
                            button3.Enabled = true;
                            break;
                        case EnumClass.SignalType.MaterialNotEnough:
                            button1.Enabled = false;
                            button2.Enabled = false;
                            break;
                    }
                }

                ), signalType);
            }
            else
            {
                switch (signalType)
                {
                    case EnumClass.SignalType.StartMaking01:
                        button1.Enabled = true;
                        break;
                    case EnumClass.SignalType.StartMaking02:
                        button2.Enabled = true;
                        break;
                    case EnumClass.SignalType.ChangeMaterial:
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        break;
                    case EnumClass.SignalType.MaterialNotEnough:
                        button1.Enabled = false;
                        button2.Enabled = false;
                        break;
                }
            }
        }
        private void UpdateUIState(EnumClass.UiType type, string message,EnumClass.ProcessType processType)
        {
            if(InvokeRequired)
            {
                this.Invoke(new AsyncUpdateUI(delegate (EnumClass.UiType type1, string message1)
                {
                    switch (type1)
                    {
                        case EnumClass.UiType.ProductWindows01:
                            log1.LogMessage(message1);
                            windows1Process = processType;
                            textBox2.Text = message1;
                            break;
                        case EnumClass.UiType.ProductWindows02:
                            log2.LogMessage(message1);
                            windows2Process = processType;
                            textBox3.Text = message1;
                            break;
                        case EnumClass.UiType.MaterialInventoryWindows:
                            textBox1.Text = message1;
                            break;
                        case EnumClass.UiType.ProductInventoryWindows:
                            break;
                        case EnumClass.UiType.ChangeMaterialWindows:
                            log3.LogMessage(message1);
                            textBox4.Text = message1;
                            break;
                    }
                }
                    ), type, message);
            }
            else
            {
                switch (type)
                {
                    case EnumClass.UiType.ProductWindows01:
                        log1.LogMessage(message);
                        break;
                    case EnumClass.UiType.ProductWindows02:
                        log2.LogMessage(message);
                        break;
                    case EnumClass.UiType.MaterialInventoryWindows:
                        textBox1.Text = message;
                        break;
                    case EnumClass.UiType.ProductInventoryWindows:
                        break;
                    case EnumClass.UiType.ChangeMaterialWindows:
                        log3.LogMessage(message);
                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if( windows1Process==EnumClass.ProcessType.FinishMaking && windows2Process == EnumClass.ProcessType.FinishMaking)
            {
                IceCreamMakingService iceCreamMaking = new IceCreamMakingService();
                iceCreamMaking.UpdateUIDelegate += UpdateUIState;
                iceCreamMaking.CallTaskFinishDelegate += UpdateButtonState;
                Thread thread = new Thread(new ParameterizedThreadStart(iceCreamMaking.ChangeMaterial));
                thread.IsBackground = true;
                thread.Start();
                if(thread.IsAlive)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;

                }
            }
            else
            {
                UpdateUIState(EnumClass.UiType.ChangeMaterialWindows, "窗口正在制作冰激凌中，无法更换原料！", EnumClass.ProcessType.CheckMaterialCount);
            }
            
            

        }
    }
}
