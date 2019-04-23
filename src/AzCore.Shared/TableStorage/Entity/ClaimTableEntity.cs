﻿using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace AzCore.Shared.TableStorage.Entity
{
    public class ClaimTableEntity : TableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PolicyNumber { get; set; }

        public string Description { get; set; }

        public IList<string> SupportingDocument { get; set; }

        public IList<string> Images { get; set; }

        public string Label { get; set; }
    }
}
