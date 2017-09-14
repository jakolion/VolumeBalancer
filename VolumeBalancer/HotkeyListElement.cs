using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VolumeBalancer
{
    class HotkeyListElement
    {
        public int id;
        public TextBox textBox;
        public Label label;
        public Func<Hotkey> getHotkey;
        public Action<Hotkey> setHotkey;
        public Action executeHotkey;

        public HotkeyListElement(int id, TextBox textBox, Label label, Func<Hotkey> getHotkey, Action<Hotkey> setHotkey, Action executeHotkey)
        {
            this.id = id;
            this.textBox = textBox;
            this.label = label;
            this.getHotkey = getHotkey;
            this.setHotkey = setHotkey;
            this.executeHotkey = executeHotkey;
        }

    }
}
