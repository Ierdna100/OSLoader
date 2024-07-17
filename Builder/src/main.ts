import fs from "fs";
import chpr from "child_process";
import path from "path";

const cmd_build_OSLoader = "cd ../OSLoader/ & dotnet build";
const cmd_build_assetBundle = "";
const cmd_build_ProjectReferences = "";
const cmd_build_LoaderExe = "";
const cmd_build_Installer = "";

const outputPath = "./build/";
const logFile = "./output.log";

function main() {
    fs.writeFileSync(logFile, "");

    chpr.exec(cmd_build_OSLoader, (err, out) => {
        console.log(err);
        console.log(out);
        log(out);
    });
}

function log(text: string) {
    fs.appendFileSync(logFile, text);
}

main();
