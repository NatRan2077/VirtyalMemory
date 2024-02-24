//using VirtualMemory.Assets;
using VirtyalMemory;

namespace VirtualMemmoryNamespace;

class Programm
{
    public static void Main()
    {
        VirtualMemmory virtualMemmory = new VirtualMemmory();

        int? a = 0;
        int? b = 0;
        virtualMemmory[1] = 16;
        virtualMemmory[500] = 5;

        a = virtualMemmory[1];
        b = virtualMemmory[500];

        virtualMemmory[1000] = 1;
        virtualMemmory[1400] = 1;
        virtualMemmory[2000] = 1;

        virtualMemmory[100000] = 1;

        Console.WriteLine(virtualMemmory[100000]);
        Console.WriteLine(virtualMemmory[1001]);

        Console.WriteLine($"a: {a}, b: {b}");
    }
}
