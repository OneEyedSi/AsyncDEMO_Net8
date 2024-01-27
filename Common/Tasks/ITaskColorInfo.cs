
namespace Common.Tasks
{
    public interface ITaskColorInfo<TId, TColor>
        where TId : struct
        where TColor : struct
    {
        TColor TextColor { get; }
    }
}