using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3
{
    class Rom
    {
        public static Rom Instance = new Rom();

        /// <summary>
        /// Game identity. Both game title and 
        /// </summary>
        public string GameTitle = null;

        /// <summary>
        /// The path to the rom
        /// </summary>
        public string Path = null;

        /// <summary>
        /// Wheter the rom is opened or not
        /// </summary>
        private bool opened = false;

        /// <summary>
        /// Wheter the rom is opened or not
        /// </summary>
        public bool Opened { get { return opened; } }

        /// <summary>
        /// Instance of a game
        /// </summary>
        public Games.Game Game = null;

        internal bool OpenRom(string path)
        {
            this.Path = path;

            BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open));
            binaryReader.BaseStream.Position = 0xA0;

            bool recognized = true;

            switch (GameTitle = Encoding.ASCII.GetString(binaryReader.ReadBytes(0x10)))
            {
                case Games.BKGR.Identifier: Game = new Games.BKGR(binaryReader); break;
                case Games.BAPI.Identifier: Game = new Games.BAPI(binaryReader); break;
                case Games.ICAG.Identifier: Game = new Games.ICAG(binaryReader); break;
                default: recognized = false; break;
            }

            binaryReader.Close();

            if (recognized)
            {
                opened = true;
                return true;
            }
            else return false;
        }

        internal void AllToMidi(string path)
        {
            for (int i = 0; i < Game.Count; i++)
            {
                ToMidi(path + "\\"+ Game.GoodName + " - " + i.ToString("D3") + ".mid", i);
            }
        }

        internal void ToMidi(string path, int index)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create));
            Game.GetSequence(index).ToMidi(binaryWriter);
            binaryWriter.Close();
        }
    }
}
