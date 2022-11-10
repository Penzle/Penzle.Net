// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Net.GettingStarted.ConsoleExample.Models
{
    internal class MedicalRelease
    {
        public string? ParentName { get; set; }
        public EmailAddress? EmailAddress { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Sex Sex { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string? PostTreatmentTherapy { get; set; }
    }
}
