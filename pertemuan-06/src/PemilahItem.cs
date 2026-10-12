namespace Pertemuan06;

public static class PemilahItem
{
    public static List<Buku> AmbilBuku(IEnumerable<Item> daftar)
    {
        ArgumentNullException.ThrowIfNull(daftar);
        return daftar.OfType<Buku>().ToList();
    }

    public static string? PenulisAtauNull(Item item)
    {
        if (item is Buku b)
            return b.Penulis;
        return null;
    }

    public static Buku KeBuku(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return (Buku)item; // bukan Buku -> InvalidCastException dari .NET
    }

    public static bool CobaKeBuku(Item? item, out Buku? buku)
    {
        if (item is Buku b)
        {
            buku = b;
            return true;
        }
        buku = null;
        return false;
    }
}
