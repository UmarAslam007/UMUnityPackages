mergeInto(LibraryManager.library, {
    RegisterBeforeUnload: function () {
        window.addEventListener("beforeunload", function () {
            SendMessage("UMObjectDataSaver", "SaveAll");
        });
    }
});
