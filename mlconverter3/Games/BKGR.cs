using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3.Games
{
    class BKGR : Game
    {
        public new const string Identifier = "BANJOKAZOOIEBKZE";

        private const int indexAddress = 0x6AE150;

        private int[] pointers;

        public override string GoodName
        {
            get { return "Banjo Kazooie Grunty's Revenge"; }
        }

        public override int Count
        {
            get { return 0x12; }
        }

        public BKGR(BinaryReader binaryReader)
            : base(binaryReader)
        {
            binaryReader.BaseStream.Position = indexAddress;

            pointers = new int[Count];
            for (int i = 0; i < Count; i++) pointers[i] = Pointer.ToInt(binaryReader.ReadInt32());
        }

        public override Sequences.Sequence GetSequence(int index)
        {
            BinaryReader binaryReader = new BinaryReader(new FileStream(Rom.Instance.Path, FileMode.Open));
            binaryReader.BaseStream.Position = pointers[index];
            
            Sequences.Sequence sequence = new Sequences.BKGRseq(binaryReader);

            binaryReader.Close();

            return sequence;
        }

        public override int[] GetPointers()
        {
            return pointers;
        }
    }
}
