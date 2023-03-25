using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Data;

namespace ETradeBackend.WebAPI.Configuraitons.ColumnWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar, null)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            return value?.ToString() ?? null;
        }
    }
}
