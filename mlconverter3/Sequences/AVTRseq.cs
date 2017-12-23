using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MidiLib;
using MidiLib.Messages;

namespace mlconverter3.Sequences
{
    class AVTRseq : Sequence
    {
        private const byte NOTE_VOLUME = 0x7F;
        private const int indexAddress = 0x0B22A4;

        List<List<Command>> events;

        // for reading
        long BitFieldAddress;
        int bits;

        private struct Command
        {
            public bool update;
            public byte note;
            public byte instrument;
        }

        public AVTRseq(BinaryReader binaryReader)
            : base(binaryReader)
        {
            byte trackCount = binaryReader.ReadByte();
            byte patternIdCount = binaryReader.ReadByte();
            binaryReader.BaseStream.Seek(1, SeekOrigin.Current); // Skip pattern loop id.
            byte patternCount = binaryReader.ReadByte();
            byte speed = binaryReader.ReadByte();
            binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
            long offset = binaryReader.BaseStream.Position;
            byte[] patternIds = binaryReader.ReadBytes(patternIdCount);

            binaryReader.BaseStream.Position = (((patternIdCount + 3) >> 2) << 2) + offset;
            int[] pointers = getPointers(binaryReader, patternCount);

            // Read actual events.
            events = new List<List<Command>>(trackCount);
            for (int i = 0; i < trackCount; i++) events.Add(new List<Command>());

            for (int i = 0; i < patternIdCount; i++)
            {
                binaryReader.BaseStream.Position = pointers[patternIds[i]];
                readPattern(binaryReader, trackCount);
            }
        }

        private int[] getPointers(BinaryReader binaryReader, int count)
        {
            int[] pointers = new int[count];
            pointers[0] = (int)binaryReader.BaseStream.Position;

            for (int i = 1; i < count; i++)
            {
                binaryReader.BaseStream.Position += (binaryReader.ReadInt32() << 2) + 4;
                pointers[i] = (int)binaryReader.BaseStream.Position;
            }

            return pointers;
        }

        private void readPattern(BinaryReader binaryReader, int trackCount)
        {
            int bitfieldLength = (5 * trackCount + 7) >> 3;
            int stepCount = binaryReader.ReadInt32();

            for (int i = 0; i < stepCount; i++)
            {
                uint offset = binaryReader.ReadUInt32();
                BitFieldAddress = indexAddress + offset;
                long address = binaryReader.BaseStream.Position;
                binaryReader.BaseStream.Position = BitFieldAddress + bitfieldLength;

                if (offset != 0)
                {
                    int track = 0;
                    bits = 128;

                    while (track < trackCount)
                    {
                        Command command = new Command();
                        command.update = true;

                        if (checkBitField(binaryReader))
                            command.note = binaryReader.ReadByte();
                        else command.note = 0;

                        if (checkBitField(binaryReader))
                            command.instrument = binaryReader.ReadByte();
                        else command.instrument = 0;

                        if (checkBitField(binaryReader))
                            binaryReader.BaseStream.Position++;
                        if (checkBitField(binaryReader))
                            binaryReader.BaseStream.Position++;
                        if (checkBitField(binaryReader))
                            binaryReader.BaseStream.Position++;

                        events[track].Add(command);
                        track++;
                    }
                }
                else
                {
                    int track = 0;

                    while (track < trackCount)
                    {
                        Command command = new Command();
                        command.update = false;
                        events[track].Add(command);
                        track++;
                    }
                }

                binaryReader.BaseStream.Position = address;
            }
        }

        private bool checkBitField(BinaryReader binaryReader)
        {
            long save = binaryReader.BaseStream.Position;
            binaryReader.BaseStream.Position = BitFieldAddress;

            bool ret = (binaryReader.ReadByte() & bits) != 0;

            if (bits <= 1)
            {
                bits = 128;
                BitFieldAddress++;
            }
            else
            {
                bits = bits >> 1;
            }

            binaryReader.BaseStream.Position = save;
            return ret;
        }

        public override bool ToMidi(BinaryWriter binaryWriter)
        {
            Midi midi = new Midi();

            midi.PPQ = 960;

            for (int i = 0; i < events.Count; i++)
            {
                midi.AddTrack(writeMidiTrack(events[i]));
            }

            midi.WriteMidi(binaryWriter);

            return true;
        }

        private Track writeMidiTrack(List<Command> commands)
        {
            Track track = new Track();

            track.AddMessage(new Controller(ControllerType.Volume, 127));
            track.AddMessage(new Patch(0));

            byte curNote = 0;

            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].update)
                {
                    if(commands[i].instrument != 0)
                    {
                        track.AddMessage(new Patch(Convert.ToByte(commands[i].instrument - 1)));
                    }

                    if (commands[i].note == 0)
                    {
                        track.AddMessage(new NoteOff(curNote, 0));
                    }
                    else
                    {
                        if (curNote != 0) 
                            track.AddMessage(new NoteOff(curNote, 0));
                        curNote = commands[i].note;
                        track.AddMessage(new NoteOn(curNote, NOTE_VOLUME));
                    }
                }

                track.AddVLV(new VLV(240));
            }

            track.AddMessage(new Meta(0x2F, new byte[0]));

            return track;
        }
    }
}
