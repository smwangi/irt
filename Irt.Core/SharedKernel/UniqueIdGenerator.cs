using System.Security.Cryptography;
using System.Text;

namespace Irt.Core.SharedKernel;

public class UniqueIdGenerator
{
    private static readonly long Epoch = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
    private static int _counter = 0;
    private static readonly int MachineId = GenerateMachineId();

    public static string NextId()
    {
        long timestamp = (DateTime.UtcNow.Ticks - Epoch) / 10000; // Convert to milliseconds
        int sequence = Interlocked.Increment(ref _counter) % 46656; // Ensures randomness (Base36 max 3 chars)

        string encodedTimestamp = EncodeBase36(timestamp).PadLeft(10, '0'); // Ensures at least 10 characters
        string encodedMachineId = EncodeBase36(MachineId).PadLeft(3, '0'); // 3-character machine ID
        string encodedSequence = EncodeBase36(sequence).PadLeft(3, '0'); // 3-character sequence

        // Format: Timestamp-MachineId-Sequence (Always 15+ characters)
        return $"{encodedTimestamp}-{encodedMachineId}-{encodedSequence}";
    }
    
    private static int GenerateMachineId()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[2];
        rng.GetBytes(bytes);
        return BitConverter.ToUInt16(bytes, 0) % 46656; // Max 36^3 unique machine IDs
    }

    private static string EncodeBase36(long value)
    {
        const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var sb = new StringBuilder();
        while (value > 0)
        {
            sb.Insert(0, chars[(int)(value % 36)]);
            value /= 36;
        }
        return sb.ToString();
    }
}