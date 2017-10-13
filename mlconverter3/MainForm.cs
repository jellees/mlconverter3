using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mlconverter3
{
    public partial class MainForm : Form
    {
        private int index;

        private void initInterface()
        {
            gameNameTbx.Text = Rom.Instance.Game.GoodName;

            int[] pointers = Rom.Instance.Game.GetPointers();
            for (int i = 0; i < pointers.Length; i++ ) sequenceLbx.Items.Add(i.ToString("") + ": " + Pointer.ToGba(pointers[i]).ToString("X8"));
        }

        private void setAddressLbl(int address)
        {
            addressLbl.Text = "Address 0x"+address.ToString("X8");
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void openRomBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Gameboy advance|*.gba|All Files|*.*";

            if (file.ShowDialog() == DialogResult.OK)
            {
                if (!Rom.Instance.OpenRom(file.FileName)) MessageBox.Show("This game is not supported");
                else initInterface();
            }
        }

        private void sequenceLbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = sequenceLbx.SelectedIndex;
            setAddressLbl(Rom.Instance.Game.GetPointers()[index]);
        }

        private void SequenceEditBtn_Click(object sender, EventArgs e)
        {

        }

        private void exportMidiBtn_Click(object sender, EventArgs e)
        {
            if (Rom.Instance.Opened)
            {
                SaveFileDialog file = new SaveFileDialog();
                file.Filter = "MIDI sequence | *.mid";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    Rom.Instance.ToMidi(file.FileName, index);
                }
            }
        }

        private void exportAllMidiBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                Rom.Instance.AllToMidi(folder.SelectedPath);
                MessageBox.Show("MIDIs succesfully exported!");
            }
        }

        private void sfEditBtn_Click(object sender, EventArgs e)
        {

        }

        private void exportSampleBtn_Click(object sender, EventArgs e)
        {

        }

        private void exportSfBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
