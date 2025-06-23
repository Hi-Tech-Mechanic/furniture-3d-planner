// Assets/Plugins/WebGL/LocalStorage.jslib
mergeInto(LibraryManager.library, {
    SaveToLocalStorage: function(keyPtr, valuePtr) {
        var key = UTF8ToString(keyPtr);
        var value = UTF8ToString(valuePtr);
        localStorage.setItem(key, value);
    },

    LoadFromLocalStorage: function(keyPtr, valuePtr, maxLength) {
        var key = UTF8ToString(keyPtr);
        var value = localStorage.getItem(key) || "";
        if (value.length > maxLength - 1) {
            console.error("Value too long for buffer");
            value = "";
        }
        stringToUTF8(value, valuePtr, maxLength);
    }
});