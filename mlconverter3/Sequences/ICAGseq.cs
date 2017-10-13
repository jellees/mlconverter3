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
    class ICAGseq : Sequence
    {
        private const byte NOTE_VOLUME = 0x7F;

        List<List<List<int>>> events;

        byte flags;

        short patternCount;

        byte[] pattern;

        long pointersAddress;

        private enum eventTypes : byte
        {
            tick = 0x00,
            note = 0x20,
            volume = 0x40,
            note_volume = 0x60,
            _8 = 0x80,
            note_8 = 0xA0,
            volume_8 = 0xC0,
            note_volume_8 = 0xE0
        }

        public ICAGseq(BinaryReader binaryReader)
            : base(binaryReader)
        {
            pointersAddress = binaryReader.BaseStream.Position + 0x16C;
            flags = binaryReader.ReadByte();
            patternCount = binaryReader.ReadInt16();
            pattern = binaryReader.ReadBytes(patternCount);

            events = new List<List<List<int>>>(16);
            for (int i = 0; i < 16; i++) events.Add(new List<List<int>>());

            // read all channels
            for (int i = 0; i < patternCount; i++)
            {
                binaryReader.BaseStream.Position = getAddress(binaryReader, pattern[i]);
                binaryReader.BaseStream.Position += 0x22; // when the tick = 0, 0x22 gets added to the pointer
                readPattern(binaryReader);
            }
        }

        /*
         * theory:
         * bit 0x20 = note: note, ??
         * bit 0x40 = volume: volume
         * bit 0x80 = ??: ??, ??
         * 
         */
        private void readPattern(BinaryReader binaryReader)
        {
            bool endOfTrack = false;

            int tick = 0;

            while (!endOfTrack)
            {
                if (tick == 0x40) break;

                byte status = binaryReader.ReadByte();
                int channel = status & 0x0F;

                switch (status & 0xF0)
                {
                    case 0x00: for (int i = 0; i < events.Count; i++) events[i].Add(new List<int> { status }); tick++;
                        break; // tick
                    case 0x20: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // ??? probably influences a note
                    case 0x40: events[channel].Add(new List<int> { status, binaryReader.ReadByte() });
                        break; // volume
                    case 0x60: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // note
                    case 0x80: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // ???
                    case 0xA0: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // ???
                    case 0xC0: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // ???
                    case 0xE0: events[channel].Add(new List<int> { status, binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte() });
                        break; // note start
                    default: throw new Exception("unknown command found: 0x" + status.ToString("X2") + "at 0x" + (binaryReader.BaseStream.Position - 1).ToString("X8"));
                }
            }
        }

        private long getAddress(BinaryReader binaryReader, int offset)
        {
            binaryReader.BaseStream.Position = pointersAddress + (offset * 4);
            return (long)Pointer.ToInt(binaryReader.ReadInt32());
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

        private Track writeMidiTrack(List<List<int>> events)
        {
            Track track = new Track();

            track.AddMessage(new Patch(0));

            int note = 0;
            int _2 = 0;
            int velocity = 0;
            int _4 = 0;
            int _5 = 0;

            for (int i = 0; i < events.Count; i++)
            {
                switch ((eventTypes)(events[i][0] & 0xF0))
                {
                    case eventTypes.tick: track.AddVLV(new VLV(240));
                        break;
                    case eventTypes.note: // note
                    case eventTypes.note_8:
                        {
                            if (velocity != 0) track.AddMessage(new NoteOff((byte)note, 0));

                            if (events[i][1] == 0x7F) note = events[i][1];
                            else note = events[i][1] + 24;
                            _2 = events[i][2];

                            if (note == 0x7F) track.AddMessage(new NoteOn((byte)note, 0));
                            else track.AddMessage(new NoteOn((byte)note, NOTE_VOLUME));
                        }
                        break;
                    case eventTypes.volume: // volume
                    case eventTypes.volume_8:
                        {
                            velocity = events[i][1];

                            if (velocity == 0)
                            {
                                track.AddMessage(new NoteOff((byte)note, 0));
                            }
                            else track.AddMessage(new Controller(ControllerType.Volume, (byte)velocity));
                        }
                        break;
                    case eventTypes.note_volume: // note and volume
                    case eventTypes.note_volume_8:
                        {
                            if (velocity != 0) track.AddMessage(new NoteOff((byte)note, 0));

                            if (events[i][1] == 0x7F) note = events[i][1];
                            else note = events[i][1] + 24;
                            _2 = events[i][2];
                            velocity = events[i][3];

                            track.AddMessage(new Controller(ControllerType.Volume, (byte)velocity));
                            if (note == 0x7F) track.AddMessage(new NoteOn((byte)note, 0));
                            else track.AddMessage(new NoteOn((byte)note, NOTE_VOLUME));
                        }
                        break;
                }
            }

            track.AddMessage(new Meta(0x2F, new byte[0]));

            return track;
        }
    }
}
