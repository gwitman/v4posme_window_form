using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_window.Libraries
{
    internal class ComboBoxItem
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
