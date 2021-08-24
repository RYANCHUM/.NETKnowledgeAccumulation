using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceCreamMaking
{
    public partial class Log : UserControl
    {
        public delegate void AlarmAppendDelegate(Color color, string text);
        public Log()
        {
            InitializeComponent();
            //设置滚动条滚动显示最后一条新数据
            richTextBox1.SelectionStart = int.MaxValue;
            richTextBox1.SelectionLength = 1;

            this.richTextBox1.HideSelection = false;
            richTextBox1.ScrollToCaret();
        }
        /// <summary>
        /// 追加显示的文本
        /// </summary>
        /// <param name="color">文本颜色</param>
        /// <param name="text">显示的文本</param>
        public void LogAppend(Color color, string text)
        {
            if (richTextBox1.Lines.Length > 10)
            {
                richTextBox1.Clear();
            }
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(text.Trim() + "\n");
        }

        /// <summary>
        /// 显示错误日志,红色
        /// </summary>
        /// <param name="text"></param>
        public void LogError(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Red, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + text);
        }
        /// <summary>
        /// 显示警告
        /// </summary>
        /// <param name="text"></param>
        public void LogWarning(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Violet, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="text"></param>
        public void LogMessage(string text)
        {
            AlarmAppendDelegate alarm = new AlarmAppendDelegate(LogAppend);
            richTextBox1.Invoke(alarm, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
        }

        private string _name;
        [Browsable(true)]
        [Description("Text名称"), Category("")]
        public string Text
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
    }
}
