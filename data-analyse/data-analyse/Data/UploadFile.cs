﻿using System.ComponentModel.DataAnnotations;

namespace data_analyse.Data

{
    public class UploadFile
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(260)]
        public string OriginalFileName { get; set; } = default!;

        [MaxLength(127)]
        public string? ContentType { get; set; }

        public long Length { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public byte[] Data { get; set; } = default!;


    }
}
