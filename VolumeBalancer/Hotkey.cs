using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolumeBalancer
{
    [Serializable]
    class Hotkey
    {
        private Keys modifierKeys;
        private Keys pressedKey;

        public Hotkey(Keys modifierKeys, Keys pressedKey)
        {
            this.modifierKeys = modifierKeys;
            this.pressedKey = pressedKey;
        }

        public Keys getModifierKeys()
        {
            return modifierKeys;
        }

        public Keys getPressedKey()
        {
            return pressedKey;
        }
    }
}
