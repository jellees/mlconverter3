using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3.Sequences
{
    abstract class Sequence
    {
        public Sequence(BinaryReader binaryReader) { }

        public abstract bool ToMidi(BinaryWriter binaryWriter);
    }
}
