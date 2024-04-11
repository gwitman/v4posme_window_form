using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_window.Libraries
{
    internal class ComboBoxItem(string? key, object? value)
    {
        public string? Key { get; set; } = key;
        public object? Value { get; set; } = value;

        public override string? ToString()
        {
            return Key;
        }
    }
}
