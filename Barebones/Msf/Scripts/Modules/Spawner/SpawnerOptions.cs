﻿using Barebones.Networking;
using System.Collections.Generic;
using System.Linq;

namespace Barebones.MasterServer
{
    public class SpawnerOptions : SerializablePacket
    {
        /// <summary>
        /// Public IP address of the machine, on which the spawner is running
        /// </summary>
        public string MachineIp { get; set; } = "xxx.xxx.xxx.xxx";

        /// <summary>
        /// Max number of processes that this spawner can handle. If 0 - unlimited
        /// </summary>
        public int MaxProcesses { get; set; } = 0;

        /// <summary>
        /// Region, to which the spawner belongs
        /// </summary>
        public string Region { get; set; } = "International";

        /// <summary>
        /// Spawner properties
        /// </summary>
        public DictionaryOptions CustomOptions { get; set; }

        public SpawnerOptions()
        {
            CustomOptions = new DictionaryOptions();
        }

        public override void ToBinaryWriter(EndianBinaryWriter writer)
        {
            writer.Write(MachineIp);
            writer.Write(MaxProcesses);
            writer.Write(Region);
            writer.Write(CustomOptions.ToDictionary());
        }

        public override void FromBinaryReader(EndianBinaryReader reader)
        {
            MachineIp = reader.ReadString();
            MaxProcesses = reader.ReadInt32();
            Region = reader.ReadString();
            CustomOptions = new DictionaryOptions(reader.ReadDictionary());
        }

        public override string ToString()
        {
            return $"MachineIp: {MachineIp}, MaxProcesses: {MaxProcesses}, Region: {Region}, Properties: {CustomOptions.ToDictionary().ToReadableString(", ", ": ")}";
        }
    }
}