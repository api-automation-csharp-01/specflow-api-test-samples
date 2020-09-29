using System.Collections.Generic;

namespace SpecflowAPITests.Helpers
{
    public class Helper
    {
        private List<string> _ids;
        private Dictionary<string, string> _data;

        public Helper()
        {
            _ids = new List<string>();
            _data = new Dictionary<string, string>();
        }

        public void StoreId(string id)
        {
            _ids.Add(id);
        }

        public List<string> GetIds()
        {
            return _ids;
        }

        public void StoreData(string key, string value)
        {
            _data.Add(key, value);
        }

        public Dictionary<string, string> getData()
        {
            return _data;
        }
    }
}
