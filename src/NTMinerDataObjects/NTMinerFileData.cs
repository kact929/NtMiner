﻿using System;
using System.Text;

namespace NTMiner {
    public class NTMinerFileData : INTMinerFile, IDbEntity<Guid> {
        public NTMinerFileData() {

        }

        public Guid GetId() {
            return this.Id;
        }

        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string Version { get; set; }

        public string VersionTag { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime PublishOn { get; set; }

        public string GetSignData() {
            StringBuilder sb = new StringBuilder();
            sb.Append(nameof(Id)).Append(Id)
                .Append(nameof(FileName)).Append(FileName)
                .Append(nameof(Version)).Append(Version)
                .Append(nameof(VersionTag)).Append(VersionTag)
                .Append(nameof(CreatedOn)).Append(CreatedOn.ToUlong())
                .Append(nameof(PublishOn)).Append(PublishOn.ToUlong());
            return sb.ToString();
        }
    }
}
