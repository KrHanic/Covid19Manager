using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Covid19Manager.Business.Common
{
    public class PatientFilter
    {
        private long _timeFrom = Convert.ToInt64(DateTime.Now.AddYears(-1).ToString("yyyyMMdd")) * 10000;
        private long _timeTo { get; set; } = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")) * 10000;

        public string TimeFrom
        {
            get { return DateTime.ParseExact(
                            _timeFrom.ToString(),
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy");
            }
            set { _timeFrom = Convert.ToInt64(DateTime.ParseExact(
                            value.ToString(),
                            "dd.MM.yyyy", CultureInfo.InvariantCulture)
                            .ToString("yyyyMMddHHmm")); 
            }
        }
        public string TimeTo
        {
            get
            {
                return DateTime.ParseExact(
                          _timeTo.ToString(),
                          "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                          .ToString("dd.MM.yyyy");
            }
            set
            {
                _timeTo = Convert.ToInt64(DateTime.ParseExact(
                          value.ToString(),
                          "dd.MM.yyyy", CultureInfo.InvariantCulture)
                          .ToString("yyyyMMddHHmm"));
            }
        }

        public long TimeFromApiFormat
        {
            get
            {
                return _timeFrom;
            }
        }
        public long TimeToApiFormat
        {
            get
            {
                return _timeTo;
            }
        }
    }
}
