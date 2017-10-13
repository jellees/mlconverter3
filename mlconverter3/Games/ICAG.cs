using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3.Games
{
    class ICAG : Game
    {
        public new const string Identifier = "ICE AGE 2 THBIAP";

        private const int indexAddress = 0xE633D4;

        private int[] pointers;

        public override string GoodName
        {
            get { return "Ice Age 2"; }
        }

        public override int Count
        {
            get { return 0x0E; }
        }

        public ICAG(BinaryReader binaryReader)
            : base(binaryReader)
        {
            binaryReader.BaseStream.Position = indexAddress;

            pointers = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                pointers[i] = Pointer.ToInt(binaryReader.ReadInt32());
                binaryReader.ReadInt32(); // skip number after the pointer
            }
        }

        public override Sequences.Sequence GetSequence(int index)
        {
            BinaryReader binaryReader = new BinaryReader(new FileStream(Rom.Instance.Path, FileMode.Open));
            binaryReader.BaseStream.Position = pointers[index];

            Sequences.Sequence sequence = new Sequences.ICAGseq(binaryReader);

            binaryReader.Close();

            return sequence;
        }

        public override int[] GetPointers()
        {
            return pointers;
        }
    }
}
