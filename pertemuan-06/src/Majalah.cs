namespace Pertemuan06;

public class Majalah : Item
{
    public int Edisi { get; }

    public Majalah(string judul, int edisi) : base(judul)
    {
        if (edisi <= 0)
            throw new ArgumentOutOfRangeException(nameof(edisi), "Edisi harus positif.");
        Edisi = edisi;
    }

    // Level 3
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        return hariTerlambat * 500;
    }

    // Level 4
    public override int MasaPinjamHari => 3;

    // Level 5
    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} edisi {Edisi}";
    }
}
