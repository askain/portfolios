
function RunShell(execPath, arg) {
    var wsfile = document.getElementById("WSFileObject");
    wsfile.RunShell(execPath, arg);
}

function makeFile(content, dataFileForFault) {
    var wsfile = document.getElementById("WSFileObject");
    wsfile.WriteFile(content, dataFileForFault);
}