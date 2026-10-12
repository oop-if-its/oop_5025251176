namespace Pertemuan06;

public class Dvd : Item
{
    public int DurasiMenit { get; }

    public Dvd(string judul, int durasiMenit) : base(judul)
    {
        if (durasiMenit <= 0)
            throw new ArgumentOutOfRangeException(nameof(durasiMenit), "Durasi harus positif.");
        DurasiMenit = durasiMenit;
    }

    // Level 3 (maks Rp50.000; dicek dulu supaya tidak overflow di hari yang sangat besar)
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) return 0;
        if (hariTerlambat >= 10) return 50000;
        return hariTerlambat * 5000;
    }

    // Level 4
    public override int MasaPinjamHari => 2;

    // Level 5
    public override string Deskripsi()
    {
        return $"{base.Deskripsi()} ({DurasiMenit} menit)";
    }
}
