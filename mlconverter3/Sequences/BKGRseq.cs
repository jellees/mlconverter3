using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MidiLib;
using MidiLib.Messages;

namespace mlconverter3.Sequences
{
    class BKGRseq : Sequence
    {
        List<List<List<int>>> events;

        int trackCount;
        int pointer;

        private enum eventTypes : byte
        {
            tempoChange = 0,
            rest8 = 1,
            rest16 = 2,
            rest24 = 3,
            noteOn = 5,
            noteOff = 6,
            Volume = 7,
            instrument = 8,
            pitchChange = 0x0A,
            EndOfTrack = 0x0B
        }

        public BKGRseq(BinaryReader binaryReader)
            : base(binaryReader)
        {
            trackCount = binaryReader.ReadInt32();
            binaryReader.BaseStream.Position += 4;
            pointer = Pointer.ToInt(binaryReader.ReadInt32());
            binaryReader.BaseStream.Position = pointer;

            int[] trackPointers = new int[trackCount];
            for (int i = 0; i < trackPointers.Length; i++) trackPointers[i] = Pointer.ToInt(binaryReader.ReadInt32());

            events = new List<List<List<int>>>();

            // read all channels
            for (int i = 0; i < trackCount; i++)
            {
                binaryReader.BaseStream.Position = trackPointers[i];
                events.Add(readTrack(binaryReader));
            }
        }

        private List<List<int>> readTrack(BinaryReader binaryReader)
        {
            List<List<int>> track = new List<List<int>>();
            bool endOfTrack = false;

            while (!endOfTrack)
            {
                byte status = binaryReader.ReadByte();

                switch (status & 0x0F)
                {
                    case 0x00: track.Add(new List<int> { status, binaryReader.ReadUInt16() + (binaryReader.ReadByte() << 16) }); 
                        break; // tempo change
                    case 0x01: track.Add(new List<int> { status, binaryReader.ReadByte() }); 
                        break; // 8-bit rest
                    case 0x02: track.Add(new List<int> { status, binaryReader.ReadUInt16() }); 
                        break; // 16-bit rest
                    case 0x03: track.Add(new List<int> { status, binaryReader.ReadUInt16() | binaryReader.ReadByte() << 16 }); 
                        break; // 24-bit rest
                    case 0x05: track.Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte() }); 
                        break; // note on 
                    case 0x06: track.Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte() }); 
                        break; // note off
                    case 0x07: track.Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte() }); 
                        break; // track volume
                    case 0x08: track.Add(new List<int> { status, binaryReader.ReadByte() }); 
                        break; // instrument
                    case 0x0A: track.Add(new List<int> { status, binaryReader.ReadUInt16() }); 
                        break; // pitch change
                    case 0x0B: track.Add(new List<int> { status }); endOfTrack = true; 
                        break; // end of track
                    default: MessageBox.Show("unknown command found: 0x" + status.ToString("X2") + "at 0x" + binaryReader.BaseStream.Position.ToString("X8")); 
                        break; // unknown
                }
            }

            return track;
        }

        public override bool ToMidi(System.IO.BinaryWriter binaryWriter)
        {
            Midi midi = new Midi();

            midi.PPQ = 480;

            for (int i = 0; i < events.Count; i++)
            {
                midi.AddTrack(writeMidiTrack(events[i]));
            }

            midi.WriteMidi(binaryWriter);

            return true;
        }

        private Track writeMidiTrack(List<List<int>> events)
        {
            Track track = new Track();

            for (int i = 0; i < events.Count; i++)
            {
                switch ((eventTypes)(events[i][0] & 0x0F))
                {
                    case eventTypes.noteOn: track.AddMessage(new NoteOn((byte)events[i][1], (byte)events[i][2], (byte)(events[i][0] >> 4)));
                        break;
                    case eventTypes.noteOff: track.AddMessage(new NoteOff((byte)events[i][1], (byte)events[i][2], (byte)(events[i][0] >> 4)));
                        break;
                    case eventTypes.rest8: track.AddVLV(new VLV(events[i][1]));
                        break;
                    case eventTypes.rest16: track.AddVLV(new VLV(events[i][1]));
                        break;
                    case eventTypes.rest24: track.AddVLV(new VLV(events[i][1]));
                        break;
                    case eventTypes.instrument: track.AddMessage(new Patch((byte)events[i][1], (byte)(events[i][0] >> 4)));
                        break;
                    case eventTypes.EndOfTrack: track.AddMessage(new Meta(0x2F, new byte[0]));
                        break;
                    case eventTypes.tempoChange: track.AddMessage(new Meta(0x51, events[i][1]));
                        break;
                }
            }

            return track;
        }
    }
}
