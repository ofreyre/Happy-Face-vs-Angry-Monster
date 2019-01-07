using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DBsettings {
    public USERVECTOR_TYPE userControls;
    public bool audioOn;
    public bool showAdds;

    public DBsettings(USERVECTOR_TYPE userControls, bool showAdds, bool audioOn) {
        this.userControls = userControls;
        this.showAdds = showAdds;
        this.audioOn = audioOn;
    }
}
