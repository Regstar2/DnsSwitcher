using DnsSwitcher.Infrastructure.Windows.Desktop;

namespace DnsSwitcher.Tests;

public sealed class DesktopClientLayoutTests
{
    [Fact]
    public void GetApplicationRoot_ReturnsRepositoryRoot_ForUiBuildOutput()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "DnsSwitcherTests", Guid.NewGuid().ToString("N"));
        var baseDirectory = Path.Combine(tempRoot, "src", "DnsSwitcher.Ui", "bin", "Release", "net10.0-windows");

        try
        {
            Directory.CreateDirectory(baseDirectory);
            File.WriteAllText(Path.Combine(tempRoot, "DnsSwitcher.sln"), string.Empty);

            var root = DesktopClientLayout.GetApplicationRoot(baseDirectory);

            Assert.Equal(tempRoot, root);
        }
        finally
        {
            if (Directory.Exists(tempRoot))
            {
                Directory.Delete(tempRoot, recursive: true);
            }
        }
    }

    [Fact]
    public void GetApplicationRoot_ReturnsPackageRoot_ForPublishedUiFolder()
    {
        var baseDirectory = @"C:\Apps\DnsSwitcher\ui\";

        var root = DesktopClientLayout.GetApplicationRoot(baseDirectory);

        Assert.Equal(@"C:\Apps\DnsSwitcher", root);
    }

    [Fact]
    public void GetApplicationRoot_ReturnsPackageRoot_ForPublishedTrayFolder()
    {
        var baseDirectory = @"C:\Apps\DnsSwitcher\tray\";

        var root = DesktopClientLayout.GetApplicationRoot(baseDirectory);

        Assert.Equal(@"C:\Apps\DnsSwitcher", root);
    }
}
