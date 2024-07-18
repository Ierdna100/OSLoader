using System;
using System.IO;
using Steamworks;
using System.Diagnostics;

namespace Installer
{
    class Program
    {
        public const uint obenseuerAppId = 951240;
        public const string OSLoaderFilepathName = @"OSLoader";
        public const string obenseuerRelativeManagedFolder = @"Obenseuer_Data\Managed";
        public const string installDataFilepath = @"./installData";
        public const string newAssembliesFilepath = @"assemblies";
        public const string OSLoaderFilepathContents = @"OSLoaderContents";
        public const string modsFilepath = @"mods";

        public const string winhttpAssemblyName = "winhttp.dll";
        public const string doorstopConfig = "doorstop_config.ini";

        static void Main(string[] args)
        {
            /* Arguments for the future:
                uninstall: uninstalls the loader
                doorstop-verbose: installs verbose version of doorstop
                -y: Uses defaults for all installation parameters (like doorstop-verbose)
                update: Update current installation
                integrity: Check integrity of modloader
                --force-install
            */
            try
            {
                SteamClient.Init(obenseuerAppId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not run installer, SteamClient Initialization threw an error: {e}");
                return;
            }

            if (!SteamApps.IsAppInstalled(obenseuerAppId))
            {
                Console.WriteLine("Could not run installer: Obenseuer is not installed!");
            }
            Console.WriteLine("Obenseuer is installed.");
            Console.WriteLine();

            string installPath = SteamApps.AppInstallDir(obenseuerAppId);

            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]) && args[0] == "uninstall")
            {
                Uninstall(installPath);
                return;
            }

            Install(installPath);
            return;
        }

        private static void Install(string installPath)
        {
            // Initial checks
            if (Directory.Exists(Path.Combine(installPath, OSLoaderFilepathName))
                || File.Exists(Path.Combine(installPath, winhttpAssemblyName)))
            {
                Console.WriteLine("Could not run installer: OSLoader is already installed!");
                Console.WriteLine("Are you missing a CLI argument (update or integrity)?");
                return;
            }
            Console.WriteLine($"Found install directory at: {installPath}");
            Console.WriteLine();

            // Replace game assemblies
            Console.WriteLine("Replacing game assemblies with unstripped ones...");
            string managedPath = Path.Combine(installPath, obenseuerRelativeManagedFolder);
            string[] newAssembliesPaths = Directory.GetFiles(Path.Combine(installDataFilepath, newAssembliesFilepath));
            int count = 0;
            foreach (string file in newAssembliesPaths)
            {
                Console.WriteLine($"{(double)count++ / newAssembliesPaths.Length * 100}% - {Path.GetFileName(file)}");
                File.Copy(file, Path.Combine(managedPath, Path.GetFileName(file)), true);
            }
            Console.WriteLine();

            // Doorstop files
            Console.WriteLine("Copying doorstop files...");
            File.Copy(Path.Combine(installDataFilepath, winhttpAssemblyName), Path.Combine(installPath, winhttpAssemblyName));
            File.Copy(Path.Combine(installDataFilepath, doorstopConfig), Path.Combine(installPath, doorstopConfig));
            Console.WriteLine("Copied doorstop files successfully.");
            Console.WriteLine();

            // Add OSLoader folder
            Directory.CreateDirectory(Path.Combine(installPath, OSLoaderFilepathName));
            Console.WriteLine("Created OSLoader directory.");
            Console.WriteLine();

            // Copy dependencies into OSLoader folder
            foreach (string file in Directory.GetFiles(Path.Combine(installDataFilepath, OSLoaderFilepathContents)))
            {
                string filename = Path.GetFileName(file);
                Console.WriteLine($"Copying {filename}");
                File.Copy(file, Path.Combine(installPath, OSLoaderFilepathName, filename));
            }
            Console.WriteLine();

            // Create mods folder
            Directory.CreateDirectory(Path.Combine(installPath, OSLoaderFilepathName, modsFilepath));
            Console.WriteLine("Created OSLoader directory.");
            Console.WriteLine();

            // Copy winforms app
            // DNE yet

            Console.WriteLine("OSLoader installed! Press any key to continue...");
            Console.ReadKey();
        }

        private static void Uninstall(string installPath)
        {
            // Delete everything in the OSLoader folder
            Directory.Delete(Path.Combine(installPath, OSLoaderFilepathName), true);

            // Delete doorstop
            File.Delete(Path.Combine(installPath, winhttpAssemblyName));
            File.Delete(Path.Combine(installPath, doorstopConfig));

            // Delete remaining doorstop logs
            string[] files = Directory.GetFiles(installPath, "*.log");
            foreach (string file in files)
            {
                if (file.ToLower().Contains("doorstop_"))
                {
                    Console.WriteLine("Deleting doorstop log file with path " + file);
                    File.Delete(file);
                }
            }

            // Verify game's integrity
            VerifyGameIntegrity();
            Console.WriteLine("Verifying game integrity on Steam...");
            Console.WriteLine("You may now close this window by pressing any key. The uninstallation process will be done when Steam finishes validating the game files");
            Console.ReadKey();
        }

        private static void VerifyGameIntegrity()
        {
            var psi = new ProcessStartInfo
            {
                FileName = $@"steam://validate/{obenseuerAppId}",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };
            Process.Start(psi);
        }
    }
}
