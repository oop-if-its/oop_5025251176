namespace Pertemuan06;

public class Buku : Item
{
    public string Penulis { get; }

    public Buku(string judul, string penulis) : base(judul)
    {
        if (string.IsNullOrWhiteSpace(penulis))
            throw new ArgumentException("Penulis tidak boleh kosong.", nameof(penulis));
        Penulis = penulis;
    }

    // Level 2
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 2000;
    }

    // Level 4
    public override int MasaPinjamHari => 14;

    // Level 5
    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} oleh {Penulis}";
    }
}
