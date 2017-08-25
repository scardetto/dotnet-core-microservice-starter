using System;
using System.Collections.Generic;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class PersistenceStatusReporter : IReportStatus
    {
        private readonly MigrationsRepository _repo;

        public PersistenceStatusReporter(MigrationsRepository repo)
        {
            _repo = repo;
        }

        public IList<StatusItem> ReportStatus()
        {
            var statusItem = new StatusItem("__NAME__.Domain.Persistence");

            // Try sending a messages
            try {
                statusItem.Comment = $"Last migration {_repo.LastMigration}";
                statusItem.Status = StatusItem.OK;
            } catch (Exception e) {
                statusItem.Status = StatusItem.Error;
                statusItem.Comment = e.Message;
            }

            return new[] { statusItem };
        }
    }
}