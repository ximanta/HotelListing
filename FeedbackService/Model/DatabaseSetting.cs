﻿namespace FeedbackService.Model
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string FeedbackCollectionName { get; set; } = null!;
    }
}
