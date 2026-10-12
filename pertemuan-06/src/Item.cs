namespace Pertemuan06;

public class Item
{
    public string Judul { get; }

    public Item(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        Judul = judul;
    }

    // Level 1 + 2
    public virtual int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 1000;
    }

    // Level 4
    public virtual int MasaPinjamHari => 7;

    // Level 5
    public virtual string Deskripsi()
    {
        return $"[{Judul}]";
    }

    public override string ToString()
    {
        return Deskripsi();
    }

    // Level 10 (bonus)
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null) return false;
        if (GetType() != obj.GetType()) return false;
        var other = (Item)obj;
        return string.Equals(Judul, other.Judul, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), StringComparer.OrdinalIgnoreCase.GetHashCode(Judul));
    }
}
