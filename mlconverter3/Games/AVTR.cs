using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3.Games
{
    class AVTR : Game
    {
        public new const string Identifier = "AVATAR\0\0\0\0\0\0BQZE";

        private const int indexAddress = 0x0B22A4;
        
        private int[] pointers;

        public override string GoodName
        {
            get { return "Avatar the last airbender"; }
        }

        public override int Count
        {
            get { return 0x011; }
        }

        public AVTR(BinaryReader binaryReader)
            : base(binaryReader)
        {
            binaryReader.BaseStream.Seek(indexAddress + 4, SeekOrigin.Begin);

            pointers = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                pointers[i] = binaryReader.ReadInt32() + indexAddress;
            }
        }

        public override Sequences.Sequence GetSequence(int index)
        {
            BinaryReader binaryReader = new BinaryReader(new FileStream(Rom.Instance.Path, FileMode.Open));
            binaryReader.BaseStream.Seek(pointers[index], SeekOrigin.Begin);

            Sequences.Sequence sequence = new Sequences.AVTRseq(binaryReader);

            binaryReader.Close();

            return sequence;
        }

        public override int[] GetPointers()
        {
            return pointers;
        }
    }
}
