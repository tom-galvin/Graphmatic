using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphmatic
{
    [DefaultEvent("ColorChanged")]
    public partial class ColorChooser : UserControl
    {
        private Color _Color;

        /// <summary>
        /// Gets or sets the color selected by the ColorChooser control.
        /// </summary>
        [Description("Changes the initial color of the ColorChooser control.")]
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                UpdateColor();
            }
        }

        public event EventHandler ColorChanged;
        protected void OnColorChanged()
        {
            var colorChanged = ColorChanged;
            if (colorChanged != null)
            {
                colorChanged(this, new EventArgs());
            }
        }

        public ColorChooser()
        {
            InitializeComponent();
        }

        private void buttonPicker_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = Color;
            dialog.AllowFullOpen = true;
            dialog.SolidColorOnly = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color = dialog.Color;
            }
        }

        private void ColorChooser_Load(object sender, EventArgs e)
        {

        }

        private void UpdateColor()
        {
            buttonPicker.BackColor = Color;
            buttonPicker.Text = "";
            OnColorChanged();
        }
    }
}
